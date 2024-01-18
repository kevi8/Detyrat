using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Register.Models;

namespace Register.Controllers;
// Name this anything you want with the word "Attribute" at the end
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        // Check to see if we got back null
        if(userId == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("SignIn", "Home", null);
        }
    }
}



public class HomeController : Controller
{
   private readonly ILogger<HomeController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private MyContext _context;         
    // Here we can "inject" our context service into the constructor 
    // The "logger" was something that was already in our code, we're just adding around it   
    public HomeController(ILogger<HomeController> logger, MyContext context)    
    {        
        _logger = logger;
        // When our HomeController is instantiated, it will fill in _context with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        _context = context;    
    }
    [SessionCheck]
    public IActionResult Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        ViewBag.userId =userId;
        ViewBag.userPost = _context.Posts.Where(e=>e.UserId ==userId ).ToList();
        ViewBag.otherPost = _context.Posts.Include(e=>e.Creator).Include(e=>e.Likes).Where(e=>e.UserId != userId).ToList();
        ViewBag.useri = _context.Users.FirstOrDefault(e=>e.UserId== userId);
        return View();
    } 

    public IActionResult Privacy()
    {
        return View();
    }
    [HttpGet("register")]
    public IActionResult Register(){
        return View("Auth");
    }
    [HttpPost("Create")]
    public IActionResult CreateUser(User userFromView){
        if (ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();   
            // Updating our newUser's password to a hashed version         
            userFromView.Password = Hasher.HashPassword(userFromView, userFromView.Password);
         _context.Add(userFromView);
         _context.SaveChanges();
         return RedirectToAction("SignIn");   
        }
        return View("Auth");
    }
    [HttpGet("SignIn")]
    public IActionResult SignIn(){
        
        return View("Auth");
    }
    [HttpPost("Login")]
    public IActionResult Login(SignIn userSubmission){

        if (ModelState.IsValid)
        {
            User? userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.SEmail);        
        // If no user ex
        if (userInDb == null)
        {   
            ModelState.AddModelError("Email", "Invalid Email");            
            return View("Auth"); 
        }
        PasswordHasher<SignIn> hasher = new PasswordHasher<SignIn>();                    
        // Verify provided password against hash stored in db        
        var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.SPassword);   
         if(result == 0)        
        {            
             ModelState.AddModelError("Password", "Invalid Password");            
            return View("Auth");       
        }
        int UserId = userInDb.UserId;
        HttpContext.Session.SetInt32("UserId", userInDb.UserId);
        return RedirectToAction("Index");

            
        }
        

        return View("Auth"); 
    }
    [HttpGet("LogOut")]
    public IActionResult LogOut(){
        HttpContext.Session.Clear();
        return RedirectToAction("SignIn");
    }
    [HttpGet("Like")]
    public IActionResult Like(int id){
        // Post posti = _context.Posts.FirstOrDefault(e=>e.PostId
        Like newLike = new Like();
        newLike.PostId = id;
        newLike.UserId = HttpContext.Session.GetInt32("UserId");
        _context.Add(newLike);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
    [HttpPost("CreatePost")]
    public IActionResult CreatePost(Post postiNgaView){
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (ModelState.IsValid)
        {

            postiNgaView.UserId = userId;
            _context.Add(postiNgaView);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.userId =userId;
        ViewBag.userPost = _context.Posts.Where(e=>e.UserId ==userId ).ToList();
        ViewBag.otherPost = _context.Posts.Include(e=>e.Creator).Include(e=>e.Likes).Where(e=>e.UserId != userId).ToList();
        return View("Index");
    }

    [HttpGet("post/details/{id}")]
    public IActionResult ShowComments(int id){
        Post post = _context.Posts.Include(e=> e.Comments).ThenInclude(e=> e.User).Include(e=> e.Creator).Include(e=>e.Likes).FirstOrDefault(e=> e.PostId ==id);
        @ViewBag.CommenterId = id;
        Data data = new Data();
        data.post = post;
        @ViewBag.CommenterId = id;
        return View(data);
    }

    [HttpGet("post/addcomment/{id}")]
    public IActionResult AddComment(int id){
        @ViewBag.CommenterId = id;
        return View();
    }

    [HttpPost("post/postcomment")]
public IActionResult PostComment(Comment comment)
{
    if(ModelState.IsValid)
    {
        _context.Add(comment);
        _context.SaveChanges();
        int? id = comment.PostId;
        return RedirectToAction("ShowComments", new { id = id });
    }
    return View("AddComment", comment);
}

[HttpGet("post/deletecomment/{id}")]
    public IActionResult DeleteComment(int id){
        Comment? comment = _context.Comments.FirstOrDefault(e=> e.CommentId == id);
        _context.Remove(comment);
        _context.SaveChanges();
        return RedirectToAction("ShowComments", new { id = comment.PostId });
    }

    [HttpGet("post/dislike/{id}")]
    public IActionResult Dislike(int id){
        int? userId = HttpContext.Session.GetInt32("UserId");
        Like? like = _context.Likes.FirstOrDefault(e=> e.PostId == id && e.UserId == userId );
        _context.Remove(like);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet("comment/edit/{id}")]
    public IActionResult EditComment(int id){
        int? userId = HttpContext.Session.GetInt32("UserId");
        ViewBag.CommenterId = userId;
        Comment? comment = _context.Comments.FirstOrDefault(e=> e.CommentId == id );
        return View(comment);
    }

    [HttpPost("comment/postedit/{id}")]
    public IActionResult PostEditComment(Comment comment, int id){
        if(ModelState.IsValid){
        Comment? editComment = _context.Comments.FirstOrDefault(e => e.CommentId == id);
        editComment.Content = comment.Content;
        _context.SaveChanges();
        int? idd = comment.PostId;
        return RedirectToAction("ShowComments", new {id=idd});
        }
        else{
            comment.CommentId = id;
            return View("EditComment", comment);
        }
    }

    [HttpGet("post/edit/{id}")]
    public IActionResult EditPost(int id){
        Post? post = _context.Posts.FirstOrDefault(e=> e.PostId == id );
        return View(post);
    }

    [HttpPost("post/postedit/{id}")]
    public IActionResult PostEditPost(Post post, int id){
        if(ModelState.IsValid){
        Post? editPost = _context.Posts.FirstOrDefault(e => e.PostId == id);
        editPost.Content = post.Content;
        _context.SaveChanges();
        return RedirectToAction("Index");
        }else{
            post.PostId = id;
            return View("EditPost", post);
        }
    }


    [HttpGet("post/deletepost/{id}")]
    public IActionResult DeletePost(int id){
         Post postToDelete = _context.Posts.Include(p => p.Comments).Include(p => p.Likes).FirstOrDefault(p => p.PostId == id);
        _context.Comments.RemoveRange(postToDelete.Comments);
    _context.Likes.RemoveRange(postToDelete.Likes);

    
    _context.Posts.Remove(postToDelete);

    _context.SaveChanges();
        return RedirectToAction("Index");
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}







[HttpGet("Like")]
    public IActionResult Connect(int id){
        // Post posti = _context.Posts.FirstOrDefault(e=>e.PostId
        Like newLike = new Like();
        newLike.PostId = id;
        newLike.UserId = HttpContext.Session.GetInt32("UserId");
        _context.Add(NewConnected);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
        [HttpGet("unlike")]
    public IActionResult unlike(int id){
        // Post posti = _context.Posts.FirstOrDefault(e=>e.PostId
        int? UserId = HttpContext.Session.GetInt32("UserId");
        Like likengaDb = _context.Likes.FirstOrDefault(e=> e.PostId== id && e.UserId== UserId);
        _context.Remove(likengaDb);
    
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
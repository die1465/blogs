using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using blogs.Models;
using efcoreModels;
using services;

namespace blogs.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private dbservice service;

    public HomeController(ILogger<HomeController> logger, dbservice db)
    {
        _logger = logger;
        service = db;
    }

    public IActionResult Index()
    {

        return View();
    }
    

    [HttpPost]
    public async Task<IActionResult> addComment(string comment, int id){
         comments c = new comments{comment = comment, commentId=null, blogId=id};
         await service.addComment(c);
        var blogs = service.getBlogById(id);
        return RedirectToAction("ViewBlog", "Home",new {id= id});
    }
    [HttpPost]
    public async Task<IActionResult> addCategory(categories c){
        await service.requestNewCategory(c);
        return RedirectToAction("Index", "Home");
    }
    public IActionResult addCategoryPage(){
        return View("AddCategory");
    }
    [HttpPost]
    public IActionResult blogByCategory( int categoryID){
        var b = service.getBlogByCategory(categoryID, null);
        return View("Index",b);
    }
    [Route("Home/ViewBlog/{id:int}")]
    public async Task<IActionResult> ViewBlog(int id){
        var blogs = service.getBlogById(id);
        await service.incrementViews(blogs);
        var category = service.getCategoryByID(blogs.categoryId);

        var otherBlogs = service.getBlogByCategory(blogs.categoryId,id);
        var comments = service.getComments(id);
        viewBlog blog = new viewBlog{blogCategory = new viewBlogCategory{blogs=blogs, category=category, comments = comments} , otherBlogs = otherBlogs};
        return View(blog);
    }
    public IActionResult addBlog()
    {
        ViewBag.categories = service.getCategories();
        return View();
    }
    public async Task<IActionResult> addBlogToDB(efcoreModels.blogs b){
        b.publishDate = DateTime.Now;
        b.visits =0;
        b.category = service.getCategoryByID(b.categoryId);
        await service.addBlog(b);
        return RedirectToAction("Index", "Home");
    }
    public IActionResult gotoEditblog( int id){
        var blog = service.getBlogById(id);
        return View("editBlog", blog);
    }
    public async Task<IActionResult> deleteBlog(int id){
        var blog = service.getBlogById(id);
        await service.deleteBlog(blog);
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> editBlog(string title, string blog, int blogID){
        var modifiedBlog = service.getBlogById(blogID);
        modifiedBlog.title = title;
        modifiedBlog.blog = blog;
        await service.editBlog(modifiedBlog);
        return RedirectToAction("ViewBlog", "Home",new {id= modifiedBlog.blogId});
    }
    public async Task<IActionResult> deleteComment(int id){
        var comment = service.getCommentById(id);
        await service.deleteComment(comment);
        var blog = service.getBlogById(comment.blogId);
        
        return RedirectToAction("ViewBlog", "Home",new {id= blog.blogId});
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

using System.ComponentModel.DataAnnotations;
using efcoreModels;
using Microsoft.AspNetCore.Mvc;
using services;
namespace components;

public class addCategoryViewComponent : ViewComponent{
    public async Task<IViewComponentResult> InvokeAsync(){
        return View();
    }
}
public class showBlogsInCategoryViewComponent : ViewComponent{
    public async Task<IViewComponentResult> InvokeAsync(List<efcoreModels.blogs> b){
        return View(b);
    }
}
public class addCommentViewComponent : ViewComponent{
    public async Task<IViewComponentResult> InvokeAsync(int id){
        return View(id);
    }
}
public class blogByCategoryViewComponent : ViewComponent{
    private readonly dbservice context;
    public blogByCategoryViewComponent(dbservice db){
        context =db;
    }
    public async Task<IViewComponentResult> InvokeAsync(){
        var category = context.getCategories();        
        return View(category);
    }
}
public class blogByMostViewedViewComponent : ViewComponent{
    private readonly dbservice context;
    public blogByMostViewedViewComponent(dbservice db){
        context =db;
    }
    public async Task<IViewComponentResult> InvokeAsync(){
        var mostViewedBlog = context.getBlogByMostViewed();
        return View(mostViewedBlog);
    }
}
public class otherBlogsInSameCategoryViewComponent : ViewComponent{
    private readonly dbservice context;
    public otherBlogsInSameCategoryViewComponent(dbservice db){
        context =db;
    }
    public async Task<IViewComponentResult> InvokeAsync(viewBlog b){
        return View(b);
    }
}
public class seeRequestedCategoriesViewComponent : ViewComponent{
    private readonly dbservice context;
    public seeRequestedCategoriesViewComponent(dbservice db){
        context =db;
    }
    public async Task<IViewComponentResult> InvokeAsync(){
        var requestedCategories = await context.getRequestedCategories();
        return View(requestedCategories);
    }
}

public class theBlogViewComponent : ViewComponent{
    private readonly dbservice context;
    public theBlogViewComponent(dbservice db){
        context =db;
    }
    public async Task<IViewComponentResult> InvokeAsync(viewBlogCategory b){
        
        
        return View(b);
    }
}
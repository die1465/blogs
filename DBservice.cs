using efcoreModels;
using Microsoft.EntityFrameworkCore;
namespace services;

public class dbservice{

    private blogDBcontext context;
    public dbservice(blogDBcontext b){
        context = b;
        context.Database.EnsureCreated();
        context.SaveChanges();
    }
    
    public async Task<int> addBlog(efcoreModels.blogs b){
        b.blogId = null;
        await context.blogs.AddAsync(b);
        await context.SaveChangesAsync();
        return 1;
    }
    public async Task<int> deleteBlog(efcoreModels.blogs b){
        context.blogs.Remove(b);
        await context.SaveChangesAsync();
        return 1;
    }
    public List<efcoreModels.blogs> getBlogByCategory(int categoryId, int? blogId){
        return context.blogs.Where(e => e.categoryId == categoryId && e.blogId != blogId).ToList();
    }
    public List<efcoreModels.blogs> getAllBlogsByCategory(){
        return context.blogs
        .OrderBy(e => e.categoryId).ToList();
    }
    public efcoreModels.blogs getBlogById(int? id){
        return context.blogs.Where(e => e.blogId == id).First();
    }
    public  List<efcoreModels.blogs> getBlogByMostViewed(){
        return context.blogs.OrderByDescending(e => e.visits).ToList();
    }
     public categories getCategoryByID(int id){
        categories c= context.categories.Where( e => e.categoryId == id).FirstOrDefault();
        if(c == null){
            return new categories{category="",categoryId=-1};
        }
        return c;
    }
    public List<comments> getComments(int blogID){
        return context.comments.Where(e => e.blogId == blogID).ToList();
    }
    public comments getCommentById(int CID){
        return context.comments.Where(e => e.commentId == CID).First();
    }
    public async Task<List<efcoreModels.blogs>> getBlogByNewest(){
        return await context.blogs.OrderBy(e => e.publishDate).ToListAsync();
    }

    public async Task<int> requestNewCategory(categories c){
        if(context.categories.Any(e => e.category == c.category) ) return 0;
        // c.categoryRequestId = Guid.NewGuid();
        
        await context.categories.AddAsync(c);
        await context.SaveChangesAsync();
        return 1;
    }
    public  List<categories> getCategories(){
        return context.categories.ToList();
    }
    public async Task<List<categoryRequests>> getRequestedCategories(){
        return await context.categoryRequests.ToListAsync();
    }
    //addRequestedCategory
    public async Task<int> addRequestedCategories(categoryRequests c){
        var cat = new categories{
        category = c.categoryRequest,
        categoryId = null
        };
        context.categories.Add(cat);
        await context.SaveChangesAsync();
        return 1;
    }
    public async Task<int> editBlog(efcoreModels.blogs b){
            context.blogs.Attach(b);
            await context.SaveChangesAsync();
        return 1;
    }
    public async Task<int> addComment(comments c){
            c.commentId=null;
            context.comments.Add(c);
            await context.SaveChangesAsync();
        return 1;
    }
    public async Task<int> editComment(comments c){
        
            context.comments.Attach(c);
            await context.SaveChangesAsync();
        return 1;
    }
    public async Task<int> deleteComment(comments c){
        
            context.comments.Remove(c);
            await context.SaveChangesAsync();
        return 1;
    }
    public async Task<int> incrementViews(efcoreModels.blogs b){
        
            b.visits++;
            context.blogs.Attach(b).Property(e => e.visits).IsModified = true;
            await context.SaveChangesAsync();
        return 1;
    }
}
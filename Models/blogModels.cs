using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace efcoreModels;

public class blogs{
    [Column("blogId")]
    public Nullable<int> blogId { get; set; }
    [Column("title")]
    public string title { get; set; } = default!;
    [Column("blog")]
    public string blog { get; set; } = default!;
    [Column("publishDate")]
    public DateTime publishDate { get; set; }
    [Column("visits")]
    public int visits { get; set; }
    [Column("categoryId")]
    public int categoryId { get; set; }
    public categories category { get; set; }=default!;
    // public virtual List<comments> comments { get; set; }=default!;
    public List<images> images { get; set; }=default!;

}
public class comments{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Nullable<int> commentId { get; set; }
    public string comment { get; set; }=default!;
    [ForeignKey("blogs")]
    public int blogId { get; set; }
    public blogs blogs {get;set;}=default!;
}
public class images{
    public Nullable<int> imageId { get; set; }
    public string imagePath { get; set; }=default!;
    public int blogId { get; set; }
    public blogs blog { get; set; }=default!;

}
public class categories{
    public Nullable<int> categoryId { get; set; }
    public string category { get; set; }=default!;
    public List<blogs> blogs { get; set; }=default!;
}
public class categoryRequests{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Nullable<int> categoryRequestId { get; set; }
    public string categoryRequest { get; set; }=default!;
}
//viewModel -> used because a view can only accept 1 model so
//i'm going to combine 2 existing models into 1

public class viewBlog{
    //my going to use this to display a blog
    public List<blogs> otherBlogs { get; set; }=default!;
    public viewBlogCategory blogCategory { get; set; }=default!;
}
public class viewBlogCategory{
    public categories category {get;set;}= default!;
    public blogs blogs { get; set; }=default!;
    public List<comments> comments { get; set; } = default!;

}
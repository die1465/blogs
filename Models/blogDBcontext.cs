using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using appuser;
using Microsoft.AspNetCore.Identity;

namespace efcoreModels;


public class blogDBcontext:IdentityDbContext<ApplicationUser>{
    public DbSet<efcoreModels.blogs> blogs{get;set;}=default!;
    public DbSet<categories> categories{get;set;}=default!;
    public DbSet<categoryRequests> categoryRequests{get;set;}=default!;
    public DbSet<comments> comments{get;set;}=default!;
    public DbSet<images> images{get;set;}=default!;

    private readonly IConfiguration _configuation;
    public blogDBcontext( DbContextOptions options, IConfiguration configuration=default!) : base(options){
        _configuation = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        var connectionString = _configuation.GetConnectionString("blogDbcontext");
        optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8,0,27)));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<categories>(entity => {
            entity.HasKey(e => e.categoryId);
            entity.Property(e => e.category);
            entity.HasMany(e => e.blogs).WithOne();
        });
        modelBuilder.Entity<efcoreModels.blogs>(entity => {
            entity.HasKey(e => e.blogId);
            entity.Property(e => e.title);
            entity.Property(e => e.visits);
            entity.Property(e => e.publishDate);
            entity.Property(e => e.blog);
            entity.HasOne(e => e.category)
            .WithMany(e => e.blogs)
            .HasForeignKey(e => e.categoryId);
            entity.HasMany(e => e.images).WithOne();
        });
        
        modelBuilder.Entity<comments>(entity => {
            entity.HasKey(e => new {e.commentId, e.blogId});
            entity.HasOne(e => e.blogs)
            .WithMany()
            .HasForeignKey(e => e.blogId);
            entity.Property( e=> e.blogId).HasColumnName("blogId");
        });
        modelBuilder.Entity<images>(entity => {
            entity.HasKey(e => new {e.imageId, e.imagePath, e.blogId});
            entity.HasOne(e => e.blog).WithMany().HasForeignKey(e => e.blogId);
        });
        modelBuilder.Entity<categoryRequests>(entity => {
            entity.HasKey(e => e.categoryRequestId);
            entity.Property(e => e.categoryRequest);
        });
    }



}
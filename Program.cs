using appuser;
using efcoreModels;
using services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// var connectionString = builder.Configuration.GetConnectionString("blogsIdentityDbContextConnection");builder.Services.AddDbContext<blogsIdentityDbContext>(options =>
//     options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<blogDBcontext>(ServiceLifetime.Transient);
builder.Services.AddTransient<dbservice>();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
options.SignIn.RequireConfirmedAccount = false)
.AddEntityFrameworkStores<blogDBcontext>()
.AddDefaultUI()
.AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

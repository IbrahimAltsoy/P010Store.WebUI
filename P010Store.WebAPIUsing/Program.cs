using P010Store.Data.Absract;
using P010Store.Data.Concrete;
using P010Store.Data;
using P010Store.Service.Absract;
using P010Store.Service.Concreate;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>();
// EntityFramework iþlemlerini yapabilmek için kullanýyoruz
builder.Services.AddTransient(typeof(IProductService), typeof(ProductService));// Projeye özel yaptýðýmýz servisi ekledik
builder.Services.AddTransient(typeof(ICategoryService), typeof(CategoryService));
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));
builder.Services.AddSession();
builder.Services.AddHttpClient();// dotnet xore de web api kullanabilmek için gerekli servisimiz 
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login";
    x.LogoutPath = "/Admin/Login/Logout";
    x.Cookie.Name = "Administrator";
    x.AccessDeniedPath = "/AccessDenied";
    x.Cookie.MaxAge = TimeSpan.FromDays(1000);
});
// Yetkilendirme ayarlarý burada yapýlmýþtýr.
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role", "Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim("Role", "User"));
});
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

app.UseRouting();
app.UseSession();// Sesssion için ekledik. 
app.UseAuthentication();// önce oturum açma sonra yetkilendirme(app.UseAuthorization();) ayarlarý da yapmýþ olduk. 

app.UseAuthorization();

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "custom",
    pattern: "{customurl?}/{controller=Home}/{action=Index}/{id?}");

app.Run();

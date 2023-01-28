using Microsoft.AspNetCore.Authentication.Cookies;
using P010Store.Data;
using P010Store.Data.Absract;
using P010Store.Data.Concreate;
using P010Store.Data.Concrete;
using P010Store.Service.Absract;
using P010Store.Service.Concreate;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>();
// EntityFramework i�lemlerini yapabilmek i�in kullan�yoruz
builder.Services.AddTransient(typeof(IProductService), typeof(ProductService));// Projeye �zel yapt���m�z servisi ekledik
builder.Services.AddTransient(typeof(ICategoryService), typeof(CategoryService));
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Veritaban� i�lemleri yapaca��m�z servisleri ekledik. Burada .net core a e�er sana Iservice interface i kullanma iste�i gelirse  Iservice s�n�f�nda bir nesne olu�tur demi� oluyoruz. 
//. Net core da 3 farkl� y�ntemle servicesleri ekliyoruz. 
//builder.Services.AddSingleton(); AddSingleton(); kullanarak olu�turdu�umuz nesneden 1 tane �rnek olu�tur ve her seferinde bu �rnek kullan�l�r. 
//builder.Services.AddTransient() y�nteminde ise �nceden olu�mu� nesne varsa o kullan�l�r yoksa yenisi olu�tururulur. 
//builder.Services.AddScoped() y�nteminde ise yap�lan istek i�in yeni bir nesne i��in olu�turulur. 

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login";
    x.LogoutPath = "/Admin/Login/Logout";
    x.Cookie.Name = "Administrator";
    x.AccessDeniedPath = "/AccessDenied";
    x.Cookie.MaxAge = TimeSpan.FromDays(1000);
});
// Yetkilendirme ayarlar� burada yap�lm��t�r.
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy=> policy.RequireClaim("Role", "Admin"));
    x.AddPolicy("UserPolicy", policy=> policy.RequireClaim("Role", "User"));
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
app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "custom",
    pattern: "{customUrl?}{controller=Home}/{action=Index}/{id?}");

app.Run();

// Katmanl� mimaride ili�ki Hiyerar�isi 
//1- WebUI �zerinden veritaban� i�lemlerini yapabilmek i�in WebUI �n dependencies(referanslar�na) Service katman�na dependencies e sa� t�klay�p add project references diyerek a��lan pencereden Service katman�na tik koyupo ok butonuyla pencereyi kapat�p ba�lant� kurduk. 
// 2- Service katman� da veritaban� i�lemlerini yapabilmek i�in Data katman�na eri�mesi gerekiyor, yine depencecies e sa� t�klay�p add project references diyerek a��lan pencereden Data katman�na i�aret koyup ekliyoruz. 
//3- Data katmana�n�n entitlere ula�abilmesi gerekiyor ki classlar� kullanarak veritaban� i�lmelerini yapabilsin. Yine ayn� yolu izleyerek veya classlar�n �zerine gelip empul e ad project references diyerek data dan entities e eri�im vermemiz gerekiyor. 
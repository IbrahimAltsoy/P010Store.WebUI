using P010Store.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>();
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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Katmanlý mimaride iliþki Hiyerarþisi 
//1- WebUI üzerinden veritabaný iþlemlerini yapabilmek için WebUI ýn dependencies(referanslarýna) Service katmanýna dependencies e sað týklayýp add project references diyerek açýlan pencereden Service katmanýna tik koyupo ok butonuyla pencereyi kapatýp baðlantý kurduk. 
// 2- Service katmaný da veritabaný iþlemlerini yapabilmek için Data katmanýna eriþmesi gerekiyor, yine depencecies e sað týklayýp add project references diyerek açýlan pencereden Data katmanýna iþaret koyup ekliyoruz. 
//3- Data katmanaýnýn entitlere ulaþabilmesi gerekiyor ki classlarý kullanarak veritabaný iþlmelerini yapabilsin. Yine ayný yolu izleyerek veya classlarýn üzerine gelip empul e ad project references diyerek data dan entities e eriþim vermemiz gerekiyor. 
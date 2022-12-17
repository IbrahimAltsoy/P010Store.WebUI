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

// Katmanl� mimaride ili�ki Hiyerar�isi 
//1- WebUI �zerinden veritaban� i�lemlerini yapabilmek i�in WebUI �n dependencies(referanslar�na) Service katman�na dependencies e sa� t�klay�p add project references diyerek a��lan pencereden Service katman�na tik koyupo ok butonuyla pencereyi kapat�p ba�lant� kurduk. 
// 2- Service katman� da veritaban� i�lemlerini yapabilmek i�in Data katman�na eri�mesi gerekiyor, yine depencecies e sa� t�klay�p add project references diyerek a��lan pencereden Data katman�na i�aret koyup ekliyoruz. 
//3- Data katmana�n�n entitlere ula�abilmesi gerekiyor ki classlar� kullanarak veritaban� i�lmelerini yapabilsin. Yine ayn� yolu izleyerek veya classlar�n �zerine gelip empul e ad project references diyerek data dan entities e eri�im vermemiz gerekiyor. 
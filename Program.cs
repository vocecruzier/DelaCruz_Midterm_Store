using Microsoft.EntityFrameworkCore;
using DelaCruz_Midterm_Store.Data;

var builder = WebApplication.CreateBuilder(args);

// ✿ Database ✿ (•ᴗ•)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ✿ Auto-create the database on start so the app always runs ✿
using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

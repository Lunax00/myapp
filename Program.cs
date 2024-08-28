using Microsoft.EntityFrameworkCore;
using MyApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using MyApp.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<MyAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyAppContextConnection")));

builder.Services.AddDefaultIdentity<MyAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MyAppContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Ensure Razor Pages are added

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Ensure Razor Pages mapping

app.Run();

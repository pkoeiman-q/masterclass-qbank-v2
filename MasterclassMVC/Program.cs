using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MasterclassMVC.Data;
using MasterclassMVC.Areas.Identity.Data;
using MasterclassApiTest.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MvcUserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MvcUserDbContextConnection"))
);

builder.Services.AddDefaultIdentity<MvcUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MvcUserDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

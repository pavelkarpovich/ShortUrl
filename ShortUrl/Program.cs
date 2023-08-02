using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShortUrl.ApplicationCore.Entities;
using ShortUrl.ApplicationCore.Interfaces;
using ShortUrl.Infrastructure.Data;
using ShortUrl.Web.Interfaces;
using ShortUrl.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider();
builder.Services.AddSession();
builder.Services.AddDbContext<ApplicationDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbConnection")));
builder.Services.AddDbContext<AccountDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("UserDbConnection")));
builder.Services.AddIdentity<Account, IdentityRole>().AddEntityFrameworkStores<AccountDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IAliasService, AliasService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<ApplicationDbContext>();
        if (catalogContext.Database.IsSqlServer())
        {
            catalogContext.Database.EnsureCreated();
        }

        var accountContext = scopedProvider.GetRequiredService<AccountDbContext>();
        if (accountContext.Database.IsSqlServer())
        {
            accountContext.Database.EnsureCreated();
        }
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred adding migration to Database");
    }
}
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

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

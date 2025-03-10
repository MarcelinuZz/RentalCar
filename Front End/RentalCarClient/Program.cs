using RentalCarClient.Service;
using RentalCarClient.Handler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAuthService, AuthHandler>();
builder.Services.AddHttpClient<IAuthService, AuthHandler>();

builder.Services.AddHttpClient<ICarService, CarHandler>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();
app.UseSession();


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

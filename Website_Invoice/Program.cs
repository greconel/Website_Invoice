using Website_Invoice.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Stap 2
builder.Services.AddHttpClient<IAdminRepository<Customer>, AdminRepository<Customer>>(httpclient =>
    httpclient.BaseAddress = new Uri(builder.Configuration["ServiceAddress"] + "customers/"));
builder.Services.AddHttpClient<IAdminRepository<Invoice>, AdminRepository<Invoice>>(httpclient =>
    httpclient.BaseAddress = new Uri(builder.Configuration["ServiceAddress"] + "invoices/"));


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

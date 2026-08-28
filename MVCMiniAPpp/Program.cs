var builder = WebApplication.CreateBuilder(args);

// MVC xidmətlərini əlavə edirik
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Şəkillər və CSS-lərin işləməsi üçün vacibdir

app.UseRouting();

app.UseAuthorization();

// MVC marşrutunu təyin edirik
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
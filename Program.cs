using Microsoft.EntityFrameworkCore;
using MVC_EF.Exemplo1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Escolher o banco dinamicamente com base na configuração
string dbProvider = builder.Configuration["DatabaseProvider"]; // Exemplo: "SQLite" ou "Pgsql"

if (dbProvider == "Pgsql")
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("Pgsql")));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("SQLite")));
}

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Livro}/{action=Index}/{id?}");

app.Run();
using GestionEmpresarial.DBContext;
using GestionEmpresarial.Helpers.Seed;
using GestionEmpresarial.Interfaces;
using GestionEmpresarial.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión a la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configuración de la autenticación basada en cookies
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";

        options.AccessDeniedPath = "/Login/AccessDenied";

        options.Cookie.Name = "GestionEmpresarial.Auth";

        options.Cookie.HttpOnly = true;

        options.Cookie.MaxAge = null;
        
        options.SlidingExpiration = true;

        options.ExpireTimeSpan = TimeSpan.FromHours(3);
    });

// Configuración de AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    
},
typeof(Program));


// Agregar servicios al contenedor de dependencias
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<ISeeder, RolSeeder>();
builder.Services.AddScoped<ISeeder, PermisoSeeder>();
builder.Services.AddScoped<ISeeder, PermisoRolSeeder>();
builder.Services.AddScoped<ISeeder, UsuarioSeeder>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddAuthorization();



var app = builder.Build();

await SeedData.InicializarAsync(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

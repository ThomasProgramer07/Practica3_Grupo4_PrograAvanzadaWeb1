
using Microsoft.EntityFrameworkCore;
using WebAvanzadaIICuatrimestre.BLL;
using WebAvanzadaIICuatrimestre.BLL.Services.PartidoPolitico;
using WebAvanzadaIICuatrimestre.BLL.Services.RepresentanteLegal;
using WebAvanzadaIICuatrimestre.BLL.Services.Votante;
using WebAvanzadaIICuatrimestre.DAL.Data;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico;
using WebAvanzadaIICuatrimestre.Middleware;
using WebAvanzadaIICuatrimestre.BLL.Services.Votacion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register EF Core DbContext (SQLite). Update the connection string in appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


//Inyecci�n de dependencias para repositorios, servicios, etc. Extraer a clase configuracion de servicios para mantener el Program.cs limpio y organizado. Se pueden crear clases est�ticas para cada capa (Repositorios, Servicios, etc.) y llamar a sus m�todos de configuraci�n desde aqu� para una mejor organizaci�n.
// Repositorios
builder.Services.AddScoped(typeof(IRepositorioGenerico<>), typeof(RepositorioGenerico<>));

//Servicios
builder.Services.AddScoped<IPartidoPoliticoServicio, PartidoPoliticoServicio>();
builder.Services.AddScoped<IRepresentanteLegalServicio, RepresentanteLegalServicio>();
builder.Services.AddScoped<IVotanteServicio, VotanteServicio>();

// Servicios Terceros
builder.Services.AddAutoMapper(cfg => { }, typeof(MapeoClases)); // Directamente desde la documentaci�n

//Servicios de Votacion
builder.Services.AddScoped<IVotacionServicio, VotacionServicio>();

builder.Services.AddHttpClient("Api", client =>
{
    client.BaseAddress = new Uri("https://localhost:7252/"); // el puerto de TU API
})
// Solo para desarrollo: aceptar el certificado autofirmado de localhost
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
});



















var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//MIDDLEWARES
app.UseMiddleware<MiddlewareGlobalExceptionHandler>();

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "RepresentanteLegal",
    pattern: "{controller=RepresentanteLegal}/{action=Index}/{id?}")
    .WithStaticAssets();

//FILTERS





//INGRESO DE VARIASBLES DE ENTORNO AZURE KEYVAULTS





//NO SE PUEDAN REGLAS DE NEGOCIO


app.Run();

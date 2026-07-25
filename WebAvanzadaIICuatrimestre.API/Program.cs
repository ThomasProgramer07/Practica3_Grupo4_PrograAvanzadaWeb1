using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebAvanzadaIICuatrimestre.BLL.Services.PartidoPolitico;
using WebAvanzadaIICuatrimestre.BLL.Services.RepresentanteLegal;
using WebAvanzadaIICuatrimestre.BLL.Services.Votacion;
using WebAvanzadaIICuatrimestre.BLL.Services.Votante;
using WebAvanzadaIICuatrimestre.DAL.Data;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepositorioGenerico<>), typeof(RepositorioGenerico<>));
builder.Services.AddScoped<IVotacionServicio, VotacionServicio>();
builder.Services.AddScoped<IPartidoPoliticoServicio, PartidoPoliticoServicio>();
builder.Services.AddScoped<IRepresentanteLegalServicio, RepresentanteLegalServicio>();
builder.Services.AddScoped<IVotanteServicio, VotanteServicio>();
builder.Services.AddScoped<IVotacionServicio, VotacionServicio>();
builder.Services.AddAutoMapper(cfg => { }, typeof(WebAvanzadaIICuatrimestre.BLL.MapeoClases));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();   // interfaz de Scalar para probar los endpoints
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
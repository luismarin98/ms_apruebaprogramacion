using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ms_apruebaprogramacion.Constans;
using ms_apruebaprogramacion.Controllers.Contract;
using ms_apruebaprogramacion.Controllers.Impl;
using ms_apruebaprogramacion.Service.Impl;
using ms_apruebaprogramacion.Utils;
using saff_core.configuracion;
using saff_core.utilitarios;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<LogUtil>();
builder.Services.AddControllers();
builder.Services.AddScoped<IDatosProgLabController, DatosProgLabController>();
builder.Services.AddScoped<IDatosProgLabRepository, DatosProgLabRepository>();
builder.Services.AddScoped<IDatosProgLabService, DatosProgLabService>();
builder.Services.AddHealthChecks();

string URL_CONSULTAR_KEYS = Environment.GetEnvironmentVariable(App.URL_KEY_DB)!;
string AMBIENTE = Environment.GetEnvironmentVariable(App.AMBIENTE)!;

if (string.IsNullOrEmpty(URL_CONSULTAR_KEYS)) throw new ArgumentException(App.URL_KEY_DB_NO_DEFINIDO);
if (string.IsNullOrEmpty(URL_CONSULTAR_KEYS)) throw new ArgumentException(App.URL_KEY_DB_NO_DEFINIDO);

var CredencialAplicacionType = App.ObtenerCredencialesAplicacion(URL_CONSULTAR_KEYS, AMBIENTE);
Provider Provider = new();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = General.Nombre_Servicio + "-" + General.Tipo_Servicio, Version = "v1" });

});

// En el m?todo ConfigureServices
builder.Services.AddCors(options =>
{
    options.AddPolicy("NUXT", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (CredencialAplicacionType.SWAGGER_ON)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", General.Nombre_Servicio + "-" + General.Tipo_Servicio + " v1");
    });
}

app.UseCors("NUXT");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseHealthChecks("/health");
app.Run();
using Microsoft.EntityFrameworkCore;
using api_transacciones.Models;
using ApiTrans.Services;
//using middlewares;
using Resend;
using ApiTrans.Email;
//using ApiTrans.Data.PaypalService;
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    // Desactiva el FileWatcher (útil en entornos con muchos archivos)
    WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"),
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: false);
if (!builder.Environment.IsDevelopment())
{
    builder.Services.Configure<StaticFileOptions>(options =>
    {
        options.OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=3600");
        };
    });
}
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton
);
builder.Services.AddOptions();
builder.Services.AddHttpClient<ResendClient>();
builder.Services.Configure<ResendClientOptions>( o =>
{
    o.ApiToken = Environment.GetEnvironmentVariable( "RESEND_KEY" )!;
} );
builder.Services.AddTransient<IResend, ResendClient>();
builder.Services.AddScoped<AuthCapituloService>();
builder.Services.AddScoped<NotificacionesService>();
builder.Services.AddScoped<PedidoService>();
builder.Services.AddScoped<DonacionService>();
builder.Services.AddScoped<TemplateService>();
builder.Services.AddControllers();
//builder.Services.AddTransient<PaypalService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
//midleware
// anda(creo) pero lo desabilito para el desarrollo
//app.UseMiddleware<JwtMiddleware>();
//app.UseAuthorization();

app.MapControllers();
app.UseWebSockets();

app.Run();

// generar modelos automaticamente
// dotnet ef dbcontext scaffold "appsettings.json>>DefaultConnection" 
// Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Models --context MyDbContext --schema transacciones

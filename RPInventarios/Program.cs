using AspNetCoreHero.ToastNotification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RPInventarios.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddNotyf(config =>
{
    config.DurationInSeconds = 5;
    config.IsDismissable = true;
    config.Position = NotyfPosition.BottomRight;
});

builder.Services.AddDbContext<InventariosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventariosContext") ?? throw new InvalidOperationException("Connection string 'InventariosContext' not found.")));

// Ofrecer información de diagnóstico sobre errores en entorno de desarrollo.
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

// Asegura que la BD exista y esté lista para usarse. Útil para entorno de desarrollo y pruebas. En producción se debe usar migraciones.
using (var scope = app.Services.CreateScope()) // Crear un ámbito de servicios.
{
    var services = scope.ServiceProvider; // Permite acceder a los servicios registrados en el contenedor de dependencias.
    var context = services.GetRequiredService<InventariosContext>(); // Recupera el contexto de la BD configurado para la app.
    context.Database.EnsureCreated(); // Verifica si la BD existe, si no, la crea automáticamente según el modelo definido.
    DbInitializer.Initialize(context);
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();

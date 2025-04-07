using Microsoft.EntityFrameworkCore;
using PomodoroAppBackend.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("seus");

builder.Services.AddCors(options =>
{
    Console.WriteLine("seus1");

    // Define a CORS policy named "AllowReactApp" to allow requests from the specified origin
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173", "http://localhost:5173") // React frontend's URL
            .AllowAnyMethod() // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials(); // For using cookies or session
    });
});

builder.WebHost.ConfigureKestrel(options =>
{
    Console.WriteLine("seus2");

    options.ListenAnyIP(8080);
});
Console.WriteLine("seus3");

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
Console.WriteLine("seus4");

// Register HttpClient
builder.Services.AddControllers();
Console.WriteLine("seus5");

builder.Services.AddHttpClient();
Console.WriteLine("seus6");

// Add session services
Console.WriteLine("seus7");

builder.Services.AddDistributedMemoryCache(); // For in-memory session storage
Console.WriteLine("seus8");

builder.Services.AddSession(options =>
{
    Console.WriteLine("seus9");

    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Always use secure cookies
    options.Cookie.SameSite = SameSiteMode.None; // Allow cross-origin cookies
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set timeout for session
    options.Cookie.HttpOnly = true; // Protect the session cookie
    options.Cookie.IsEssential = true; // Make the cookie essential
});

Console.WriteLine("seus10");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
Console.WriteLine("seus11");

// builder.Services.AddSwaggerGen();

Console.WriteLine("seus12");

var app = builder.Build();
Console.WriteLine("seus13");

// Check if we should only run migrations and then exit
if (args.Contains("--migrate"))
{
    Console.WriteLine("seus14");

    using (var scope = app.Services.CreateScope())
    {
        Console.WriteLine("seus15");

        var services = scope.ServiceProvider;
        try
        {
            Console.WriteLine("seus16");

            var context = services.GetRequiredService<ApplicationDBContext>();
            Console.WriteLine("seus17");

            context.Database.Migrate();
            Console.WriteLine("seus18");

            Console.WriteLine("Database migration completed successfully.");
            Console.WriteLine("seus19");

        }
        catch (Exception ex)
        {
            Console.WriteLine("seus20");

            Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
            Console.WriteLine("seus21");

            // Exit with error code
            Environment.Exit(1);
            Console.WriteLine("seus22");

        }
    }
    Console.WriteLine("seus23");

    // Exit after migration
    Environment.Exit(0);
}

Console.WriteLine("seus24");

// Continue with normal application startup
app.UseCors("AllowReactApp");
Console.WriteLine("seus25");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("seus26");

    app.UseSwagger();
    Console.WriteLine("seus27");

    app.UseSwaggerUI();
    Console.WriteLine("seus28");

}

app.UseSession();
Console.WriteLine("seus");

app.UseHttpsRedirection();
Console.WriteLine("seus");

app.UseAuthorization();
Console.WriteLine("seus");

app.MapControllers();

// Always run migrations on application startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
    db.Database.Migrate();
}

app.Run();
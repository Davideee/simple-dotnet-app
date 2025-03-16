using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Repository;
using Model.Repository.Interfaces;
using Model.Services;
using Model.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

var connectionStringUrl = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresTestDbConnectionUrl");
Console.WriteLine($"connectionStringUrl: {connectionStringUrl}");
var connectionString = ConvertPostgresUrlToConnectionString(connectionStringUrl) ??
                       Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ??
                       builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<UserContext>(options => { options.UseNpgsql(connectionString); });

builder.Services.AddControllers();
using (var scope = builder.Services.BuildServiceProvider().CreateScope()) {
    var dbContext = scope.ServiceProvider.GetRequiredService<UserContext>();

    try {
        // Versuche, die Datenbank zu erreichen
        await dbContext.Database.CanConnectAsync();
        Console.WriteLine("Database connection successful");
    }
    catch (Exception ex) {
        // Fehlerbehandlung
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();

string? ConvertPostgresUrlToConnectionString(string? url) {
    if (string.IsNullOrEmpty(url)) {
        return null;
    }

    var uri = new Uri(url);
    var userInfo = uri.UserInfo.Split(':');

    return
        $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=True";
}
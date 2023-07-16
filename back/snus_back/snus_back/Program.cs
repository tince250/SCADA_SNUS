using Microsoft.EntityFrameworkCore;
using snus_back;
using snus_back.data_access;
using snus_back.Hubs;
using snus_back.Models;
using snus_back.Repositories;
using snus_back.Services;
using snus_back.Services.ServiceInterfaces;
using snus_back.WebSockets;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<SNUSDbContext>(options =>
    {
        options.UseLazyLoadingProxies().
        UseSqlite("Data Source = SnusDB.db");
    }, ServiceLifetime.Transient);

builder.Services.AddCors();

// Services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<IIOEntryService, IOEntryService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<SimulationDriver>();
builder.Services.AddTransient<ScanService>();

// Repositories
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<IOEntryRepository>();
builder.Services.AddTransient<TagRepository>();
builder.Services.AddTransient<AlarmRepository>();

builder.Services.AddSingleton<UpdateInputHandler>();
builder.Services.AddSingleton<UpdateAlarmHandler>();
builder.Services.AddSingleton<WebSocketConnectionManager>();

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

/*builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "mmm", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});*/
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapHub<UpdateInputHub>("/hub/updateInput");

app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(120),
});

app.MapRazorPages();

/*using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<ScanService>().Run();*/

app.Run();



/*using (var db = new SNUSDbContext())
{
    Console.WriteLine("Adding some authors into database...");
    User author1 = new User { Name = "Roberto", LastName = "Bolano" };
    db.Users.Add(author1);
    db.SaveChanges();
}*/

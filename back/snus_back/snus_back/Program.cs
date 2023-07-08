using snus_back.data_access;
using snus_back.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


using (var db = new SNUSDbContext())
{
    Console.WriteLine("Adding some authors into database...");
    User author1 = new User { Name = "Roberto", LastName = "Bolano" };
    db.Users.Add(author1);
    db.SaveChanges();
}
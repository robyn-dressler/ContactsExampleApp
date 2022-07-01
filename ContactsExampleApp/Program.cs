using ContactsExampleApp.Repositories;
using ContactsExampleApp.Repositories.Abstract;
using ContactsExampleApp.Services;
using ContactsExampleApp.Services.Abstract;
using DbUp;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMatBlazor();
builder.Services.AddControllers();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

//Add database context
string dbConnectionString = builder.Configuration.GetConnectionString("ContactsDB");
builder.Services.AddDbContext<ContactsDBContext>(options => options.UseSqlServer(dbConnectionString));

//Run DB scripts
var upgrader =
        DeployChanges.To
            .SqlDatabase(dbConnectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .WithTransactionPerScript()
            .LogToConsole()
            .Build();

var result = upgrader.PerformUpgrade();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.MapControllers();

app.Run();

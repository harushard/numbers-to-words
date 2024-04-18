using NumbersToWords;
using NumbersToWords.WebApi;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup();
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app);

await app.RunAsync();
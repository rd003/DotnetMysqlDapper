using DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddTransient<IPersonRepository, PersonRepository>();

var app = builder.Build();

// app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();

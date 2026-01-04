using ToDoListe.Api.Data;
using ToDoListe.Api.Dtos;
using ToDoListe.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddingCors(); 

builder.Services.AddValidation(); 
builder.AddTaskDb();

var app = builder.Build();
app.UseCors("FrontendPolicy");

app.MapTasksEndpoints(); 

app.MigrateDb(); 

app.Run();

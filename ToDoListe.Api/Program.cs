using ToDoListe.Api.Dtos;
using ToDoListe.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapTasksEndpoints(); 

app.Run();

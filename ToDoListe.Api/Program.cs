using ToDoListe.Api.Dtos;
using ToDoListe.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation(); 

var app = builder.Build();

app.MapTasksEndpoints(); 

app.Run();

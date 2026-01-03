using ToDoListe.Api.Data;
using ToDoListe.Api.Dtos;
using ToDoListe.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation(); 

//connection string
var connString = "Data Source= ToDoListe.db"; 
builder.Services.AddSqlite<TaskContext>(
   connString
); 

var app = builder.Build();

app.MapTasksEndpoints(); 

app.MigrateDb(); 

app.Run();

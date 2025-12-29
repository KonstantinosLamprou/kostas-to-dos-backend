using ToDoListe.Api.Dtos;

const string GetTaskEndpointName = "GetTask";

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<TaskDto> tasks = [
    new (
        1, 
        "Einkaufen",
        true,
        new DateOnly(2025, 12, 28)),
    new (
        2, 
        "Lernen",
        false,
        new DateOnly(2025, 12, 29)),
    new (
        3, 
        "Fußball",
        true,
        new DateOnly(2025, 12, 28)),
    new (
        4, 
        "Spielen",
        false,
        new DateOnly(2025, 12, 28))
    
];

// GET /tasks
app.MapGet("/tasks", () => tasks); //der zweite Teil ist der Handler 


// GET /tasks/id 
//hier findet ein Parameter binding durch .Net zwischen id im Pfad und dem Argument id aus der Lambda Funktion 
app.MapGet("/tasks/{id}", (int id) => 
{
    var task =  tasks.Find(task => task.Id == id); 
    //Wichtig: Wenn nach etwas angefragt wird was nicht existiert: 
    return task is not null ? Results.Ok(task) : Results.NotFound(); 

}).WithName(GetTaskEndpointName); 

// POST /tasks
app.MapPost("/tasks", (CreateTaskDto newTask) =>
{//Instanz der TaskDto -> diese wird auch dem CreatedAtRoute gegeben 
    TaskDto task = new (
        tasks.Count + 1,
        newTask.Title, 
        newTask.IsComplete,
        newTask.TaskDatum
    );
    tasks.Add(task); 

    return Results.CreatedAtRoute(GetTaskEndpointName, new {id = task.Id}, task); 
}); 
//CreatedAtRoute = wandle das Objekt in JSON um und 
//schicke es im Body der Antwort zurück an den Client

// PUT /tasks/1 
app.MapPut("/tasks/{id}", (int id, UpdateTaskDto updateTask) =>
{
    var index = tasks.FindIndex(task => task.Id == id); 

    tasks[index] = new TaskDto(
        id, 
        updateTask.Title, 
        updateTask.IsComplete, 
        updateTask.TaskDatum
    );

    return Results.NoContent(); 
}); 

// DELETE /tasks/id 

app.MapDelete("/tasks/{id}", (int id) =>
{
    tasks.RemoveAll(task => task.Id == id); 
    
    return Results.NoContent(); 
}); 

app.Run();

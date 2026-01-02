using ToDoListe.Api.Dtos;

namespace ToDoListe.Api.Endpoints;

//Klären was eine exstenion method ist
public static class TasksEndpoints
{
    const string GetTaskEndpointName = "GetTask";
    private static readonly List<TaskDto> tasks = [
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

    
    public static void MapTasksEndpoints(this WebApplication app)
    {
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
            //Minus 1 steht für, wenn keine results geupdated werden konnten 
            if (index == -1)
            {
                return Results.NotFound(); 
            }

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
    }
}

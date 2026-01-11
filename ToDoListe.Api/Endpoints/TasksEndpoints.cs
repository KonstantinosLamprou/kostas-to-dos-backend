using Microsoft.EntityFrameworkCore;
using ToDoListe.Api.Data;
using ToDoListe.Api.Dtos;
using ToDoListe.Api.Models;

namespace ToDoListe.Api.Endpoints;

//Klären was eine exstenion method ist
public static class TasksEndpoints
{
    const string GetTaskEndpointName = "GetTask";

    //Wir erweitern die Webapplication Klasse mit dieser Hilfsklasse und packen dort die Endpunkte rein 
    //-> this ist die Markierung/Wichtig! 
    public static void MapTasksEndpoints(this WebApplication app)
    {   
        //DRY 
        var group = app.MapGroup("/tasks"); 

        group.MapGet("/", async(TaskContext dbContext) 
            => await dbContext.ToDoTasks
                                    .Select(task => new TaskSummaryDto(
                                        task.Id,
                                        task.Title, 
                                        task.IsComplete,
                                        task.TaskDatum
                                    )
                                )
                                .AsNoTracking()
                                .ToListAsync()
                            );

    
        // GET /tasks/id 
        //Wichtig: Diesen Endpunkt nutze ich um? Die Aufgaben zu laden wenn ich die Anwendung komplett lösche
        //hier findet ein Parameter binding durch .Net zwischen id im Pfad und dem Argument id aus der Lambda Funktion 
        group.MapGet("/{id}", async (int id, TaskContext dbContext) =>
        {
            var task = await dbContext.ToDoTasks.FindAsync(id);
            //Wichtig: Wenn nach etwas angefragt wird was nicht existiert: 
            return task is null ? Results.NotFound() : Results.Ok(
                new TaskSummaryDto(
                    task.Id,
                    task.Title,
                    task.IsComplete,
                    task.TaskDatum
                )
            );

        }).WithName(GetTaskEndpointName); 
        
        // POST /tasks
        group.MapPost("/", async (CreateTaskDto newTask, TaskContext dbContext) =>
        {
            //Hier wird die Task genommen und als Instanz erstellt 
            
            ToDoTask task = new()
            {
               Title = newTask.Title, 
               IsComplete = newTask.IsComplete, 
               TaskDatum = newTask.TaskDatum 
            }; 

            //mit diesem Befehl fügen wir noch nicht ganz die Task in die Database, sondern legen
            //fest -> diese Task sollte in die Database angelegt werden 
            dbContext.ToDoTasks.Add(task); 
            //hier wird nun die Task gesafed und in die Datenbank eingefügt 
            await dbContext.SaveChangesAsync(); 

            TaskSummaryDto taskDto = new(
                task.Id, 
                task.Title,
                task.IsComplete,
                task.TaskDatum
            );

            return Results.CreatedAtRoute(GetTaskEndpointName, new {id = taskDto.Id}, taskDto); 
        }); 
        //CreatedAtRoute = wandle das Objekt in JSON um und 
        //schicke es im Body der Antwort zurück an den Client

        // PUT /tasks/1 
        group.MapPut("/{id}", async(int id, UpdateTaskDto updateTask, TaskContext dbContext) =>
        {
            var existingTask = await dbContext.ToDoTasks.FindAsync(id);

            if(existingTask is null)
            {
                return Results.NotFound(); 
            }

            existingTask.Title = updateTask.Title;
            existingTask.IsComplete = updateTask.IsComplete;
            existingTask.TaskDatum = updateTask.TaskDatum; 

            await dbContext.SaveChangesAsync(); 

            return Results.NoContent(); 
        }); 

        // DELETE /tasks/id 
        group.MapDelete("/{id}", async(int id, TaskContext dbContext) =>
        {
            //bulk deletion
            await dbContext.ToDoTasks
                                .Where(task => id == task.Id)
                                .ExecuteDeleteAsync(); 

            return Results.NoContent(); 
        }); 
    }
}

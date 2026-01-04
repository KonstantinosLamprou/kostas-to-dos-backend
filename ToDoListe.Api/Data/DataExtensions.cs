using Microsoft.EntityFrameworkCore;

namespace ToDoListe.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope(); 
        var dbContext = scope.ServiceProvider
                            .GetRequiredService<TaskContext>(); 
        dbContext.Database.Migrate(); 
    }

    public static void AddTaskDb(this WebApplicationBuilder builder)
    {
        //connection string
        var connString = builder.Configuration.GetConnectionString("ToDoListe");
        //Der Db Context sollte nur ein Scoped Service sein
        //1. Für jeden request wird einer neue Instanz für den Context erstellt 
        //2. db verbindungen sind eine limitierte und teure resource 
        //3. es ist nicht thread safe -> Scoped geht zukünftigen issues mit concurrency aus dem weg 
        //4. kleinlebige instanzen -> Optimisation des Speicherverbrauchs 
        builder.Services.AddScoped<TaskContext>(); 
        //registrieren des contexts für den service container 
        builder.Services.AddSqlite<TaskContext>(connString); 

    }

    public static void AddingCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>

            options.AddPolicy("FrontendPolicy",
                policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                })
        );

    }
}

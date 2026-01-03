using Microsoft.EntityFrameworkCore;
using ToDoListe.Api.Models;

namespace ToDoListe.Api.Data;

public class TaskContext(DbContextOptions<TaskContext> options) 
    : DbContext(options)
{
    public DbSet<ToDoTask> ToDoTasks => Set<ToDoTask>();

}

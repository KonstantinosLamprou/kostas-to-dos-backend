using ToDoListe.Api.Dtos;

namespace ToDoListe.Api.Models;

//accessor get 
//mutator set
//Es sind auto-completed properties  
//ASP.NET Core nutzt die Setter (set) der Klasse, um die Daten aus dem JSON als C#-Objekt zu füllen.
//Wenn man die Daten später wieder als Antwort zurückschickt, nutzt ASP.NET Core die Getter (get), um das JSON zu erzeugen.
public class ToDoTask
{
    public required int Id { get; set; }
    public required string Title { get; set; }

    public bool IsComplete { get; set; }

    public DateOnly TaskDatum { get; set; }

} 

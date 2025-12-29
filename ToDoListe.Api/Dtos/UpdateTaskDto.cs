namespace ToDoListe.Api.Dtos;

public record UpdateTaskDto(
    string Title, 
    bool IsComplete, 
    DateOnly TaskDatum
);

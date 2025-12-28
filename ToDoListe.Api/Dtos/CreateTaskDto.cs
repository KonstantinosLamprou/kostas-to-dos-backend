namespace ToDoListe.Api.Dtos;

public record CreateTaskDto(
    string Title, 
    bool IsComplete, 
    DateOnly TaskDatum
); 

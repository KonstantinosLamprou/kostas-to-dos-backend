using System.ComponentModel.DataAnnotations;

namespace ToDoListe.Api.Dtos;

public record CreateTaskDto(
    [Required][StringLength(30)] string Title, 
    bool IsComplete, 
    DateOnly TaskDatum
); 

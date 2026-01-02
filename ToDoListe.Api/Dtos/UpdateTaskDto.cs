using System.ComponentModel.DataAnnotations;

namespace ToDoListe.Api.Dtos;

public record UpdateTaskDto(
    [Required][StringLength(30)]string Title, 
    bool IsComplete, 
    DateOnly TaskDatum
);

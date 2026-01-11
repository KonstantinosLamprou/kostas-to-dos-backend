using System.ComponentModel.DataAnnotations;

namespace ToDoListe.Api.Dtos;

//Ein DTO ist wie ein Vertrag zwischen dem Client und einem Server, da es ein gemeinsames Abkommen 
//zueinander teilt, wie es Daten transferiert und handled.  

public record TaskSummaryDto(
    [Required] int Id,
    [Required][StringLength(30)]string Title, 
    bool IsComplete, 
    DateOnly TaskDatum 
);

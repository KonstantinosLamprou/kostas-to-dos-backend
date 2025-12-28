namespace ToDoListe.Api.Dtos;

//Ein DTO ist wie ein Vertrag zwischen dem Client und einem Server, da es ein gemeinsames Abkommen 
//zueinander teilt, wie es Daten transferiert und handled.  

public record TaskDto(
    int Id, 
    string Title, 
    bool IsComplete, 
    DateOnly TaskDatum 
);

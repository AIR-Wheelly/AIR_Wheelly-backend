namespace AIR_Wheelly_Common.Models;

public class Manafacturer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Model> Models { get; set; }
    
}
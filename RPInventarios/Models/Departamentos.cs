using System.ComponentModel.DataAnnotations;

namespace RPInventarios.Models;
public class Departamento
{
    public int Id { get; set; }
    [Display(Name = "Departamento")]
    public string Nombre { get; set; }
}

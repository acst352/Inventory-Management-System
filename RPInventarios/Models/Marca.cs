namespace RPInventarios.Models;
public class Marca
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public virtual ICollection<Producto> Productos { get; set; }
    // Se usa ICollection<Producto> para representar la relación 1:N entre
    // Marca y Producto.
}
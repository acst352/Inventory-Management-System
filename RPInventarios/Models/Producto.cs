using System.ComponentModel.DataAnnotations;
namespace RPInventarios.Models;

public enum EstatusProducto
{
    Inactivo = 0,
    Activo = 1,
    [Display(Name = "En proceso de Activación")]
    EnProcesoActivacion = 2
}

public class Producto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
    [MinLength(1, ErrorMessage = "El nombre del producto debe contener al menos 1 caracter.")]
    [MaxLength(50, ErrorMessage = "El nombre del producto no puede contener más de 50 caracteres.")]
    public string Nombre { get; set; } = string.Empty;
    [Display(Name = "Descripción")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "La descripción del producto debe contener entre 1 y 200 caracteres.")]
    public string Descripcion { get; set; } = string.Empty;
    [Display(Name = "Marca")]
    [Required(ErrorMessage = "La marca del producto es obligatoria.")]
    public int MarcaId { get; set; }
    public virtual Marca? Marca { get; set; }
    [Required(ErrorMessage = "El costo del producto es obligatorio.")]
    [Range(1, 9999999999, ErrorMessage = "El costo debe estar entre 1 y 9.999.999.999.")]
    [RegularExpression(@"^\d{1,10}(\.\d{1,2})?$", ErrorMessage = "El costo debe ser un número positivo, permitiendo hasta dos decimales y sin letras.")]
    public decimal Costo { get; set; }
    public EstatusProducto Estatus { get; set; } = EstatusProducto.Activo;
}
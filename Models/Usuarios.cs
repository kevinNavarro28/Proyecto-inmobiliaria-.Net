using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inmobiliaria.Models;



public enum enRoles{
    Empleado = 1,
    Administrador = 2,
}

public class Usuarios
{
    [Required]
    [Display(Name ="Codigo")]
    public int Id { get; set; }
    [Required]
    public String? Nombre { get ; set;}
    [Required]
    public String? Apellido { get ; set;}
    [Required, EmailAddress]
    public String? Email { get ; set;}
    [Required, DataType(DataType.Password)]
    public String? Clave { get ; set;}

    [Display(Name ="Foto")]
    public String? Avatarruta {get;set;}
    
    [NotMapped]
    public IFormFile? AvatarFile{get;set;} 

    public int Rol { get ; set;}


    [NotMapped]
    public string RolNombre => Rol > 0 ? ((enRoles)Rol).ToString() : "";


    
     public static IDictionary<int, string> ObtenerRoles()
		{
			SortedDictionary<int, string> roles = new SortedDictionary<int, string>();
			Type tipoEnumRol = typeof(enRoles);
			foreach (var valor in Enum.GetValues(tipoEnumRol))
			{
				roles.Add((int)valor, Enum.GetName(tipoEnumRol, valor));
			}
			return roles;
		}

}
 

    


    


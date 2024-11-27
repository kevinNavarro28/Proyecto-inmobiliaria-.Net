namespace Inmobiliaria.Models;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Propietarios
{
    public int Id { get; set; }
    public String ? Nombre { get ; set;}
    public String ? Apellido { get ; set;}
    public long Dni { get ; set;}
    [Display(Name="Teléfono")]
    public long Telefono { get ; set;}
    public String ? Email { get ; set;}
    [Display(Name="Dirección")]
    public String ? Direccion { get ; set;}
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Nacimiento { get ; set;}


    public Propietarios(){
     


    }
}

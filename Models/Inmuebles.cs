
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inmobiliaria.Models;



public class Inmuebles
{   
    [Display(Name="Codigo")]
    public int Id { get; set; }

    public String ? Tipo { get ; set;}

    [Required][Display (Name ="Dirección")]
    public String ? Direccion { get ; set;}
    public String ? Uso { get ; set;}

    public Double ? Precio {get ; set;}
    [Display(Name="Cantidad De Ambientes")]
    public int Cantidad_Ambientes { get ; set;}

    [Required]
    public int Superficie { get ; set;}

    [Required]
    public Double Latitud { get ; set;}
    public Double Longitud { get ; set;}

    [Display(Name="Dueño")]

    public int PropietarioId{get ; set;}
    [ForeignKey(nameof(Id))]

    public Propietarios? propietario {get;set;}
    

    public Inmuebles(){
     


    }
}

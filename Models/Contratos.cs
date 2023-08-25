using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inmobiliaria.Models;

public class Contratos

{   
    [Display(Name ="Numero de Contrato")]
    public int Id {get ; set;}
    public DateTime Fecha_Inicio {get ; set;}

    public DateTime Fecha_Fin {get ; set;}

    public Double Monto {get ; set;}

    [Display(Name="Inmueble")]

    public int InmuebleId{get ; set;}
    [ForeignKey(nameof(Id))]

    public Inmuebles inmueble {get;set;}

    [Display(Name="Inquilino")]

    public int InquilinoId{get ; set;}
    [ForeignKey(nameof(Id))]

    public Inquilinos inquilino {get;set;}



    public Contratos(){
     


    }
}

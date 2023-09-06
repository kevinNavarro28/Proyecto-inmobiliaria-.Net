using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Inmobiliaria.Models;

public class Pagos
{
    public int Id { get; set; }
    public DateTime Fecha_Pago { get ; set;}
    public Double Importe { get ; set;}
    
    [Display(Name="Contrato")]

    public int ContratoId{get ; set;}
    [ForeignKey(nameof(Id))]

    public Contratos Contrato {get;set;}



    public Pagos(){
     


    }
}

using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioPago
{

    string connectionString = "Server=127.0.0.1;port=3306;User=root;Password=;Database=inmobiliarianavarro;SslMode=none"; 

public RepositorioPago()
{

}

public List<Pagos>GetPagos()
{
    List<Pagos> pagos = new List<Pagos>(); 
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT Id,Fecha_Pago,Importe,ContratoId FROM pagos p";
        using(var command = new MySqlCommand(query , connection)){
        connection.Open();
        using (var reader = command.ExecuteReader()){
            while(reader.Read())
            {
                Pagos pago = new Pagos
                { 
                Id = reader.GetInt32(nameof(Pagos.Id)),
                Fecha_Pago = reader.GetDateTime(nameof(Pagos.Fecha_Pago)),
                Importe = reader.GetInt32(nameof(Pagos.Importe)),
                ContratoId = reader.GetInt32(nameof(Pagos.ContratoId)),
                Contrato = new Contratos{
                    Id = reader.GetInt32(nameof(Contratos.Id)),
                }
               

                };
                pagos.Add(pago);

            }
        }

     }
     connection.Close();
    } 
    return pagos;  

}
public Pagos ObtenerPago(int Id)
{
    Pagos res = null;
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT Id,Fecha_Pago,Importe,ContratoId 
        FROM pagos
        WHERE Id = @Id";
        using(var command = new MySqlCommand(query , connection)){
            command.Parameters.AddWithValue("@Id", Id);
        connection.Open();
        using (var reader = command.ExecuteReader()){
            if(reader.Read())
            {
                res = new Pagos
                { 
                Id = reader.GetInt32(nameof(Pagos.Id)),
                Fecha_Pago = reader.GetDateTime(nameof(Pagos.Fecha_Pago)),
                Importe = reader.GetInt32(nameof(Pagos.Importe)),
                ContratoId = reader.GetInt32(nameof(Pagos.ContratoId)),

                };
              

            }
        }

     }
     connection.Close();
    } 
    return res;  

}

public int Alta(Pagos pagos){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"INSERT INTO pagos (Fecha_Pago,Importe,ContratoId)
    VALUES (@fecha_pago,@importe,@ContratoId);
    SELECT LAST_INSERT_ID();";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
        
        command.Parameters.AddWithValue("@fecha_pago",pagos.Fecha_Pago);
        command.Parameters.AddWithValue("@importe",pagos.Importe);
        command.Parameters.AddWithValue("@ContratoId",pagos.ContratoId);
      
        connection.Open();
        res = Convert.ToInt32(command.ExecuteScalar());
        connection.Close();

    }

}
return res;

}

public int Modificar(Pagos pago){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"UPDATE pagos SET 
    fecha_pago=@fecha_pago,
    importe=@importe,
    contratoid=@contratoid
    WHERE Id = @id; ";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
       
        command.Parameters.AddWithValue("@fecha_pago",pago.Fecha_Pago);
        command.Parameters.AddWithValue("@importe",pago.Importe);
        command.Parameters.AddWithValue("@contratoid",pago.ContratoId);
        command.Parameters.AddWithValue("@id",pago.Id);
   
        connection.Open();
        res = command.ExecuteNonQuery();
        connection.Close();

    }

}
return res;

}
public int Borrar(int Id){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"DELETE FROM pagos WHERE id = @id;";

    using(MySqlCommand command = new MySqlCommand (query,connection)){
       command.Parameters.AddWithValue("@id",Id);
        connection.Open();
        res = command.ExecuteNonQuery();
        connection.Close();

    }

}
return res;
 }


    


}

using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioContrato
{

    string connectionString = "Server=127.0.0.1;port=3306;User=root;Password=;Database=inmobiliarianavarro;SslMode=none"; 

public RepositorioContrato()
{

}

public List<Contratos>GetContratos()
{
    List<Contratos> contratos = new List<Contratos>(); 
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT c.Id,c.Fecha_Inicio,c.Fecha_Fin,c.Monto,c.InquilinoId,c.InmuebleId,p.Nombre,p.Apellido,i.Tipo,i.Direccion 
        FROM contratos c INNER JOIN inmuebles i ON c.InmuebleId = i.Id
        JOIN inquilinos p ON c.InquilinoId = p.Id;";
        using(var command = new MySqlCommand(query , connection)){
        connection.Open();
        using (var reader = command.ExecuteReader()){
            while(reader.Read())
            {
                Contratos contrato = new Contratos
                { 
                Id = reader.GetInt32(nameof(Contratos.Id)),
                Fecha_Inicio = reader.GetDateTime(nameof(Contratos.Fecha_Inicio)),
                Fecha_Fin = reader.GetDateTime(nameof(Contratos.Fecha_Fin)),
                Monto = reader.GetInt64(nameof(Contratos.Monto)),
                InmuebleId = reader.GetInt32(nameof(Contratos.InmuebleId)),
                inmueble = new Inmuebles{
                    Id = reader.GetInt32(nameof(Inmuebles.Id)),
                    Tipo = reader.GetString(nameof(Inmuebles.Tipo)),
                    Direccion = reader.GetString(nameof(Inmuebles.Direccion))
                },


                InquilinoId = reader.GetInt32(nameof(Contratos.InquilinoId)),
                inquilino = new Inquilinos{
                    Id = reader.GetInt32(nameof(Inquilinos.Id)),
                    Nombre = reader.GetString(nameof(Inquilinos.Nombre)),
                    Apellido = reader.GetString(nameof(Inquilinos.Apellido))
                }

               
               

                };
                contratos.Add(contrato);

            }
        }

     }
     connection.Close();
    } 
    return contratos;  

}
public Contratos ObtenerContrato(int id)
{
     Contratos res = null;
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT Id,Fecha_Inicio,Fecha_Fin,Monto,InmuebleId,InquilinoId 
        FROM contratos 
        WHERE Id = @Id";
        using(var command = new MySqlCommand(query , connection)){
            command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using (var reader = command.ExecuteReader()){
            if(reader.Read())
            {
                res = new  Contratos
                { 
                Id = reader.GetInt32(nameof( Contratos.Id)),
                Fecha_Inicio = reader.GetDateTime(nameof( Contratos.Fecha_Inicio)),
                Fecha_Fin = reader.GetDateTime(nameof( Contratos.Fecha_Fin)),
                Monto = reader.GetInt32(nameof( Contratos.Monto)),
                InmuebleId = reader.GetInt32(nameof( Contratos.InmuebleId)),
                InquilinoId = reader.GetInt32(nameof( Contratos.InquilinoId)),

                };
              

            }
        }

     }
     connection.Close();
    } 
    return res;  

}

public int Alta(Contratos contratos){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"INSERT INTO contratos (Id,Fecha_Inicio,Fecha_Fin,Monto,InmuebleId,InquilinoId)
    VALUES (@id,@fecha_inicio,@fecha_fin,@monto,@InmuebleId,@InquilinoId);
    SELECT LAST_INSERT_ID();";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
        command.Parameters.AddWithValue("@id",contratos.Id);
        command.Parameters.AddWithValue("@fecha_inicio",contratos.Fecha_Inicio);
        command.Parameters.AddWithValue("@fecha_fin",contratos.Fecha_Fin);
        command.Parameters.AddWithValue("@monto",contratos.Monto);
        command.Parameters.AddWithValue("@InmuebleId",contratos.InmuebleId);
        command.Parameters.AddWithValue("@InquilinoId",contratos.InquilinoId);
       
        connection.Open();
        res = Convert.ToInt32(command.ExecuteScalar());
        connection.Close();

    }

}
return res;

}

public int Modificar(Contratos contratos){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"UPDATE contratos SET 
    fecha_inicio= @fecha_inicio,
    fecha_fin = @fecha_fin,
    monto = @monto,
    inmuebleid = @inmuebleid,
    inquilinoid = @inquilinoid
    WHERE Id = @id; ";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
        command.Parameters.AddWithValue("@fecha_inicio",contratos.Fecha_Inicio);
        command.Parameters.AddWithValue("@fecha_fin",contratos.Fecha_Fin);
        command.Parameters.AddWithValue("@monto",contratos.Monto);
        command.Parameters.AddWithValue("@inmuebleid",contratos.InmuebleId);
        command.Parameters.AddWithValue("@inquilinoid",contratos.InquilinoId);
        command.Parameters.AddWithValue("@id",contratos.Id);
       
        connection.Open();
        res = command.ExecuteNonQuery();
        connection.Close();

    }

}
return res;

}
public int Borrar(int id){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"DELETE FROM contratos WHERE id = @id;";

    using(MySqlCommand command = new MySqlCommand (query,connection)){
       command.Parameters.AddWithValue("@id",id);
        connection.Open();
        res = command.ExecuteNonQuery();
        connection.Close();

    }

}
return res;
 }


    


}

using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioPropietario
{

    string connectionString = "Server=127.0.0.1;port=3306;User=root;Password=;Database=inmobiliarianavarro;SslMode=none"; 

public RepositorioPropietario()
{

}

public List<Propietarios>GetPropietarios()
{
    List<Propietarios> propietarios = new List<Propietarios>(); 
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT Id,Nombre,Apellido,Dni,Telefono,Email,Direccion,Nacimiento FROM propietarios ";
        using(var command = new MySqlCommand(query , connection)){
        connection.Open();
        using (var reader = command.ExecuteReader()){
            while(reader.Read())
            {
                Propietarios propietario = new Propietarios
                { 
                Id = reader.GetInt32(nameof(Propietarios.Id)),
                Nombre = reader.GetString(nameof(Propietarios.Nombre)),
                Apellido = reader.GetString(nameof(Propietarios.Apellido)),
                Dni = reader.GetInt64(nameof(Propietarios.Dni)),
                Telefono = reader.GetInt64(nameof(Propietarios.Telefono)),
                Email = reader.GetString(nameof(Propietarios.Email)),
                Direccion = reader.GetString(nameof(Propietarios.Direccion)), 
                Nacimiento = reader.GetDateTime(nameof(Propietarios.Nacimiento))
                

                };
                propietarios.Add(propietario);

            }
        }

     }
     connection.Close();
    } 
    return propietarios;  

}
public Propietarios ObtenerPropietario(int id)
{
    Propietarios res = null;
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT Id,Nombre,Apellido,Dni,Telefono,Email,Direccion,Nacimiento 
        FROM propietarios 
        WHERE Id = @Id";
        using(var command = new MySqlCommand(query , connection)){
            command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using (var reader = command.ExecuteReader()){
            if(reader.Read())
            {
                res = new Propietarios
                { 
                Id = reader.GetInt32(nameof(Propietarios.Id)),
                Nombre = reader.GetString(nameof(Propietarios.Nombre)),
                Apellido = reader.GetString(nameof(Propietarios.Apellido)),
                Dni = reader.GetInt64(nameof(Propietarios.Dni)),
                Telefono = reader.GetInt64(nameof(Propietarios.Telefono)),
                Email = reader.GetString(nameof(Propietarios.Email)),
                Direccion = reader.GetString(nameof(Propietarios.Direccion)), 
                Nacimiento = reader.GetDateTime(nameof(Propietarios.Nacimiento))
                

                };
              

            }
        }

     }
     connection.Close();
    } 
    return res;  

}

public int Alta(Propietarios propietario){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"INSERT INTO propietarios ( nombre,apellido,dni,telefono,email,direccion,nacimiento)
    VALUES (@nombre,@apellido,@dni,@telefono,@email,@direccion,@nacimiento);
    SELECT LAST_INSERT_ID();";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
        command.Parameters.AddWithValue("@nombre",propietario.Nombre);
        command.Parameters.AddWithValue("@apellido",propietario.Apellido);
        command.Parameters.AddWithValue("@dni",propietario.Dni);
        command.Parameters.AddWithValue("@telefono",propietario.Telefono);
        command.Parameters.AddWithValue("@email",propietario.Email);
        command.Parameters.AddWithValue("@direccion",propietario.Direccion);
        command.Parameters.AddWithValue("@nacimiento",propietario.Nacimiento);
        connection.Open();
        res = Convert.ToInt32(command.ExecuteScalar());
        connection.Close();

    }

}
return res;

}

public int Modificar(Propietarios propietario){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"UPDATE propietarios SET 
    nombre= @nombre,
    apellido = @apellido,
    dni = @dni,
    telefono = @telefono,
    email = @email,
    direccion = @direccion,
    nacimiento = @nacimiento
    WHERE Id = @id;";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
        command.Parameters.AddWithValue("@id",propietario.Id);
        command.Parameters.AddWithValue("@nombre",propietario.Nombre);
        command.Parameters.AddWithValue("@apellido",propietario.Apellido);
        command.Parameters.AddWithValue("@dni",propietario.Dni);
        command.Parameters.AddWithValue("@telefono",propietario.Telefono);
        command.Parameters.AddWithValue("@email",propietario.Email);
        command.Parameters.AddWithValue("@direccion",propietario.Direccion);
        command.Parameters.AddWithValue("@nacimiento",propietario.Nacimiento);
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
    string query = @"DELETE FROM propietarios WHERE Id = @id;";

    using(MySqlCommand command = new MySqlCommand (query,connection)){
       command.Parameters.AddWithValue("@Id",id);
        connection.Open();
        res = command.ExecuteNonQuery();
        connection.Close();

    }

}
return res;
 }


    


}

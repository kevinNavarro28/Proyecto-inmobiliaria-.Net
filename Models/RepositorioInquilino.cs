using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioInquilino
{

    string connectionString = "Server=127.0.0.1;port=3306;User=root;Password=;Database=inmobiliarianavarro;SslMode=none"; 

public RepositorioInquilino()
{

}

public List<Inquilinos>GetInquilinos()
{
    List<Inquilinos> inquilinos = new List<Inquilinos>(); 
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT Id,Nombre,Apellido,Dni,Telefono,Email,Direccion,Nacimiento FROM inquilinos ";
        using(var command = new MySqlCommand(query , connection)){
        connection.Open();
        using (var reader = command.ExecuteReader()){
            while(reader.Read())
            {
                Inquilinos inquilino = new Inquilinos
                { 
                Id = reader.GetInt32(nameof(Inquilinos.Id)),
                Nombre = reader.GetString(nameof(Inquilinos.Nombre)),
                Apellido = reader.GetString(nameof(Inquilinos.Apellido)),
                Dni = reader.GetInt64(nameof(Inquilinos.Dni)),
                Telefono = reader.GetInt64(nameof(Inquilinos.Telefono)),
                Email = reader.GetString(nameof(Inquilinos.Email)),
                Direccion = reader.GetString(nameof(Inquilinos.Direccion)), 
                Nacimiento = reader.GetDateTime(nameof(Inquilinos.Nacimiento))
                

                };
                inquilinos.Add(inquilino);

            }
        }

     }
     connection.Close();
    } 
    return inquilinos;  

}
public Inquilinos ObtenerInquilino(int id)
{
    Inquilinos res = null;
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT Id,Nombre,Apellido,Dni,Telefono,Email,Direccion,Nacimiento 
        FROM inquilinos 
        WHERE Id = @Id";
        using(var command = new MySqlCommand(query , connection)){
            command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using (var reader = command.ExecuteReader()){
            if(reader.Read())
            {
                res = new Inquilinos
                { 
                Id = reader.GetInt32(nameof(Inquilinos.Id)),
                Nombre = reader.GetString(nameof(Inquilinos.Nombre)),
                Apellido = reader.GetString(nameof(Inquilinos.Apellido)),
                Dni = reader.GetInt64(nameof(Inquilinos.Dni)),
                Telefono = reader.GetInt64(nameof(Inquilinos.Telefono)),
                Email = reader.GetString(nameof(Inquilinos.Email)),
                Direccion = reader.GetString(nameof(Inquilinos.Direccion)), 
                Nacimiento = reader.GetDateTime(nameof(Inquilinos.Nacimiento))
                

                };
              

            }
        }

     }
     connection.Close();
    } 
    return res;  

}

public int Alta(Inquilinos inquilino){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"INSERT INTO inquilinos ( nombre,apellido,dni,telefono,email,direccion,nacimiento)
    VALUES (@nombre,@apellido,@dni,@telefono,@email,@direccion,@nacimiento);
    SELECT LAST_INSERT_ID();";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
        command.Parameters.AddWithValue("@nombre",inquilino.Nombre);
        command.Parameters.AddWithValue("@apellido",inquilino.Apellido);
        command.Parameters.AddWithValue("@dni",inquilino.Dni);
        command.Parameters.AddWithValue("@telefono",inquilino.Telefono);
        command.Parameters.AddWithValue("@email",inquilino.Email);
        command.Parameters.AddWithValue("@direccion",inquilino.Direccion);
        command.Parameters.AddWithValue("@nacimiento",inquilino.Nacimiento);
        connection.Open();
        res = Convert.ToInt32(command.ExecuteScalar());
        connection.Close();

    }

}
return res;

}

public int Modificar(Inquilinos inquilino){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"UPDATE inquilinos SET 
    nombre= @nombre,
    apellido = @apellido,
    dni = @dni,
    telefono = @telefono,
    email = @email,
    direccion = @direccion,
    nacimiento = @nacimiento
    WHERE Id = @id; ";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
        command.Parameters.AddWithValue("@id",inquilino.Id);
        command.Parameters.AddWithValue("@nombre",inquilino.Nombre);
        command.Parameters.AddWithValue("@apellido",inquilino.Apellido);
        command.Parameters.AddWithValue("@dni",inquilino.Dni);
        command.Parameters.AddWithValue("@telefono",inquilino.Telefono);
        command.Parameters.AddWithValue("@email",inquilino.Email);
        command.Parameters.AddWithValue("@direccion",inquilino.Direccion);
        command.Parameters.AddWithValue("@nacimiento",inquilino.Nacimiento);
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
    string query = @"DELETE FROM inquilinos WHERE id = @id;";

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

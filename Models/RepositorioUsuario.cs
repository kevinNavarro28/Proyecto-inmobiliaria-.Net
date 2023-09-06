using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioUsuario
{

    string connectionString = "Server=127.0.0.1;port=3306;User=root;Password=;Database=inmobiliarianavarro;SslMode=none"; 

public RepositorioUsuario()
{

}

public List<Usuarios>GetUsuarios()
{
    List<Usuarios> usuarios = new List<Usuarios>(); 
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT Id,Nombre,Apellido,Email,Clave,Avatarruta,Rol FROM usuarios ";
        using(var command = new MySqlCommand(query , connection)){
        connection.Open();
        using (var reader = command.ExecuteReader()){
            while(reader.Read())
            {
                Usuarios usuario = new Usuarios
                { 
                Id = reader.GetInt32(nameof(Usuarios.Id)),
                Nombre = reader.GetString(nameof(Usuarios.Nombre)),
                Apellido = reader.GetString(nameof(Usuarios.Apellido)),
                Email = reader.GetString(nameof(Usuarios.Email)),
                Clave = reader.GetString(nameof(Usuarios.Clave)),
                Avatarruta = reader.GetString(nameof(Usuarios.Avatarruta)),
                Rol = reader.GetInt32(nameof(Usuarios.Rol)),
               
                
              
                

                };
                usuarios.Add(usuario);

            }
        }

     }
     connection.Close();
    } 
    return usuarios;  

}
public Usuarios ObtenerUsuario(int id)
{
    Usuarios res = null;
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT Id,Nombre,Apellido,Email,Clave,Avatarruta,Rol FROM usuarios
        WHERE Id = @Id";
        using(var command = new MySqlCommand(query , connection)){
            command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using (var reader = command.ExecuteReader()){
            if(reader.Read())
            {
                res = new Usuarios
                { 
                Id = reader.GetInt32(nameof(Usuarios.Id)),
                Nombre = reader.GetString(nameof(Usuarios.Nombre)),
                Apellido = reader.GetString(nameof(Usuarios.Apellido)),
                Email = reader.GetString(nameof(Usuarios.Email)),
                Clave = reader.GetString(nameof(Usuarios.Clave)),
                Avatarruta = reader.GetString(nameof(Usuarios.Avatarruta)),
                Rol = reader.GetInt32(nameof(Usuarios.Rol)),

                };
              

            }
        }

     }
     connection.Close();
    } 
    return res;  

}
public Usuarios ObtenerUsuarioPorEmail(string Email)
{
    Usuarios res = null;
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT Id,Nombre,Apellido,Email,Clave,Avatarruta,Rol FROM usuarios
        WHERE Email = @Email";
        using(var command = new MySqlCommand(query , connection)){
            command.Parameters.AddWithValue("@Email", Email);
        connection.Open();
        using (var reader = command.ExecuteReader()){
            if(reader.Read())
            {
                res = new Usuarios
                { 
                Id = reader.GetInt32(nameof(Usuarios.Id)),
                Nombre = reader.GetString(nameof(Usuarios.Nombre)),
                Apellido = reader.GetString(nameof(Usuarios.Apellido)),
                Email = reader.GetString(nameof(Usuarios.Email)),
                Clave = reader.GetString(nameof(Usuarios.Clave)),
                Avatarruta = reader.GetString(nameof(Usuarios.Avatarruta)),
                Rol = reader.GetInt32(nameof(Usuarios.Rol)),

                };
              

            }
        }

     }
     connection.Close();
    } 
    return res;  

}




public int Alta(Usuarios usuarios){
int res = -1;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"INSERT INTO usuarios (Nombre,Apellido,Email,Clave,Avatarruta,Rol)
    VALUES (@nombre,@apellido,@email,@clave,@avatarruta,@rol);
    SELECT LAST_INSERT_ID();";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
        command.Parameters.AddWithValue("@nombre",usuarios.Nombre);
        command.Parameters.AddWithValue("@apellido",usuarios.Apellido);
        command.Parameters.AddWithValue("@email",usuarios.Email);
        command.Parameters.AddWithValue("@clave",usuarios.Clave);
        if(String.IsNullOrEmpty(usuarios.Avatarruta))
                {
                    command.Parameters.AddWithValue("@avatarruta","");
                }else{
                    command.Parameters.AddWithValue("@avatarruta",usuarios.Avatarruta);
                }
        
        command.Parameters.AddWithValue("@rol",usuarios.Rol);
        
        connection.Open();
        res = Convert.ToInt32(command.ExecuteScalar());
        usuarios.Id = res;
        connection.Close();

    }

}
return res;

}

public int Modificar(Usuarios usuarios){
int res = -1;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"UPDATE usuarios SET 
    nombre= @nombre,
    apellido = @apellido,
    email = @email
    WHERE Id = @id;";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
       
        command.Parameters.AddWithValue("@nombre",usuarios.Nombre);
        command.Parameters.AddWithValue("@apellido",usuarios.Apellido);
        command.Parameters.AddWithValue("@email",usuarios.Email);
        command.Parameters.AddWithValue("@id", usuarios.Id);
        connection.Open();
        res = command.ExecuteNonQuery();
        connection.Close();

    }

}
return res;

}
public int ModificarFoto(int id,string avatarruta){
    int res = -1;
    using(MySqlConnection connection = new MySqlConnection(connectionString)){
        string query = @"UPDATE usuarios SET
        avatarruta = @avatarruta
        WHERE Id = @Id;";
    using(MySqlCommand command = new MySqlCommand(query,connection)){
        command.Parameters.AddWithValue("@avatarruta",avatarruta);
        command.Parameters.AddWithValue("@id",id);
        connection.Open();
        res = command.ExecuteNonQuery();
		connection.Close();

    }
    }
    return res;
}

public int ModificarP(Usuarios usuarios){
int res = -1;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"UPDATE usuarios SET 
    nombre= @nombre,
    apellido = @apellido,
    email = @email,
    clave = @clave,
    avatarruta = @avatarruta,
    rol = @rol
    WHERE Id = @id;";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
       
        command.Parameters.AddWithValue("@nombre",usuarios.Nombre);
        command.Parameters.AddWithValue("@apellido",usuarios.Apellido);
        command.Parameters.AddWithValue("@email",usuarios.Email);
        command.Parameters.AddWithValue("@clave",usuarios.Clave);
        command.Parameters.AddWithValue("@avatarruta",usuarios.Avatarruta);
        command.Parameters.AddWithValue("@rol",usuarios.Rol);
        command.Parameters.AddWithValue("@id", usuarios.Id);
        connection.Open();
        res = command.ExecuteNonQuery();
        connection.Close();

    }

}
return res;

}

public int ModificarClave(int id, string claveNueva)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				string sql = $"UPDATE Usuarios SET Clave=@clave " +
					$"WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
				  command.Parameters.AddWithValue("@clave", claveNueva);			
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
public int Borrar(int id){
int res = -1;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"DELETE FROM usuarios WHERE Id = @id;";

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

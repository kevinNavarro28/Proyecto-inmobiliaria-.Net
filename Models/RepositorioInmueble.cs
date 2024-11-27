using MySql.Data.MySqlClient;

namespace Inmobiliaria.Models;

public class RepositorioInmueble
{

    string connectionString = "Server=127.0.0.1;port=3306;User=root;Password=;Database=inmobiliarianavarro;SslMode=none"; 

public RepositorioInmueble()
{

}

public List<Inmuebles>GetInmuebles()
{
    List<Inmuebles> inmuebles = new List<Inmuebles>(); 
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT i.Id,i.Tipo,i.Direccion,i.Uso,i.Precio,i.Cantidad_Ambientes,i.Superficie,i.Latitud,i.Longitud,Estado,
        i.PropietarioId,p.Nombre,p.Apellido FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.Id ";
        using(var command = new MySqlCommand(query , connection)){
        connection.Open();
        using (var reader = command.ExecuteReader()){
            while(reader.Read())
            {
                Inmuebles inmueble = new Inmuebles
                { 
                Id = reader.GetInt32(nameof(Inmuebles.Id)),
                Tipo = reader.GetString(nameof(Inmuebles.Tipo)),
                Direccion = reader.GetString(nameof(Inmuebles.Direccion)),
                Uso = reader.GetString(nameof(Inmuebles.Uso)),
                Precio = reader.GetInt32(nameof(Inmuebles.Precio)),
                Cantidad_Ambientes = reader.GetInt32(nameof(Inmuebles.Cantidad_Ambientes)),
                Superficie = reader.GetInt32(nameof(Inmuebles.Superficie)),                
                Latitud = reader.GetInt32(nameof(Inmuebles.Latitud)),
                Longitud = reader.GetInt32(nameof(Inmuebles.Longitud)),
                Estado = reader.GetString(nameof(Inmuebles.Estado)),
                PropietarioId = reader.GetInt32(nameof(Inmuebles.PropietarioId)),
                propietario = new Propietarios{
                    Id = reader.GetInt32(nameof(Propietarios.Id)),
                    Nombre = reader.GetString(nameof(Propietarios.Nombre)),
                    Apellido = reader.GetString(nameof(Propietarios.Apellido))
                }

                };
                inmuebles.Add(inmueble);

            }
        }

     }
     connection.Close();
    } 
    return inmuebles;  

}


public List<Inmuebles> ObtenerInmueblesPorPropietario(int Id)
{
    List<Inmuebles> inmuebles = new List<Inmuebles>();

    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        var query = @"SELECT i.Id,i.Direccion,i.Tipo,i.Precio,i.Uso,i.Superficie
                      FROM inmuebles i
                      WHERE PropietarioId = @Id;";
                      
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Id",Id);
            connection.Open();
            
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Inmuebles inmueble = new Inmuebles
                    {
                        Id = reader.GetInt32(nameof(Inmuebles.Id)),
                        Direccion = reader.GetString(nameof(Inmuebles.Direccion)),
                        Tipo = reader.GetString(nameof(Inmuebles.Tipo)),
                        Precio = reader.GetDouble(nameof(Inmuebles.Precio)),
                        Uso = reader.GetString(nameof(Inmuebles.Uso)),
                        Superficie = reader.GetInt32(nameof(Inmuebles.Superficie))
                    };
                    
                    inmuebles.Add(inmueble);
                }
            }
        }
        
        connection.Close();
    }

    return inmuebles;
}

public List<Inmuebles>ObtenerDisponibles()
{
    List<Inmuebles> inmuebles = new List<Inmuebles>(); 
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        var query = @"SELECT i.Id,i.Tipo,i.Direccion,i.Uso,i.Precio,i.Cantidad_Ambientes,i.Superficie,i.Latitud,i.Longitud,i.Estado,
        i.PropietarioId,p.Nombre,p.Apellido FROM Inmuebles i INNER JOIN Propietarios p ON i.PropietarioId = p.Id WHERE i.Estado LIKE 'Disponible'";
        using(var command = new MySqlCommand(query , connection)){
        connection.Open();
        using (var reader = command.ExecuteReader()){
            while(reader.Read())
            {
                Inmuebles inmueble = new Inmuebles
                { 
                Id = reader.GetInt32(nameof(Inmuebles.Id)),
                Tipo = reader.GetString(nameof(Inmuebles.Tipo)),
                Direccion = reader.GetString(nameof(Inmuebles.Direccion)),
                Uso = reader.GetString(nameof(Inmuebles.Uso)),
                Precio = reader.GetInt32(nameof(Inmuebles.Precio)),
                Superficie = reader.GetInt32(nameof(Inmuebles.Superficie)),                
                Estado = reader.GetString(nameof(Inmuebles.Estado)),
                PropietarioId = reader.GetInt32(nameof(Inmuebles.PropietarioId)),
                propietario = new Propietarios{
                    Id = reader.GetInt32(nameof(Propietarios.Id)),
                    Nombre = reader.GetString(nameof(Propietarios.Nombre)),
                    Apellido = reader.GetString(nameof(Propietarios.Apellido))
                }

                };
                inmuebles.Add(inmueble);

            }
        }

     }
     connection.Close();
    } 
    return inmuebles;  

}
public List<Inmuebles> GetInmueblesDisponiblesPorFecha(DateTime fechaInicio, DateTime fechaFin)
{
    List<Inmuebles> inmueblesDisponibles = new List<Inmuebles>();

    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        // Consulta SQL para obtener inmuebles disponibles
        var query = @"
            SELECT i.Id, i.Tipo, i.Direccion
            FROM inmuebles i
            LEFT JOIN contratos c ON i.Id = c.InmuebleId
                AND (
                    (c.Fecha_Inicio <= @fechaFin AND c.Fecha_Fin >= @fechaInicio)
                )
            WHERE i.Estado = 'disponible'  -- Filtramos por estado disponible
            AND c.Id IS NULL;";

        using (MySqlCommand command = new MySqlCommand(query, connection))
        {
            // Parámetros de la consulta
            command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            command.Parameters.AddWithValue("@fechaFin", fechaFin);

            connection.Open();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var inmueble = new Inmuebles
                    {
                        Id = reader.GetInt32(nameof(Inmuebles.Id)),
                        Tipo = reader.GetString(nameof(Inmuebles.Tipo)),
                        Direccion = reader.GetString(nameof(Inmuebles.Direccion))
                    };

                    inmueblesDisponibles.Add(inmueble);
                }
            }
        }
    }

    return inmueblesDisponibles;
}

public Inmuebles ObtenerInmueble(int id)
{
    Inmuebles inmueble = null;
    
    using (MySqlConnection connection = new MySqlConnection(connectionString)) 
    {
        string sql = @$"SELECT i.Id,i.Tipo,i.Direccion,i.Uso,i.Precio,i.Cantidad_Ambientes,i.Superficie,i.Latitud,i.Longitud,Estado,
        i.PropietarioId,p.Nombre,p.Apellido FROM inmuebles i INNER JOIN propietarios p ON i.PropietarioId = p.Id
        WHERE i.Id = @Id";
        using(var command = new MySqlCommand(sql , connection)){
            command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using (var reader = command.ExecuteReader()){
            if(reader.Read())
            {
                inmueble = new Inmuebles
                { 
                Id = reader.GetInt32(nameof(Inmuebles.Id)),
                Tipo = reader.GetString(nameof(Inmuebles.Tipo)),
                Direccion = reader.GetString(nameof(Inmuebles.Direccion)),
                Uso = reader.GetString(nameof(Inmuebles.Uso)),
                Precio = reader.GetInt32(nameof(Inmuebles.Precio)),
                Cantidad_Ambientes = reader.GetInt32(nameof(Inmuebles.Cantidad_Ambientes)),
                Superficie = reader.GetInt32(nameof(Inmuebles.Superficie)),
                Latitud = reader.GetInt32(nameof(Inmuebles.Latitud)),
                Longitud = reader.GetInt32(nameof(Inmuebles.Longitud)),
                Estado = reader.GetString(nameof(Inmuebles.Estado)),
                     PropietarioId = reader.GetInt32(nameof(Inmuebles.PropietarioId)),
                propietario = new Propietarios{
                    Id= reader.GetInt32(nameof(Propietarios.Id)),
                    Nombre = reader.GetString(nameof(Propietarios.Nombre)),
                    Apellido = reader.GetString(nameof(Propietarios.Apellido))
                }
             

                };
              

            }
        }

     }
     connection.Close();
    } 
    return inmueble;  

}

public int Alta(Inmuebles inmuebles){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"INSERT INTO inmuebles (Tipo,Direccion,Uso,Precio,Cantidad_Ambientes,Superficie,Latitud,Longitud,Estado,PropietarioId )
    VALUES (@tipo,@direccion,@uso,@precio,@cantidad_ambientes,@superficie,@latitud,@longitud,@estado,@PropietarioId);
    SELECT LAST_INSERT_ID();";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
        command.Parameters.AddWithValue("@tipo",inmuebles.Tipo);
        command.Parameters.AddWithValue("@direccion",inmuebles.Direccion);
        command.Parameters.AddWithValue("@uso",inmuebles.Uso);
        command.Parameters.AddWithValue("@precio",inmuebles.Precio);
        command.Parameters.AddWithValue("@cantidad_ambientes",inmuebles.Cantidad_Ambientes);
        command.Parameters.AddWithValue("@superficie",inmuebles.Superficie);
        command.Parameters.AddWithValue("@latitud",inmuebles.Latitud);
        command.Parameters.AddWithValue("@longitud",inmuebles.Longitud);
        command.Parameters.AddWithValue("@estado","Disponible");
        command.Parameters.AddWithValue("@PropietarioId",inmuebles.PropietarioId);
        connection.Open();
        res = Convert.ToInt32(command.ExecuteScalar());
        connection.Close();

    }

}
return res;

}

public int Modificar(Inmuebles inmueble){
int res = 0;
using(MySqlConnection connection = new MySqlConnection(connectionString))
{
    string query = @"UPDATE inmuebles SET 
    tipo= @tipo,
    direccion = @direccion,
    uso = @uso,
    precio = @precio,
    cantidad_ambientes = @cantidad_ambientes,
    superficie = @superficie,
    latitud = @latitud,
    longitud = @longitud,
    estado = @estado,
    propietarioid = @propietarioid
    WHERE Id = @id; ";
    using(MySqlCommand command = new MySqlCommand (query,connection)){
        
        command.Parameters.AddWithValue("@tipo",inmueble.Tipo);
        command.Parameters.AddWithValue("@direccion",inmueble.Direccion);
        command.Parameters.AddWithValue("@uso",inmueble.Uso);
        command.Parameters.AddWithValue("@precio",inmueble.Precio);
        command.Parameters.AddWithValue("@cantidad_ambientes",inmueble.Cantidad_Ambientes);
        command.Parameters.AddWithValue("@superficie",inmueble.Superficie);
        command.Parameters.AddWithValue("@latitud",inmueble.Latitud);
        command.Parameters.AddWithValue("@longitud",inmueble.Longitud);
        command.Parameters.AddWithValue("@estado",inmueble.Estado);
        command.Parameters.AddWithValue("@propietarioid",inmueble.PropietarioId);
        command.Parameters.AddWithValue("@id",inmueble.Id);
        {
            
        };
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
    string query = @"DELETE FROM inmuebles WHERE id = @id;";

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

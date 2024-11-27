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

public List<Inmuebles> GetInmueblesDisponibles(DateTime fechaInicio, DateTime fechaFin)
    {
        List<Inmuebles> inmuebles = new List<Inmuebles>();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            var query = @"SELECT i.Id, i.Tipo, i.Direccion
                          FROM inmuebles i
                          WHERE NOT EXISTS (
                              SELECT 1
                              FROM contratos c
                              WHERE c.InmuebleId = i.Id 
                              AND c.Fecha_Inicio <= @fechaFin 
                              AND c.Fecha_Fin >= @fechaInicio
                          );";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                command.Parameters.AddWithValue("@fechaFin", fechaFin);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inmuebles inmueble = new Inmuebles
                        {
                            Id = reader.GetInt32(nameof(Inmuebles.Id)),
                            Tipo = reader.GetString(nameof(Inmuebles.Tipo)),
                            Direccion = reader.GetString(nameof(Inmuebles.Direccion))
                        };

                        inmuebles.Add(inmueble);
                    }
                }
                connection.Close();
            }
        }

        return inmuebles;
    }





public List<Contratos> ObtenerContratosVigentes()
{
    var contratosVigentes = new List<Contratos>();
    using (var connection = new MySqlConnection(connectionString))
    {
        var query = @"SELECT * FROM Contratos
                      WHERE Fecha_Inicio <= @fechaActual AND Fecha_Fin >= @fechaActual";
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@fechaActual", DateTime.Today);

            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Contratos contrato = new Contratos
                    {
                        Id = reader.GetInt32("Id"),
                        Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                        Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                        Monto = reader.GetDouble("Monto"),
                        InmuebleId = reader.GetInt32("InmuebleId"),
                        InquilinoId = reader.GetInt32("InquilinoId")
                    };
                    contratosVigentes.Add(contrato);
                }
            }
        }
    }
    return contratosVigentes;
}

public List<Contratos> ObtenerContratosPorInmueble(int InmuebleId)
{
    var contratos = new List<Contratos>();
    using (var connection = new MySqlConnection(connectionString))
    {
        // Modificando la consulta para traer también el nombre y apellido del inquilino y la dirección del inmueble
        var query = @"
            SELECT c.*, i.Direccion, inq.Nombre, inq.Apellido 
            FROM Contratos c
            JOIN Inmuebles i ON c.InmuebleId = i.Id
            JOIN Inquilinos inq ON c.InquilinoId = inq.Id
            WHERE c.InmuebleId = @InmuebleId";
        
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@InmuebleId", InmuebleId);

            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
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
    return contratos;
}}}
    
    

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
public double CalcularMulta(Contratos contrato, List<Pagos> pagosRealizados)
{
    // Verificar si no se han realizado pagos
    if (!pagosRealizados.Any())
    {
        return contrato.Monto * 2 ;
    }

    // Calcular los meses totales del contrato
    double mesesTotalesContrato = ((contrato.Fecha_Fin.Year - contrato.Fecha_Inicio.Year)*12)  +                     
                                   contrato.Fecha_Fin.Month - contrato.Fecha_Inicio.Month;

    // Calcular los meses cumplidos hasta ahora
    double mesesCumplidos = ((DateTime.Now.Year - contrato.Fecha_Inicio.Year) * 12) +
                             DateTime.Now.Month - contrato.Fecha_Inicio.Month;

    // Calcular los meses restantes
    double mesesRestantes = Math.Max(mesesTotalesContrato - mesesCumplidos, 0);

    // Determinar la multa regular
    return mesesCumplidos < (mesesTotalesContrato / 2)
        ? contrato.Monto * 2
        : contrato.Monto;
}
public void Baja(int id)
{
    using (var conexion = new MySqlConnection(connectionString))
    {
        conexion.Open();

        // Primero elimina los pagos relacionados
        var queryPagos = "DELETE FROM pagos WHERE ContratoId = @id";
        using (var cmd = new MySqlCommand(queryPagos, conexion))
        {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // Luego elimina el contrato
        var queryContrato = "DELETE FROM contratos WHERE Id = @id";
        using (var cmd = new MySqlCommand(queryContrato, conexion))
        {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}


}

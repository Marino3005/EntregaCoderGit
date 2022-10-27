using EntregaCoder.Models;
using System.Data;
using System.Data.SqlClient;

namespace EntregaCoder.Repository
{
    public class ADO_Usuario
    {
        public static Usuario Traer_Usuario(string Nombre)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                string commText = "SELECT * FROM Usuario WHERE Nombre =@NomUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);

                var Parametero = new SqlParameter("NomUsu", SqlDbType.VarChar);
                Parametero.Value = Nombre;
                Comm.Parameters.Add(Parametero);
                var reader = Comm.ExecuteReader();
                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader.GetValue(0));
                    usuario.Nombre = Convert.ToString(reader.GetValue(1));
                    usuario.Apellido = Convert.ToString(reader.GetValue(2));
                    usuario.NombreUsuario = Convert.ToString(reader.GetValue(3));
                    usuario.Contraseña = Convert.ToString(reader.GetValue(4));
                    usuario.Mail = Convert.ToString(reader.GetValue(5));
                }
                reader.Close();
                connection.Close();
                return usuario;

            }
        }

        public static Usuario inicio_Sesion(string nombreUsuario, string Contrasena)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                string commText = "SELECT Contraseña FROM Usuario WHERE NombreUsuario =@NomUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);

                var Parametero = new SqlParameter("NomUsu", SqlDbType.VarChar);
                Parametero.Value = nombreUsuario;
                Comm.Parameters.Add(Parametero);
                var holder = Convert.ToString(Comm.ExecuteScalar());

                if (holder == Contrasena)
                {
                    string commText1 = "SELECT * FROM Usuario WHERE NombreUsuario =@NomUsu";
                    SqlCommand Comm1 = new SqlCommand(commText1, connection);

                    var Parametero1 = new SqlParameter("NomUsu", SqlDbType.VarChar);
                    Parametero1.Value = nombreUsuario;
                    Comm1.Parameters.Add(Parametero1);
                    var reader = Comm1.ExecuteReader();

                    while (reader.Read())
                    {
                        usuario.Id = Convert.ToInt32(reader.GetValue(0));
                        usuario.Nombre = Convert.ToString(reader.GetValue(1));
                        usuario.Apellido = Convert.ToString(reader.GetValue(2));
                        usuario.NombreUsuario = Convert.ToString(reader.GetValue(3));
                        usuario.Contraseña = Convert.ToString(reader.GetValue(4));
                        usuario.Mail = Convert.ToString(reader.GetValue(5));
                    }
                    reader.Close();
                    connection.Close();
                    return usuario;
                }
                else return usuario;

            }
        }

        public static void Modificar_Usuario(Usuario us)
        {

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                string commText = "UPDATE Usuario SET Nombre =@Nom, Apellido =@Apell, NombreUsuario =@NomUsu, Contraseña =@ConUsu, mail =@Mailusu  WHERE Id =@IdUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);

                var Parametero = new SqlParameter("Nom", SqlDbType.VarChar);
                Parametero.Value = us.Nombre;
                Comm.Parameters.Add(Parametero);

                var Parametero1 = new SqlParameter("Apell", SqlDbType.VarChar);
                Parametero1.Value = us.Apellido;
                Comm.Parameters.Add(Parametero1);

                var Parametero2 = new SqlParameter("NomUsu", SqlDbType.VarChar);
                Parametero2.Value = us.NombreUsuario;
                Comm.Parameters.Add(Parametero2);

                var Parametero3 = new SqlParameter("ConUsu", SqlDbType.VarChar);
                Parametero3.Value = us.Contraseña;
                Comm.Parameters.Add(Parametero3);

                var Parametero4 = new SqlParameter("Mailusu", SqlDbType.VarChar);
                Parametero4.Value = us.Mail;
                Comm.Parameters.Add(Parametero4);

                var Parametero5 = new SqlParameter("IdUsu", SqlDbType.BigInt);
                Parametero5.Value = us.Id;
                Comm.Parameters.Add(Parametero5);



                Comm.ExecuteNonQuery();
            }
        }

        public static void Eliminar_Usuario(int id)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                string commText = "DELETE FROM Usuario WHERE Id =@IdUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);

                var Parametero = new SqlParameter("IdUsu", SqlDbType.BigInt);
                Parametero.Value = id;
                Comm.Parameters.Add(Parametero);
                Comm.ExecuteNonQuery();
            }
        }

        public static void Crear_Usuario(Usuario usu)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                string commText = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, mail) " +
                    "VALUES (@Nom, @Apell, @NomUsu, @Contr, @MailUsu)";
                SqlCommand Comm = new SqlCommand(commText, connection);

                var Parametero = new SqlParameter("Nom", SqlDbType.VarChar);
                Parametero.Value = usu.Nombre;
                Comm.Parameters.Add(Parametero);

                var Parametero1 = new SqlParameter("Apell", SqlDbType.VarChar);
                Parametero1.Value = usu.Apellido;
                Comm.Parameters.Add(Parametero1);

                var Parametero2 = new SqlParameter("NomUsu", SqlDbType.VarChar);
                Parametero2.Value = usu.NombreUsuario;
                Comm.Parameters.Add(Parametero2);

                var Parametero3 = new SqlParameter("Contr", SqlDbType.VarChar);
                Parametero3.Value = usu.Contraseña;
                Comm.Parameters.Add(Parametero3);

                var Parametero4 = new SqlParameter("MailUsu", SqlDbType.VarChar);
                Parametero4.Value = usu.Mail;
                Comm.Parameters.Add(Parametero4);


                Comm.ExecuteNonQuery();
            }
        }

        public static List<Usuario> Traer_Todos_Usuarios()
        {
            var listaUsuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM usuario";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var usuario = new Usuario();

                    usuario.Id = Convert.ToInt32(reader.GetValue(0));
                    usuario.Nombre = reader.GetValue(1).ToString();
                    usuario.Apellido = reader.GetValue(2).ToString();
                    usuario.NombreUsuario = reader.GetValue(3).ToString();
                    usuario.Contraseña = reader.GetValue(4).ToString();
                    usuario.Mail = reader.GetValue(5).ToString();

                    listaUsuarios.Add(usuario);

                }
                reader.Close();
                connection.Close();

            }
            return listaUsuarios;
        }
        
    }
}

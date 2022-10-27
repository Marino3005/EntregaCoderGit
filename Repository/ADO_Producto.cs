using EntregaCoder.Models;
using System.Data;
using System.Data.SqlClient;

namespace EntregaCoder.Repository
{
    public class ADO_Producto
    {
        public static List<Producto> Traer_Producto_De_Usuario(int IdUsuario)
        {
            var listaProductos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {


                connection.Open();
                string commText = "SELECT * FROM Producto WHERE IdUsuario =@IdUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);
                var Parametero = new SqlParameter("IdUsu", SqlDbType.BigInt);
                Parametero.Value = IdUsuario;
                Comm.Parameters.Add(Parametero);
                var reader = Comm.ExecuteReader();
                while (reader.Read())
                {
                    var Produc = new Producto();

                    Produc.Id = Convert.ToInt32(reader.GetValue(0));
                    Produc.Descripciones = Convert.ToString(reader.GetValue(1));
                    Produc.Costo = Convert.ToDouble(reader.GetValue(2));
                    Produc.PrecioVenta = Convert.ToDouble(reader.GetValue(3));
                    Produc.Stock = Convert.ToInt32(reader.GetValue(4));
                    Produc.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                    listaProductos.Add(Produc);
                }
                reader.Close();
            }
            return listaProductos;
        }

        public static List<Producto> Traer_Todos_Productos()
        {
            var listaProductos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {


                connection.Open();
                string commText = "SELECT * FROM Producto";
                SqlCommand Comm = new SqlCommand(commText, connection);
                var reader = Comm.ExecuteReader();
                while (reader.Read())
                {
                    var Produc = new Producto();

                    Produc.Id = Convert.ToInt32(reader.GetValue(0));
                    Produc.Descripciones = Convert.ToString(reader.GetValue(1));
                    Produc.Costo = Convert.ToDouble(reader.GetValue(2));
                    Produc.PrecioVenta = Convert.ToDouble(reader.GetValue(3));
                    Produc.Stock = Convert.ToInt32(reader.GetValue(4));
                    Produc.IdUsuario = Convert.ToInt32(reader.GetValue(5));

                    listaProductos.Add(Produc);
                }
                reader.Close();
            }
            return listaProductos;
        }

        public static void Crear_Producto(Producto prod)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                string commText = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) " +
                    "VALUES (@Desc, @Costo, @Precio, @Stock, @IdUsu)";
                SqlCommand Comm = new SqlCommand(commText, connection);

                var Parametero = new SqlParameter("Desc", SqlDbType.VarChar);
                Parametero.Value = prod.Descripciones;
                Comm.Parameters.Add(Parametero);

                var Parametero1 = new SqlParameter("Costo", SqlDbType.Money);
                Parametero1.Value = prod.Costo;
                Comm.Parameters.Add(Parametero1);

                var Parametero2 = new SqlParameter("Precio", SqlDbType.Money);
                Parametero2.Value = prod.PrecioVenta;
                Comm.Parameters.Add(Parametero2);

                var Parametero3 = new SqlParameter("Stock", SqlDbType.Int);
                Parametero3.Value = prod.Stock;
                Comm.Parameters.Add(Parametero3);

                var Parametero4 = new SqlParameter("IdUsu", SqlDbType.BigInt);
                Parametero4.Value = prod.IdUsuario;
                Comm.Parameters.Add(Parametero4);


                Comm.ExecuteNonQuery();
            }
        }

        public static void Modificar_Producto(Producto producto)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                string commText = "UPDATE Producto SET Descripciones =@Des, Costo =@Cost, PrecioVenta =@PrecioVen, Stock =@Stoc, IdUsuario =@IdUsu  WHERE Id =@IdProd";
                SqlCommand Comm = new SqlCommand(commText, connection);

                var Parametero = new SqlParameter("Des", SqlDbType.VarChar);
                Parametero.Value = producto.Descripciones;
                Comm.Parameters.Add(Parametero);

                var Parametero1 = new SqlParameter("Cost", SqlDbType.Money);
                Parametero1.Value = producto.Costo;
                Comm.Parameters.Add(Parametero1);

                var Parametero2 = new SqlParameter("PrecioVen", SqlDbType.Money);
                Parametero2.Value = producto.PrecioVenta;
                Comm.Parameters.Add(Parametero2);

                var Parametero3 = new SqlParameter("Stoc", SqlDbType.Int);
                Parametero3.Value = producto.Stock;
                Comm.Parameters.Add(Parametero3);

                var Parametero4 = new SqlParameter("IdUsu", SqlDbType.BigInt);
                Parametero4.Value = producto.IdUsuario;
                Comm.Parameters.Add(Parametero4);

                var Parametero5 = new SqlParameter("IdProd", SqlDbType.BigInt);
                Parametero5.Value = producto.Id;
                Comm.Parameters.Add(Parametero5);



                Comm.ExecuteNonQuery();
            }
        }

        public static void Eliminar_Producto(int id)
        {
            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {
                connection.Open();
                string commText = "DELETE FROM Producto WHERE Id =@IdProd";
                SqlCommand Comm = new SqlCommand(commText, connection);

                var Parametero = new SqlParameter("IdProd", SqlDbType.BigInt);
                Parametero.Value = id;
                Comm.Parameters.Add(Parametero);
                Comm.ExecuteNonQuery();
            }
        }
    }
}


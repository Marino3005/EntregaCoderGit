using EntregaCoder.Models;
using System.Data;
using System.Data.SqlClient;

namespace EntregaCoder.Repository
{
    public class ADO_ProductoVendido
    {
        public static List<ProductoVendido> Traer_ProductoVendido(int IdUsuario)
        {
            var listaProductosVendidos = new List<ProductoVendido>();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {


                connection.Open();
                string commText = "SELECT  pv.Id, pv.Stock, pv.IdProducto, pv.IdVenta FROM Producto as p INNER JOIN ProductoVendido as pv ON p.Id = pv.IdProducto WHERE p.IdUsuario = @IdUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);
                var Parametero = new SqlParameter("IdUsu", SqlDbType.BigInt);
                Parametero.Value = IdUsuario;
                Comm.Parameters.Add(Parametero);
                var reader = Comm.ExecuteReader();
                while (reader.Read())
                {
                    var Produc = new ProductoVendido();

                    Produc.id = Convert.ToInt32(reader.GetValue(0));
                    Produc.Stock = Convert.ToInt32(reader.GetValue(1));
                    Produc.IdProducto = Convert.ToInt32(reader.GetValue(2));
                    Produc.IdVenta = Convert.ToInt32(reader.GetValue(3));

                    listaProductosVendidos.Add(Produc);
                }
                reader.Close();
            }
            return listaProductosVendidos;
        }
    }
}

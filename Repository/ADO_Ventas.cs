using EntregaCoder.Models;
using System.Data;
using System.Data.SqlClient;

namespace EntregaCoder.Repository
{
    public class ADO_Ventas
    {
        public static List<Venta> Traer_Ventas(int IdUsuario)
        {
            var listaVentas = new List<Venta>();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {

                connection.Open();
                string commText = "SELECT * FROM Venta WHERE IdUsuario =@IdUsu";
                SqlCommand Comm = new SqlCommand(commText, connection);
                var Parametero = new SqlParameter("IdUsu", SqlDbType.BigInt);
                Parametero.Value = IdUsuario;
                Comm.Parameters.Add(Parametero);
                var reader = Comm.ExecuteReader();
                while (reader.Read())
                {
                    var Venta = new Venta();

                    Venta.id = Convert.ToInt32(reader.GetValue(0));
                    Venta.Comentarios = Convert.ToString(reader.GetValue(1));
                    Venta.IdUsuario = Convert.ToInt32(reader.GetValue(2));


                    listaVentas.Add(Venta);
                }
                reader.Close();
            }
            return listaVentas;
        }

        public static void Cargar_Venta(int idUsuario, string descripcionProducto, int stockVendido)
        {
            List<Usuario> lista = ADO_Usuario.Traer_Todos_Usuarios();
            List<Producto> listaProducto = ADO_Producto.Traer_Todos_Productos();
            Producto productoNuevo = new Producto();

            bool validacion1 = false;
            bool validacion2 = false;
            string comentario = "Venta de " + stockVendido + " " + descripcionProducto;

            //VALIDACION DE SI EXISTE EL USUARIO Y SI EXISTE EL PRODUCTO INGRESADO
            foreach (Usuario usuario in lista)
            {
                if (usuario.Id == idUsuario)
                {
                    validacion1 = true;
                }
            }
            foreach (Producto producto in listaProducto)
            {
                if (producto.Descripciones == descripcionProducto)
                {
                    validacion2 = true;

                    //BUSCAR CAMBIAR LA DESCRIPCION DEL PRODUCTO POR SU ID,
                    //YA QUE ESTE ULTIMO ES EL QUE SE INGRESA EN LA TABLA PRODUCTOVENDIDO
                    
                    
                    productoNuevo.Id =  producto.Id;
                    productoNuevo.Descripciones = producto.Descripciones;
                    productoNuevo.Stock = producto.Stock;
                    productoNuevo.Costo = producto.Costo;
                    productoNuevo.PrecioVenta = producto.PrecioVenta;
                    productoNuevo.IdUsuario = producto.IdUsuario;
                }
                
            }
            if(validacion1 && validacion2)
            {
                //CONEXION A LA BASE DE DATOS
                using (SqlConnection connection = new SqlConnection(General.connectionString()))
                {
                    
                    //CARGAR VENTA A PRODUCTOS VENDIDOS
                    connection.Open();
                    string commText = "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) " +
                        "VALUES (@Stock, @IdProdu, @IdVen)";
                    SqlCommand Comm = new SqlCommand(commText, connection);

                    var Parametero = new SqlParameter("Stock", SqlDbType.Int);
                    Parametero.Value = stockVendido;
                    Comm.Parameters.Add(Parametero);

                    var Parametero1 = new SqlParameter("IdProdu", SqlDbType.BigInt);
                    Parametero1.Value = productoNuevo.Id;
                    Comm.Parameters.Add(Parametero1);

                    var Parametero2 = new SqlParameter("IdVen", SqlDbType.BigInt);
                    Parametero2.Value = idUsuario;
                    Comm.Parameters.Add(Parametero2);

                    Comm.ExecuteNonQuery();


                    //CARGAR VENTA A TABLA DE VENTAS

                    string commText1 = "INSERT INTO Venta (Comentarios, IdUsuario) " +
                        "VALUES (@Com, @IdUsu)";
                    SqlCommand Comm1 = new SqlCommand(commText1, connection);

                    var Parameter = new SqlParameter("Com", SqlDbType.VarChar);
                    Parameter.Value = comentario;
                    Comm1.Parameters.Add(Parameter);

                    var Parameter1 = new SqlParameter("IdUsu", SqlDbType.BigInt);
                    Parameter1.Value = idUsuario;
                    Comm1.Parameters.Add(Parameter1);

                    Comm1.ExecuteNonQuery();

                    //Actualizar el Stock del producto que se vendio
                    productoNuevo.Stock = productoNuevo.Stock - stockVendido;
                    ADO_Producto.Modificar_Producto(productoNuevo);

                }
            }

        }

        public static List<Venta> Traer_Todas_Ventas()
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection connection = new SqlConnection(General.connectionString()))
            {


                connection.Open();
                string commText = "SELECT * FROM Venta";
                SqlCommand Comm = new SqlCommand(commText, connection);
                var reader = Comm.ExecuteReader();
                while (reader.Read())
                {
                    var Venta = new Venta();

                    Venta.id = Convert.ToInt32(reader.GetValue(0));
                    Venta.Comentarios = Convert.ToString(reader.GetValue(1));
                    Venta.IdUsuario = Convert.ToInt32(reader.GetValue(2));


                    ventas.Add(Venta);
                }
                reader.Close();
            }
            return ventas;
        }
    }
}

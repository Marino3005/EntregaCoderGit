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

        public static void Cargar_Venta(int idUsuario, string comentario, List<ProductoVendido> listaProductos)
        {
            List<Usuario> lista = ADO_Usuario.Traer_Todos_Usuarios();
            List<Producto> listaTodosProductos = ADO_Producto.Traer_Todos_Productos();
            Producto productoNuevo = new Producto();

            bool validacion1 = false;
            bool validacion2 = false;
            

            //VALIDACION DE SI EXISTE EL USUARIO 
            foreach (Usuario usuario in lista)
            {
                if (usuario.Id == idUsuario)
                {
                    validacion1 = true;
                }
            }
            
            if(validacion1)
            {
                //CONEXION A LA BASE DE DATOS
                using (SqlConnection connection = new SqlConnection(General.connectionString()))
                {
                    connection.Open();

                    //INSERTAR EN LA TABLA VENTA LA VENTA, EL ID LO PASO COMO PARAMETRO,
                    //EL COMENTARIO YA LO CREAMOS.

                    //USAMOS EL SELECT @@IDENTITY PARA GUARDAR EN UNA VARIABLE EL ID DE LA NUEVA VENTA,
                    //YA QUE ESTE SERA PASADO COMO PARAMETRO A LA TABLA PRODUCTOVENDIDO EN LA COLUMNA "IdVenta". 
                    string commText1 = "INSERT INTO Venta (Comentarios, IdUsuario) " +
                            "VALUES (@Com, @IdUsu)  select @@identity";
                    SqlCommand Comm1 = new SqlCommand(commText1, connection);

                    var Parameter = new SqlParameter("Com", SqlDbType.VarChar);
                    Parameter.Value = comentario;
                    Comm1.Parameters.Add(Parameter);

                    var Parameter1 = new SqlParameter("IdUsu", SqlDbType.BigInt);
                    Parameter1.Value = idUsuario;
                    Comm1.Parameters.Add(Parameter1);

                    int nuevoId = Convert.ToInt32(Comm1.ExecuteScalar());
                

                

                
               

                    //ITERAR POR TODOS LOS PRODUCTOSVENDIDOS QUE PASO EL USUARIO, VALIDAR QUE EL PRODUCTO EXISTE
                    //Y AGREGAR CADA UNO DE ELLOS A LA TABLA PRODUCTOVENDIDO.
                    foreach (ProductoVendido producto in listaProductos)
                    {
                        
                        //VALIDAR QUE EL PRODUCTO EXISTE. SI EXISTE, TRAER TODOS LOS DATOS DEL PRODUCTO.
                        foreach (Producto producto1 in listaTodosProductos)
                        {
                            if (producto1.Id == producto.IdProducto)
                            {
                                validacion2 = true;

                                productoNuevo.Id = producto1.Id;
                                productoNuevo.Descripciones = producto1.Descripciones;
                                productoNuevo.Stock = producto1.Stock;
                                productoNuevo.Costo = producto1.Costo;
                                productoNuevo.PrecioVenta = producto1.PrecioVenta;
                                productoNuevo.IdUsuario = producto1.IdUsuario;
                            }
                        }

                        
                        if (validacion2)
                        {

                            //INSERTAR EN LA TABLA PRODUCTOS VENDIDOS LOS DATOS DE LA NUEVA VENTA.

                            string commText = "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) " +
                                "VALUES (@Stock, @IdProdu, @IdVen)";
                            SqlCommand Comm = new SqlCommand(commText, connection);

                            var Parametero = new SqlParameter("Stock", SqlDbType.Int);
                            Parametero.Value = producto.Stock;
                            Comm.Parameters.Add(Parametero);

                            var Parametero1 = new SqlParameter("IdProdu", SqlDbType.BigInt);
                            Parametero1.Value = producto.IdProducto;
                            Comm.Parameters.Add(Parametero1);

                            var Parametero2 = new SqlParameter("IdVen", SqlDbType.BigInt);
                            Parametero2.Value = nuevoId;
                            Comm.Parameters.Add(Parametero2);

                            Comm.ExecuteNonQuery();


                            //Actualizar el Stock del producto que se vendio
                            productoNuevo.Stock = productoNuevo.Stock - producto.Stock;
                            ADO_Producto.Modificar_Producto(productoNuevo);
                        }
                    }
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

using Jardines2023.Comun.Interfaces;
using Jardines2023.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jardines2023.Entidades.Dtos.Cliente;

namespace Jardines2023.Datos.Repositorios
{
    public class RepositorioCompra:IRepoCompra
    {
        private readonly string cadenaConexion;
        public RepositorioCompra()
        {
            cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexion"].ToString();
        }

        public void Agregar(Compra compra)
        {
            try
            {
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    var selectQuery = @"INSERT INTO Clientes (CompraId,FechaCompra,ProveedorId,Total)
                                                    VALUES (@CompraId, @FechaCompra, @ProveedorId, @Total); SELECT SCOPE_IDENTITY()";
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        comando.Parameters.Add("@CompraId", SqlDbType.Int);
                        comando.Parameters["@CompraId"].Value = compra.CompraId;

                        comando.Parameters.Add("@FechaCompra", SqlDbType.NVarChar);
                        comando.Parameters["@FechaCompra"].Value = compra.FechaCompra;

                        comando.Parameters.Add("@ProveerdorId", SqlDbType.Int);
                        comando.Parameters["@ProveedorId"].Value = compra.ProveedorId;

                        comando.Parameters.Add("@Total", SqlDbType.Float);
                        comando.Parameters["@Total"].Value = compra.Total;

                        int id = Convert.ToInt32(comando.ExecuteScalar());
                        compra.CompraId = id;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void Borrar(int compraID)
        {
            throw new NotImplementedException();
        }

        public void Editar(Compra compra)
        {
            throw new NotImplementedException();
        }

        public bool Existe(Compra compra)
        {
            throw new NotImplementedException();
        }

        public int GetCantidad()
        {
            try
            {
                int cantidad = 0;
                using (var con = new SqlConnection(cadenaConexion))
                {
                    con.Open();
                    string selectQuery = "SELECT COUNT(*) FROM Compras";
                    using (var cmd = new SqlCommand(selectQuery, con))
                    { 

                        cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
                return cantidad;
            }
            catch (Exception)
            {

                throw;
            }
        }

     

        public List<Compra> GetClientesPorPagina(int registrosPorPagina, int paginaActual)
        {
            try
            {
                List<Compra> lista = new List<Compra>();
                using (var con = new SqlConnection(cadenaConexion))
                {
                    con.Open();
                    string selectQuery = @"SELECT c.CompraId, c.FechaCompra, c.ProveedorId,c.Total
                                         FROM Compras c
                                         ORDER BY c.CompraId
                                         OFFSET @cantidadRegistros
                                         ROWS FETCH NEXT @cantidadPorPagina ROWS ONLY";


                    using (var comando = new SqlCommand(selectQuery, con))
                    {
                        comando.Parameters.Add("@cantidadRegistros", SqlDbType.Int);
                        comando.Parameters["@cantidadRegistros"].Value = registrosPorPagina * (paginaActual - 1);

                        comando.Parameters.Add("@cantidadPorPagina", SqlDbType.Int);
                        comando.Parameters["@cantidadPorPagina"].Value = registrosPorPagina;
                      
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var compra = ConstruirCompra(reader);

                                lista.Add(compra);
                            }
                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Compra> GetCompra()
        {
            try
            {
                List<Compra> lista = new List<Compra>();
                using (var conn = new SqlConnection(cadenaConexion))
                {
                    conn.Open();
                    string selectQuery = "SELECT c.CompraId,c.FechaCompra,c.ProveedorId,c.Total FROM Compras c ORDER BY CompraId";
                    using (var comando = new SqlCommand(selectQuery, conn))
                    {
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var compra = ConstruirCompra(reader);

                                lista.Add(compra);
                            }

                        }
                    }
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private Compra ConstruirCompra(SqlDataReader reader)
        {
            var compra = new Compra()
            {
                CompraId = reader.GetInt32(0),
                FechaCompra = reader.GetDateTime(1),
                ProveedorId = reader.GetInt32(2),
                Total = reader.GetDecimal(3),

            };
            return compra;
        }
    }
}

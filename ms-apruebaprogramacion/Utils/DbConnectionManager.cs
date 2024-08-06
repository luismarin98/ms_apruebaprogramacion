using Microsoft.Data.SqlClient;

namespace ms_apruebaprogramacion.Utils
{
    public class DbConnectionManager : IDisposable
    {
        private readonly SqlConnection SqlConnection;

        public DbConnectionManager(Provider Provider)
        {
            SqlConnection = new SqlConnection(Provider.Url.SQLConnection);
        }

        public SqlConnection ObtenerConexion()
        {
            if (SqlConnection.State != System.Data.ConnectionState.Open)
            {
                SqlConnection.Open();

            }
            return SqlConnection;
        }

        public void CerrarConexion()
        {
            if (SqlConnection.State != System.Data.ConnectionState.Closed)
            {
                SqlConnection.Close();

            }
        }

        // Implementacion de IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Liberar recursos gestionados (cerrar la conexion)
                CerrarConexion();
            }
        }
        // Destructor
        ~DbConnectionManager()
        {
            Dispose(false);
        }
    }
}

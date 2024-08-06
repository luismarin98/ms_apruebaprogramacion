using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ms_apruebaprogramacion.Constans;
using ms_apruebaprogramacion.Controllers.Contract;
using ms_apruebaprogramacion.Utils;
using saff_core.utilitarios;
using System.Transactions;

namespace ms_apruebaprogramacion.Service.Impl
{
    public class DatosProgLabRepository : IDatosProgLabRepository
    {
        private readonly LogUtil _logger;
        private readonly DbConnectionManager _connection;

        public DatosProgLabRepository(LogUtil logger, DbConnectionManager connection)
        {
            _logger = logger;
            _connection = connection;
        }

        public async Task<ActionResult<object>> ActualizarProgLab(string mmcab_Surrogate)
        {
            try
            {
                _logger.LogInfo(General.Nombre_Servicio, "Inicia metodo - ActualizarDatosProgLab", General.Nombre_Metodo);
                SqlTransaction sqlTransaction = _connection.ObtenerConexion().BeginTransaction();
                string query = $"UPDATE DOLDTA.SAW_MIGRA_PROGLAB SET CSF_RESCHAR1_01='S' WHERE MMCAB_SURROGATE={mmcab_Surrogate}";

                using (SqlCommand command = new SqlCommand(query, _connection.ObtenerConexion(), sqlTransaction))
                {

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(General.Nombre_Servicio, "Error en el metodo - ActualizarDatosProgLab", ex, General.Nombre_Metodo);
                throw;
            }
            finally
            {
                _logger.LogInfo(General.Nombre_Servicio, "Finaliza metodo - ActualizarDatosProgLab", General.Nombre_Metodo);
                _connection.CerrarConexion();
            }
        }
    }
}

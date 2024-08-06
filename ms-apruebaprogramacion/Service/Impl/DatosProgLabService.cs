using Microsoft.AspNetCore.Mvc;
using ms_apruebaprogramacion.Constans;
using ms_apruebaprogramacion.Controllers.Contract;
using saff_core.utilitarios;

namespace ms_apruebaprogramacion.Service.Impl
{
    public class DatosProgLabService : IDatosProgLabService
    {
        private readonly IDatosProgLabRepository _repository;
        private readonly LogUtil _logger;

        DatosProgLabService(DatosProgLabRepository repository, LogUtil logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ActionResult<object>> ActualizarProgLab(string mmcab_Surrogate)
        {
            try
            {
                _logger.LogInfo(General.Nombre_Servicio, "Inicia metodo - ActualizarDatosProgLab", General.Nombre_Metodo);
                var res = await _repository.ActualizarProgLab(mmcab_Surrogate);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(General.Nombre_Servicio, "Error en el metodo - ActualizarDatosProgLab", ex, General.Nombre_Metodo);
                throw;
            }
            finally
            {
                _logger.LogInfo(General.Nombre_Servicio, "Finaliza metodo - ActualizarDatosProgLab", General.Nombre_Metodo);
            }
        }
    }
}

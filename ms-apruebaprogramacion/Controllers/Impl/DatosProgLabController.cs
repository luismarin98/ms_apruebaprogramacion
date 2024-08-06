using Microsoft.AspNetCore.Mvc;
using ms_apruebaprogramacion.Constans;
using ms_apruebaprogramacion.Controllers.Contract;
using Newtonsoft.Json;
using saff_core.constantes;
using saff_core.exception;
using saff_core.utilitarios;

namespace ms_apruebaprogramacion.Controllers.Impl
{
    [Route("v1/" + General.Tipo_Servicio + "/")]
    [Tags(General.Nombre_Servicio)]
    [ApiController]

    public class DatosProgLabController : Controller, IDatosProgLabController
    {
        private readonly LogUtil LogUtil;
        private readonly IDatosProgLabService _srv;

        public DatosProgLabController(LogUtil _logUtil, IDatosProgLabService srv) { LogUtil = _logUtil; _srv = srv; }

        [HttpGet(General.Nombre_Servicio + "/{mmcab_Surrogate}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [Produces(MimeType.JSON)]
        async Task<ActionResult<object>> ActualizarProgLab(string mmcab_Surrogate)
        {
            ObjectResult Response;
            try
            {
                LogUtil.LogInfo(General.Nombre_Servicio, "Inicia capacidad - controller", General.Nombre_Metodo);
                var rsp = await _srv.ActualizarProgLab(mmcab_Surrogate);
                Response = StatusCode(StatusCodes.Status200OK, rsp);
            }
            catch (BadHttpRequestException ex)
            {
                LogUtil.LogError(General.Nombre_Servicio, "Error de Lectura", ex, General.Nombre_Metodo);
                Response = StatusCode(StatusCodes.Status404NotFound, "Año anterior no existe");
            }
            catch (JsonSerializationException ex)
            {
                LogUtil.LogError(General.Nombre_Servicio, "Error de Lectura", ex, General.Nombre_Metodo);
                Response = StatusCode(StatusCodes.Status409Conflict, "Registros existente");
            }
            catch (ServiceException ex)
            {
                LogUtil.LogError(General.Nombre_Servicio, "Error de Negocio", ex, General.Nombre_Metodo);
                Response = DataValidator.ValidarResultadoExcepcion(ex);
            }
            catch (Exception ex)
            {
                LogUtil.LogError(General.Nombre_Servicio, "Error de Servicio", ex, General.Nombre_Metodo);
                Response = StatusCode(StatusCodes.Status500InternalServerError, MensajesDelSistema.ERROR_INTERNO_SERVIDOR);
            }
            finally
            {
                LogUtil.LogInfo(General.Nombre_Servicio, "Finaliza capacidad - controller", General.Nombre_Metodo);
            }
            return Response;
        }
    }
}

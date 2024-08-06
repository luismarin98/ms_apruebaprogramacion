using Microsoft.AspNetCore.Mvc;

namespace ms_apruebaprogramacion.Controllers.Contract
{
    public interface IDatosProgLabController
    {
        public Task<ActionResult<object>> ActualizarProgLab(string mmcab_Surrogate);
    }
}

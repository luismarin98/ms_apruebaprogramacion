using Microsoft.AspNetCore.Mvc;

namespace ms_apruebaprogramacion.Controllers.Contract
{
    public interface IDatosProgLabService
    {
        public Task<ActionResult<object>> ActualizarProgLab(string mmcab_Surrogate);
    }
}

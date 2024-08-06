using Microsoft.AspNetCore.Mvc;

namespace ms_apruebaprogramacion.Controllers.Contract
{
    public interface IDatosProgLabRepository
    {
        public Task<ActionResult<object>> ActualizarProgLab(string mmcab_Surrogate);
    }
}

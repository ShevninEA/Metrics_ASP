using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{

    // TODO: Домашнее задание [Пункт 2]
    // В проект агента сбора метрик добавьте контроллеры для сбора метрик, аналогичные
    // менеджеру сбора метрик.Добавьте методы для получения метрик с агента, доступные по
    //следующим путям
    // a. api/metrics/cpu/from/{fromTime}/to/{toTime} [ВЫПОЛНИЛИ ВМЕСТЕ]
    // b. api / metrics / dotnet / errors - count / from /{ fromTime}/ to /{ toTime}
    // c. api / metrics / network / from /{ fromTime}/ to /{ toTime}
    // d. api / metrics / hdd / left / from /{ fromTime}/ to /{ toTime} [ВЫПОЛНИЛИ ВМЕСТЕ]
    // e. api / metrics / ram / available / from /{ fromTime}/ to /{ toTime}
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }

    }
}

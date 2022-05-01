using API.ListManagement.database;
using API.ListManagement.EC;
using Library.ListManagement.Standard.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.ListManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(ILogger<AppointmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IEnumerable<ItemDTO> Get()
        {
            return new AppointmentEC().Get();
        }
        [HttpPost("AddOrUpdate")]
        public AppointmentDTO AddOrUpdate([FromBody] AppointmentDTO todo)
        {

            return new AppointmentEC().AddOrUpdate(todo);
        }

        [HttpPost("Delete")]
        public AppointmentDTO Delete([FromBody] DeleteItemDTO deleteItemDTO)
        {
            return new AppointmentEC().Delete(deleteItemDTO.IdToDelete);
        }
    }
}

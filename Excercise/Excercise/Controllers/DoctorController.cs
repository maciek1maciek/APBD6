using Excercise.Models;
using Excercise.Models.DTOs; 
using Excercise.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Excercise.Controllers
{  
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService; 

        public DoctorController( IDoctorService doctorService)
        { 
            _doctorService = doctorService;
        }

        [HttpGet]
        [Route("{idDoctor}")]
        public async Task<IActionResult> GetDoctorAsync(int idDoctor)
        {
            var doctorExist = await _doctorService.DoesDoctorExist(idDoctor);
            if (!doctorExist) {
                return NotFound();
            }
            var doctor = await _doctorService.GetDoctor(idDoctor);
            return Ok(doctor);
 
        }

        [HttpPost]
        [Route("/addDoctor")]
        public async Task<IActionResult> AddDoctor(DoctorPOST doctorPOST) 
        {
            var doctorExist = await _doctorService.DoesDoctorExist(doctorPOST.IdDoctor);
            if (!doctorExist) {
                return Conflict("taki identyfikator jest w bazie ");
            }
            var doctor = await _doctorService.GetDoctor(doctorPOST.IdDoctor);
            return Ok(doctor);
        }

 

    }
}

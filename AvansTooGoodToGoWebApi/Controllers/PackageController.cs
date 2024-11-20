using AvansTooGoodToGoWebApi.Models;
using Core.DomainServices.Repos.Intf;
using Microsoft.AspNetCore.Mvc;

namespace AvansTooGoodToGoWebApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PackageController : ControllerBase {
        private readonly IPackageRepository _packageRepository;
        private readonly IStudentRepository _studentRepository;

        public PackageController(IPackageRepository packageRepository, IStudentRepository studentRepository) {
            _packageRepository = packageRepository;
            _studentRepository = studentRepository;
        }

        [HttpPost(nameof(Reserve))]
        public IActionResult Reserve([FromBody] ReserveModel reserveModel) {
            var package = _packageRepository.GetById(reserveModel.PackageId);
            var student = _studentRepository.GetByEmail(reserveModel.StudentEmail);

            _packageRepository.AsignReserve(package, student);

            if (package == null) {
                return NotFound(new { message = "Package bestaat niet" });
            }
            if (student == null) {
                return NotFound(new { message = "Student bestaat niet" });
            }

            return Ok(new { message = "Reservering gemaakt" });
        }
    }
}
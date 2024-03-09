using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class autorlibroController : ControllerBase
    {
        private readonly Parcial1aContext _Parcial1aContext;

        public autorlibroController(Parcial1aContext parcial1aContext)
        {
            _Parcial1aContext = parcial1aContext;
        }

        [HttpGet]
        [Route("GetAllAutorLibros")]
        public IActionResult GetAllAutorLibros()
        {
            var autorLibros = _Parcial1aContext.Autorlibros.ToList();

            if (autorLibros.Count == 0)
            {
                return NotFound();
            }

            return Ok(autorLibros);
        }

        [HttpGet("{autorid}/{libroid}", Name = "GetAutorLibroById")]
        public IActionResult GetAutorLibroById(int autorid, int libroid)
        {
            var autorLibro = _Parcial1aContext.Autorlibros.Find(autorid, libroid);

            if (autorLibro == null)
            {
                return NotFound();
            }

            return Ok(autorLibro);
        }

        [HttpPost]
        [Route("CreateAutorLibro")]
        public IActionResult CreateAutorLibro([FromBody] Autorlibro autorLibro)
        {
            if (autorLibro == null)
            {
                return BadRequest();
            }

            _Parcial1aContext.Autorlibros.Add(autorLibro);
            _Parcial1aContext.SaveChanges();

            return CreatedAtRoute("GetAutorLibroById", new { autorid = autorLibro.Autorid, libroid = autorLibro.Libroid }, autorLibro);
        }

        [HttpPut("{autorid}/{libroid}")]
        public IActionResult UpdateAutorLibro(int autorid, int libroid, [FromBody] Autorlibro autorLibro)
        {
            if (autorLibro == null || autorLibro.Autorid != autorid || autorLibro.Libroid != libroid)
            {
                return BadRequest();
            }

            var existingAutorLibro = _Parcial1aContext.Autorlibros.Find(autorid, libroid);

            if (existingAutorLibro == null)
            {
                return NotFound();
            }

            existingAutorLibro.Orden = autorLibro.Orden;

            _Parcial1aContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{autorid}/{libroid}")]
        public IActionResult DeleteAutorLibro(int autorid, int libroid)
        {
            var autorLibro = _Parcial1aContext.Autorlibros.Find(autorid, libroid);

            if (autorLibro == null)
            {
                return NotFound();
            }

            _Parcial1aContext.Autorlibros.Remove(autorLibro);
            _Parcial1aContext.SaveChanges();

            return NoContent();
        }


    }
}

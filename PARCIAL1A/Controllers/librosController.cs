using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class librosController : ControllerBase
    {
        private readonly Parcial1aContext _Parcial1aContext;

        public librosController(Parcial1aContext parcial1aContext)
        {
            _Parcial1aContext = parcial1aContext;
        }

        [HttpGet]
        [Route("GetAllLibros")]
        public IActionResult GetAllLibros()
        {
            var libros = _Parcial1aContext.Libros.ToList();

            if (libros.Count == 0)
            {
                return NotFound();
            }

            return Ok(libros);
        }

        [HttpGet("{id}", Name = "GetLibroById")]
        public IActionResult GetLibroById(int id)
        {
            var libro = _Parcial1aContext.Libros.Find(id);

            if (libro == null)
            {
                return NotFound();
            }

            return Ok(libro);
        }

        [HttpPost]
        [Route("CreateLibro")]
        public IActionResult CreateLibro([FromBody] Libro libro)
        {
            if (libro == null)
            {
                return BadRequest();
            }

            _Parcial1aContext.Libros.Add(libro);
            _Parcial1aContext.SaveChanges();

            return CreatedAtRoute("GetLibroById", new { id = libro.Id }, libro);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLibro(int id, [FromBody] Libro libro)
        {
            if (libro == null || libro.Id != id)
            {
                return BadRequest();
            }

            var existingLibro = _Parcial1aContext.Libros.Find(id);

            if (existingLibro == null)
            {
                return NotFound();
            }

            existingLibro.Titulo = libro.Titulo;

            _Parcial1aContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLibro(int id)
        {
            var libro = _Parcial1aContext.Libros.Find(id);

            if (libro == null)
            {
                return NotFound();
            }

            _Parcial1aContext.Libros.Remove(libro);
            _Parcial1aContext.SaveChanges();

            return NoContent();
        }

    }
}

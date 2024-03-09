using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class autoresController : ControllerBase
    {
        private readonly Parcial1aContext _Parcial1aContext;

        public autoresController(Parcial1aContext parcial1aContext)
        {
            _Parcial1aContext = parcial1aContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var autores = _Parcial1aContext.Autores.ToList();

            if (autores.Count == 0)
            {
                return NotFound();
            }

            return Ok(autores);
        }

        [HttpGet("{id}", Name = "GetAutorById")]
        public IActionResult GetById(int id)
        {
            var autor = _Parcial1aContext.Autores.Find(id);

            if (autor == null)
            {
                return NotFound();
            }

            return Ok(autor);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] Autore autor)
        {
            if (autor == null)
            {
                return BadRequest();
            }

            _Parcial1aContext.Autores.Add(autor);
            _Parcial1aContext.SaveChanges();

            return CreatedAtRoute("GetAutorById", new { id = autor.Id }, autor);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Autore autor)
        {
            if (autor == null || autor.Id != id)
            {
                return BadRequest();
            }

            var existingAutor = _Parcial1aContext.Autores.Find(id);

            if (existingAutor == null)
            {
                return NotFound();
            }

            existingAutor.Nombre = autor.Nombre;

            _Parcial1aContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var autor = _Parcial1aContext.Autores.Find(id);

            if (autor == null)
            {
                return NotFound();
            }

            _Parcial1aContext.Autores.Remove(autor);
            _Parcial1aContext.SaveChanges();

            return NoContent();
        }

    }
}

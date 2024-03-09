using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class extrasController : ControllerBase
    {
        private readonly Parcial1aContext _Parcial1aContext;

        public extrasController(Parcial1aContext parcial1aContext)
        {
            _Parcial1aContext = parcial1aContext;
        }

        [HttpGet]
        [Route("GetLibrosByAutor")]
        public IActionResult GetLibrosByAutor(string nombreAutor)
        {
            var librosPorAutor = _Parcial1aContext.Libros
                .Join(
                    _Parcial1aContext.Autorlibros,
                    libro => libro.Id,
                    autorlibro => autorlibro.Libroid,
                    (libro, autorlibro) => new { Libro = libro, AutorLibro = autorlibro })
                .Join(
                    _Parcial1aContext.Autores,
                    combined => combined.AutorLibro.Autorid,
                    autor => autor.Id,
                    (combined, autor) => new { combined.Libro, Autor = autor })
                .Where(result => result.Autor.Nombre == nombreAutor)
                .Select(result => new { result.Libro, result.Autor.Nombre }) 
                .ToList();

            if (librosPorAutor.Count == 0)
            {
                return NotFound();
            }

            return Ok(librosPorAutor);
        }



        [HttpGet]
        [Route("GetUltimos20PostsByAutor")]
        public IActionResult GetUltimos20PostsByAutor(string nombreAutor)
        {
            var ultimos20PostsPorAutor = _Parcial1aContext.Posts
                .Where(post => post.Autor.Nombre == nombreAutor)
                .OrderByDescending(post => post.Fechapublicacion)
                .Take(20)
                .Select(post => new
                {
                    post.Id,
                    post.Titulo,
                    post.Contenido,
                    post.Fechapublicacion,
                    post.Autorid,
                    Autor = post.Autor != null ? post.Autor.Nombre : null
                })
                .ToList();

            if (ultimos20PostsPorAutor.Count == 0)
            {
                return NotFound();
            }

            return Ok(ultimos20PostsPorAutor);
        }


        [HttpGet]
        [Route("GetPostsByLibro")]
        public IActionResult GetPostsByLibro(string tituloLibro)
        {
            var postsPorLibro = _Parcial1aContext.Posts
                .Join(
                    _Parcial1aContext.Autorlibros,
                    post => post.Autorid,
                    autorlibro => autorlibro.Autorid,
                    (post, autorlibro) => new { Post = post, AutorLibro = autorlibro })
                .Join(
                    _Parcial1aContext.Libros,
                    combined => combined.AutorLibro.Libroid,
                    libro => libro.Id,
                    (combined, libro) => new { combined.Post, Libro = libro })
                .Join(
                    _Parcial1aContext.Autores,
                    combined => combined.Post.Autorid,
                    autor => autor.Id,
                    (combined, autor) => new
                    {
                        combined.Post.Id,
                        combined.Post.Titulo,
                        combined.Post.Contenido,
                        combined.Post.Fechapublicacion,
                        combined.Post.Autorid,
                        Libro = combined.Libro,
                        Autor = autor.Nombre
                    })
                .Where(result => result.Libro.Titulo == tituloLibro)
                .ToList();

            if (postsPorLibro.Count == 0)
            {
                return NotFound();
            }

            return Ok(postsPorLibro);
        }




    }
}

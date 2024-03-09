using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class postController : ControllerBase
    {
        private readonly Parcial1aContext _Parcial1aContext;

        public postController(Parcial1aContext parcial1aContext)
        {
            _Parcial1aContext = parcial1aContext;
        }

        [HttpGet]
        [Route("GetAllPosts")]
        public IActionResult GetAllPosts()
        {
            var posts = _Parcial1aContext.Posts.ToList();

            if (posts.Count == 0)
            {
                return NotFound();
            }

            return Ok(posts);
        }

        [HttpGet("{id}", Name = "GetPostById")]
        public IActionResult GetPostById(int id)
        {
            var post = _Parcial1aContext.Posts.Find(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        [Route("CreatePost")]
        public IActionResult CreatePost([FromBody] Post post)
        {
            if (post == null)
            {
                return BadRequest();
            }

            _Parcial1aContext.Posts.Add(post);
            _Parcial1aContext.SaveChanges();

            return CreatedAtRoute("GetPostById", new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] Post post)
        {
            if (post == null || post.Id != id)
            {
                return BadRequest();
            }

            var existingPost = _Parcial1aContext.Posts.Find(id);

            if (existingPost == null)
            {
                return NotFound();
            }

            existingPost.Titulo = post.Titulo;
            existingPost.Contenido = post.Contenido;
            existingPost.Fechapublicacion = post.Fechapublicacion;

            _Parcial1aContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _Parcial1aContext.Posts.Find(id);

            if (post == null)
            {
                return NotFound();
            }

            _Parcial1aContext.Posts.Remove(post);
            _Parcial1aContext.SaveChanges();

            return NoContent();
        }


    }
}

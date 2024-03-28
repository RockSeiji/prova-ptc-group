using Cast.Models;
using Cast.services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cast.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("all")]
        public ActionResult GetAll()
        {
            var posts  = _postService.GetAll();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(Guid id)
        {
            var post = _postService.GetById(id);
            return Ok(post);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public ActionResult Add([FromBody] Post post)
        {
            _postService.Add(post);
            return Ok("Operação realizada com sucesso.");
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public ActionResult Update([FromBody] Post post)
        {
            _postService.Update(post);
            return Ok("Operação realizada com sucesso.");
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            _postService.Delete(id);
            return Ok("Operação realizada com sucesso.");
        }
    }
}

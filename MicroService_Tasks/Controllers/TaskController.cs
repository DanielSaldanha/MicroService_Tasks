using MicroService_Tasks.Data;
using MicroService_Tasks.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace MicroService_Tasks.Controllers
{
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        [HttpGet("GET")]
        public async Task<ActionResult> get()
        {
            var res = await _context.Lista.ToListAsync();
            if (res == null) return NotFound("erro ao buscar dados");

            return Ok(res);
        }

        [HttpPost("POST")]
        public async Task<ActionResult> post([FromBody] Tarefa tarefa)
        {
            if (tarefa == null)
            {
                return BadRequest("Dados inválidos.");
            }
            _context.Lista.Add(tarefa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(post), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> put(int id, [FromBody] Tarefa tarefa)
        {
            _context.Entry(tarefa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var res = await _context.Lista.FindAsync(id);
            if (res == null) return NotFound("usuario inexistente");
            _context.Lista.Remove(res);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

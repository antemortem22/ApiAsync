using Crud.Domain.Request;
using Crud.Domain.Response;
using Crud.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Crud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : ControllerBase
    {
        private readonly IMDBContext _context;

        public DirectorController(IMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDirectores([FromQuery] GetDirectoresRequest request)
        {
            int skip = request.Skip;
            int take = request.Take;

            var result = await _context.Directores.Skip(skip).Take(take).ToListAsync();
            int count = await _context.Directores.CountAsync();

            var response = new GetDirectoresResponse()
            {
                Directores = result,
                Total = count
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirectorById([FromRoute] int id)
        {
            var result = await _context.Directores.FirstOrDefaultAsync(w => w.IdDirector == id);

            if (result == null)
                return NotFound(new { Error = $"No se pudo encontrar el id {id}" });

            return Ok(result);
        }

        [HttpGet("peliculas")]
        public async Task<IActionResult> GetPeliculasByDirector([FromQuery] int idDirector)
        {
            var result = await _context.Directores
                .Where(w => w.IdDirector == idDirector)
                .Include(i => i.Peliculas)
                .ToListAsync();

            return Ok(result);
        }
    }
}

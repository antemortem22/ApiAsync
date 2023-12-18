using Crud.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Crud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasGeneroController : ControllerBase
    {
        private readonly IMDBContext _context;

        public PeliculasGeneroController(IMDBContext context)
        {
            _context = context;
        }

        [HttpGet("{genero}")]
        public async Task<IActionResult> GetPeliculasByGenero([FromRoute] string genero)
        {
            var gen = await _context.Generos.FirstOrDefaultAsync(g => g.Nombre == genero);

            if (gen == null)
                return NotFound(new { Error = $"No se pudo encontrar el género {genero}" });

            var result = await _context.Generos
                .Where(w => w.IdGenero == gen.IdGenero)
                .SelectMany(a => a.PeliculasGeneros
                    .Select(peli => new
                    {
                        a.IdGenero,
                        NombreGenero = a.Nombre,
                        TituloPelicula = peli.IdPeliculaNavigation.Titulo
                    })
                )
                .ToListAsync();

            return Ok(result);
        }
    }
}

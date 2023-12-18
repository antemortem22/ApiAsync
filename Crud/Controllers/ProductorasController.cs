using Crud.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class ProductorasController : ControllerBase
    {
        private readonly IMDBContext _context; //Inyeccion de dependencia 
        public ProductorasController(IMDBContext context)
        {
            _context = context;
        }

        [HttpGet("{productora}")]
        public async Task<IActionResult> GetPeliculasByProductoras([FromRoute] string productora)
        {
            var produ = await _context.Productoras.FirstOrDefaultAsync(p => p.Nombre == productora);
            if (produ == null) return NotFound(new { Error = $"No se pudo encontrar la productora {produ}" });

            var result = await _context.Productoras
                                   .Where(w => w.IdProductora == produ.IdProductora)
                                   .Select(productora => new
                                   {
                                       productora.IdProductora,
                                       productora.Nombre,
                                       RecaudacionTotal = productora.Peliculas.Sum(pelicula => pelicula.Recaudacion),
                                       Peliculas = productora.Peliculas.Select(pelicula => new
                                       {
                                           pelicula.IdPelicula,
                                           pelicula.Titulo,
                                           pelicula.Recaudacion
                                       })
                                   })
                                   .ToListAsync();

            return Ok(result);
        }
    }
}

using Crud.Domain.Entities;
namespace Crud.Domain.Response
{
    public class GetDirectoresResponse
    {
        public List<Directore> Directores { get; set; }
        public int Total {  get; set; }

    }
}

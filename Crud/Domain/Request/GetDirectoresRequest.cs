namespace Crud.Domain.Request
{
    public class GetDirectoresRequest
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 0; //=> asocia el 0 por default si no se le pasan valores
    }
}

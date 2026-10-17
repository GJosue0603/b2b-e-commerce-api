
namespace b2b_e_commerce_models
{
    public class ResultadoPaginado<T>
    {
        public long CantidadTotalDeItems { get; set; }

        public IEnumerable<T> Items { get; set; }

        public ResultadoPaginado()
        {
            Items = new List<T>();
            CantidadTotalDeItems = 0L;
        }

        public ResultadoPaginado(IEnumerable<T> items, long cantidadTotalDeItems)
        {
            Items = items ?? new List<T>();
            CantidadTotalDeItems = cantidadTotalDeItems;
        }
    }
}
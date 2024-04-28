using System.Data;
using Frontend.DataAccess;

namespace Frontend.Logic
{
    public class ListaVuelosLogic
    {
        private ListaVuelosData listaVuelosData;

        public ListaVuelosLogic()
        {
            listaVuelosData = new ListaVuelosData();
        }
        public DataTable ListadoVuelos(int operacion)
        {
            return listaVuelosData.ListadoVuelos(operacion);
        }
        public DataTable FiltrarVuelos(int op, string valor)
        {
            return listaVuelosData.FiltrarVuelos(op, valor);
        }
        public DataTable FiltrarVuelosVarios(string aeropuerto, string destino, string fecha)
        {
            return listaVuelosData.FiltrarVuelosVarios(aeropuerto, destino, fecha);
        }
        public DataTable CargarDestinosAeropuerto(string nombreAeropuerto)
        {
            return listaVuelosData.CargarDestinosAeropuerto(nombreAeropuerto);
        }
    }
}

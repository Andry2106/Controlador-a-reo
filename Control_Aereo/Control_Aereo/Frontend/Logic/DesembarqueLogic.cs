using Frontend.DataAccess;
using System.Data;

namespace Frontend.Logic
{
    public class DesembarqueLogic
    {
        private DesembarqueData desembarqueData;

        public DesembarqueLogic()
        {
            desembarqueData = new DesembarqueData();
        }

        public DataTable ObtenerHangares()
        {
            return desembarqueData.ObtenerHangares();
        }
        public DataTable CargarHangaresConAviones()
        {
            return desembarqueData.CargarHangaresConAviones();
        }
        public DataTable CargarHangaresSinAviones()
        {
            return desembarqueData.CargarHangaresSinAviones();
        }
        public DataTable CargarAvionesPorHangar(int idHangar)
        {
            return desembarqueData.CargarAvionesPorHangar(idHangar);
        }
        public void AgregarAvionEnHangar(string nombreHangar, string numeroDeRegistroAvion)
        {
            desembarqueData.AgregarAvionEnHangar(nombreHangar, numeroDeRegistroAvion);
        }
        public DataTable CargarTodosLosAviones()
        {
            return desembarqueData.CargarTodosLosAviones();
        }
        public void EliminarAvionDeHangar(string nombreHangar, string numeroDeRegistroAvion)
        {
            desembarqueData.EliminarAvionDeHangar(nombreHangar, numeroDeRegistroAvion);
        }
        public DataTable BuscarModelosAvionesPorHangar(string nombreHangar)
        {
            return desembarqueData.BuscarModelosAvionesPorHangar(nombreHangar);
        }

        public string DesembarcarPasajero(int idEmbarque, int idPuerta)
        {
            return desembarqueData.DesembarcarPasajero(idEmbarque, idPuerta);
        }


        public DataTable CargarEmbarque()
        {
            return desembarqueData.CargarEmbarque();
        }


        public DataTable ObtenerPuertasActivas()
        {
            return desembarqueData.ObtenerPuertasActivas();
        }
    }
}


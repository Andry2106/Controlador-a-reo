using Frontend.DataAccess;
using System.Data;

namespace Frontend.Logic
{
    public class EmbarcamientoLogic
    {
        private EmbarcamientoData embarcamientoData;

        public EmbarcamientoLogic()
        {
            embarcamientoData = new EmbarcamientoData();
        }

        public DataTable ListadoPasajeros()
        {
            return embarcamientoData.ListadoPasajeros();
        }

        public DataTable ListadoEquipamiento()
        {
            return embarcamientoData.ListadoEquipamiento();
        }

        public void InsertarEmbarque(int IDEquipaje, int IDPasajero)
        {
            embarcamientoData.InsertarEmbarque(IDEquipaje, IDPasajero);
        }

        public DataTable ObtenerDatosEmbarque()
        {
            return embarcamientoData.ObtenerDatosEmbarque();
        }

        public DataTable ObtenerPuertasActivas()
        {
            return embarcamientoData.ObtenerPuertasActivas();
        }

        public DataTable ObtenerDatosAvionPorID(int IDAvion)
        {
            return embarcamientoData.ObtenerDatosAvionPorID(IDAvion);
        }

        public string InsertarEmbarcamiento(int IDAvion, int IDEmbarque, int IDPuerta)
        {
            string resultado = embarcamientoData.InsertarEmbarcamiento(IDAvion, IDEmbarque, IDPuerta);

            return resultado;
        }

        public string InsertarEmbarcamientoMasivo(int IDAvion, int IDPuerta)
        {
            string resultado = embarcamientoData.InsertarEmbarcamientoMasivo(IDAvion, IDPuerta);

            return resultado;

        }

        public string InsertarEmbarqueMasivo()
        {
            embarcamientoData.InsertarEmbarqueMasivo();

            return "Embarcamiento masivo realizado correctamente.";
        }

        public DataTable ObtenerEmbarcamientosPorAvion(int IDAvion)
        {
            return embarcamientoData.ObtenerEmbarcamientosPorAvion(IDAvion);
        }

        public void EliminarEmbarcamientoPorID(int IDEmbarque)
        {
            embarcamientoData.EliminarEmbarcamientoPorID(IDEmbarque);
        }

        public void EliminarEmbarcamientosPorIDAvion(int IDAvion)
        {
            embarcamientoData.EliminarEmbarcamientosPorIDAvion(IDAvion);
        }

        public void EliminarEmbarquePorIDEmbarque(int IDEmbarque)
        {
            embarcamientoData.EliminarEmbarquePorIDEmbarque(IDEmbarque);
        }

        public void EliminarEmbarquesNoVinculados()
        {
            embarcamientoData.EliminarEmbarquesNoVinculados();
        }
    }
}

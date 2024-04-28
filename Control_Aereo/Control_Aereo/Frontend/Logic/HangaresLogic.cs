using Frontend.DataAccess;
using System;
using System.Data;

namespace Frontend.Logic
{
    public class HangaresLogic
    {
        private HangaresData hangaresData;

        public HangaresLogic()
        {
            hangaresData = new HangaresData();
        }

        public DataTable MantenimientoHangares(int opcion, int idHangar, int idtipo, string nombre, string ubicacion, int capacidad, string estado, decimal tamaño, decimal altura, int idAeropuerto)
        {
            return hangaresData.MantenimientoHangares(opcion, idHangar, idtipo, nombre, ubicacion, capacidad, estado,tamaño,altura, idAeropuerto);
        }
        public int ObtenerIdHangarPorNombre(string nombreHangar)
        {
            return hangaresData.ObtenerIdHangarPorNombre(nombreHangar);
        }
        public DataTable ObtenerAeropuertos()
        {
            return hangaresData.ObtenerAeropuertos();
        }
        public int ObtenerIdAeropuertoPorNombre(string nombreAeropuerto)
        {
            return hangaresData.ObtenerIdAeropuertoPorNombre(nombreAeropuerto);
        }
        public int ObtenerIdTipoHangarPorNombre(string nombreTipoHangar)
        {
            return hangaresData.ObtenerIdTipoHangarPorNombre(nombreTipoHangar);
        }
        public DataTable ObtenerTiposHangares()
        {
            return hangaresData.ObtenerTiposHangares();
        }
        public bool HangarEnUso(int idHangar)
        {
            return hangaresData.HangarEnUso(idHangar);
        }
    }
}

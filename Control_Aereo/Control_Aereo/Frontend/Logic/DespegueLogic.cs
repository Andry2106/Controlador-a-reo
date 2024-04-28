using Frontend.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;

namespace Frontend.Logic
{
    public class DespegueLogic
    {
        private DespegueData despegueData;

        public DespegueLogic()
        {
            despegueData = new DespegueData();
        }

        public DataTable ObtenerAvionPorID(int avionID)
        {
            return despegueData.ObtenerAvionPorID(avionID);
        }

        public DataTable ObtenerVueloPorID(int vueloID)
        {
            return despegueData.ObtenerVueloPorID(vueloID);
        }

        public void InsertarDespegue(string horaDespegue, string origen, string destino, int numeroVuelo, int idPista, int idPuerta)
        {
            despegueData.InsertarDespegue(horaDespegue, origen, destino, numeroVuelo, idPista, idPuerta);
        }

        public void CambiarEstadoVuelo(int numeroVuelo, int nuevoEstado)
        {
            despegueData.CambiarEstadoVuelo(numeroVuelo, nuevoEstado);
        }
        public void PasarDespegue(int numeroVuelo)
        {
            despegueData.PasarDespegue(numeroVuelo);
        }
        public void BorrarVueloPorNumero(int numeroVuelo)
        {
            despegueData.BorrarVueloPorNumero(numeroVuelo);
        }
        public void InsertarValidacionesDespegue02(int NumeroVuelo)
        {
            despegueData.SP_Insertar_ValidacionesDespegue02(NumeroVuelo);
        }
        public int ValidarValidacionesDespegue(int numeroVuelo)
        {
            int resultado = 0; // 0 por defecto, podría representar un estado indeterminado

            bool validacionesCumplidas = despegueData.ValidarValidacionesDespegue(numeroVuelo);

            if (validacionesCumplidas)
            {
                resultado = 1; // 1 para indicar que las validaciones se cumplieron
            }
            else
            {
                resultado = -1; // -1 para indicar que las validaciones no se cumplieron
            }

            return resultado;
        }
        public DataTable BuscarValidacionesFallidas(int numeroVuelo)
        {
            DataTable dataTable = new DataTable();

            try
            {
                dataTable = despegueData.BuscarValidacionesFallidas(numeroVuelo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar validaciones fallidas: " + ex.Message);
            }

            return dataTable;
        }
    }
}

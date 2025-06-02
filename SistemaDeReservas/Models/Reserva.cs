using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeReservas.Models
{
    public class Reserva
    {
        public DateTime DataEntrada { get; set; }
        public DateTime DataSaida { get; set; }

        public int PrecoDiaria { get; set; }

        public int CustoTotal { get; private set; }
        
        public int NumeroQuarto { get; set; }

        public Reserva(int numeroQuarto,int precoDiaria, DateTime dataEntrada, DateTime dataSaida)
        {
            NumeroQuarto = numeroQuarto;
            DataEntrada = dataEntrada;
            DataSaida = dataSaida;
            PrecoDiaria = precoDiaria;
            CalcularCustoTotal();


        }
        
        private void CalcularCustoTotal()
            {
                TimeSpan periodo = DataSaida - DataEntrada;
                CustoTotal = PrecoDiaria * periodo.Days;
            }

    }
    
    
}
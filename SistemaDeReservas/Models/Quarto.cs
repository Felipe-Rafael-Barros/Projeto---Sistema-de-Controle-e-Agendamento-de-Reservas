using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeReservas.Models
{
    public class Quarto
    {

        public int Numero { get; set; } // Número do quarto
        public String Tipo { get; set; } // Tipo, alto padrão, medio, baixo... exemplo

        public bool Disponivel { get; set; } = true; // Inicio com todos disponíveis

        public int PrecoDiaria { get; set; }

        public Quarto(int numero, string tipo, int precodiaria, bool disponivel)
        
        {
            Numero = numero;
            Tipo = tipo;
            PrecoDiaria = precodiaria;
            Disponivel = disponivel;
            
        }
    }
}
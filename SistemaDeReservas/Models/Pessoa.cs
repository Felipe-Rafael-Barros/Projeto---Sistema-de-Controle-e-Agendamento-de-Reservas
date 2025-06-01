using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeReservas.Models
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public String CPF { get; set; }
        public int NumeroQuarto { get; set; }

        public DateTime DataCadastro { get; set; }

        public Pessoa(string nome, int idade, String cpf, int numeroQuarto, DateTime dataCadastro)
        {
            Nome = nome;
            Idade = idade;
            CPF = cpf;
            NumeroQuarto = numeroQuarto;
            DataCadastro = dataCadastro;
        }

    }


}
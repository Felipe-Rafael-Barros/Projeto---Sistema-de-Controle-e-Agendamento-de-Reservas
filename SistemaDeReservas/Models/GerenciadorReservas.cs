using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeReservas.Models
{
    public class GerenciadorReservas
    {
        private readonly List<Reserva> _reservas;
        private readonly GerenciadorPessoas _gerenciadorPessoas;

        private readonly GerenciadorQuartos _gerenciadorQuartos;



        public GerenciadorReservas(GerenciadorPessoas GerenciadorDePessoas, GerenciadorQuartos GerenciadorDeQuartos)
        {
            //Fazendo cada reserva receber caracteristicas de pessoas e quarto
            _reservas = new List<Reserva>();
            _gerenciadorPessoas = GerenciadorDePessoas;
            _gerenciadorQuartos = GerenciadorDeQuartos;

        }

        public String ListarPessoasCadastradas()
        {
            return _gerenciadorPessoas.ListarPessoasCadastradas();
        }
        public String ListarQuartos()
        {
            return _gerenciadorQuartos.ListarQuartosFormatado();
        }

        public void CadastrarNovaReserva()
        {
            Console.WriteLine("Vamos iniciar o cadastro!");
            

            Console.WriteLine("Escolha o quarto dentre as opções disponíveis");
            Console.WriteLine(ListarQuartos());

            int NumeroQuarto = _gerenciadorQuartos.AlterarDisponibilidade();

            int PrecoDiaria = _gerenciadorQuartos.DiariaDoQuarto(NumeroQuarto);
            
            Console.WriteLine("Data de Saida? formato: DD/MM/AAAA"); //Precisa verificar ainda as datas
            DateTime DataSaida = DateTime.Parse(Console.ReadLine());

            DateTime DataEntrada = DateTime.Now;

            _gerenciadorPessoas.CadastrarPessoa(NumeroQuarto,DataEntrada);

        

            new Reserva(PrecoDiaria, DataEntrada, DataSaida);
        }
    }
}
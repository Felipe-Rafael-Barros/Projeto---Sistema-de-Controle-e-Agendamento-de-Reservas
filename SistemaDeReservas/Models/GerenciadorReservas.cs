using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace SistemaDeReservas.Models
{
    public class GerenciadorReservas
    {
        private readonly List<Reserva> _reservas;
        private readonly GerenciadorPessoas _gerenciadorPessoas;

        private readonly GerenciadorQuartos _gerenciadorQuartos;

        private readonly List<string> TitularesQuartos;

        private readonly List<int> NumeroQuartos;

        private readonly List<string> TipoQuarto;



        public GerenciadorReservas(GerenciadorPessoas GerenciadorDePessoas, GerenciadorQuartos GerenciadorDeQuartos)
        {
            //Fazendo cada reserva receber caracteristicas de pessoas e quarto
            _reservas = new List<Reserva>();
            _gerenciadorPessoas = GerenciadorDePessoas;
            _gerenciadorQuartos = GerenciadorDeQuartos;

            TitularesQuartos = new List<string>();
            NumeroQuartos = new List<int>();
            TipoQuarto = new List<string>();

        }

        public String ListarPessoasCadastradas()
        {
            return _gerenciadorPessoas.ListarPessoasCadastradas();
        }
        public String ListarQuartos()
        {
            return _gerenciadorQuartos.ListarQuartosFormatado();
        }

        public String ListarReservas()
        {
            if (!_reservas.Any())
                return "Nenhuma reserva cadastrada.";

            var tabela = new StringBuilder();

            
            tabela.AppendLine("┌──────────┬──────────────┬─────────────┬─────────────┬────────────┬────────────┐");
            tabela.AppendLine("│ Titular  │    Tipo      │  Nº Quarto  │ Preço Total │Data Entrada│ Data Saida │");
            tabela.AppendLine("│          │              │             │             │            │            │");
            tabela.AppendLine("├──────────┼──────────────┼─────────────┼─────────────┤────────────┤────────────┤");

            for (int i = 0; i < _reservas.Count; i++)
            {

                tabela.AppendLine($"│ {LimitString(TitularesQuartos[i], 7),-7}│ {LimitString(TipoQuarto[i], 12),-12} │ {LimitString(NumeroQuartos[i].ToString(), 11),-11} │R$ {LimitString(_reservas[i].CustoTotal.ToString(), 10),-10}│ {_reservas[i].DataEntrada:dd/MM/yyyy} │{_reservas[i].DataSaida:dd/MM/yyyy}  │");

                tabela.AppendLine("└──────────┴──────────────┴─────────────┴─────────────┴────────────┴────────────┘");


            }
            return tabela.ToString();
        }
        private string LimitString(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty.PadRight(maxLength);
            
            return input.Length <= maxLength ? input : input.Substring(0, maxLength - 1) + "...";
        }

        public void CadastrarNovaReserva()
        {
            Console.WriteLine("Vamos iniciar o cadastro!");


            Console.WriteLine("Escolha o quarto dentre as opções disponíveis");
            Console.WriteLine(ListarQuartos());

            int NumeroQuarto = _gerenciadorQuartos.AlterarDisponibilidade();

            (int PrecoDiaria, String TipoDoQuarto) = _gerenciadorQuartos.DiariaDoQuarto(NumeroQuarto);

            Console.WriteLine("Data de Saida? formato: DD/MM/AAAA"); //Precisa verificar ainda as datas
            DateTime DataSaida = DateTime.Parse(Console.ReadLine());

            DateTime DataEntrada = DateTime.Now;

            string Titular = _gerenciadorPessoas.CadastrarPessoa(NumeroQuarto, DataEntrada); //Cadastra as pessoas e me retorna a primeira cadastrasda(Titular do Quarto)

            TitularesQuartos.Add(Titular);
            NumeroQuartos.Add(NumeroQuarto);
            TipoQuarto.Add(TipoDoQuarto);

            _reservas.Add(new Reserva(PrecoDiaria, DataEntrada, DataSaida));
        }

 
    }
}
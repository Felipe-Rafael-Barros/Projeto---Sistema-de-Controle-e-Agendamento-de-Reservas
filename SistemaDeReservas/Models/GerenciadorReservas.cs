using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Diagnostics;

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

            int NumeroQuarto = _gerenciadorQuartos.AlterarDisponibilidade(true, 0);

            (int PrecoDiaria, String TipoDoQuarto) = _gerenciadorQuartos.DiariaDoQuarto(NumeroQuarto);

            Console.WriteLine("Data de Saida? formato: DD/MM/AAAA"); //Precisa verificar ainda as datas
            DateTime DataSaida = DateTime.Parse(Console.ReadLine());

            DateTime DataEntrada = DateTime.Now;

            string Titular = _gerenciadorPessoas.CadastrarPessoa(NumeroQuarto, DataEntrada); //Cadastra as pessoas e me retorna a primeira cadastrasda(Titular do Quarto)

            TitularesQuartos.Add(Titular);
            NumeroQuartos.Add(NumeroQuarto);
            TipoQuarto.Add(TipoDoQuarto);

            _reservas.Add(new Reserva(NumeroQuarto, PrecoDiaria, DataEntrada, DataSaida));
        }

        public string CancelarReserva()
        {
            if (!_reservas.Any())
                return "Nenhuma reserva cadastrada.";


            int NumeroAlvo = 0;

            Console.WriteLine(ListarQuartos());
            Console.WriteLine("Qual o quarto que você quer cancelar a reserva?");
            int NumeroDoQuarto = int.Parse(Console.ReadLine());

            for (int i = 0; i < _reservas.Count; i++)
            {
                if (NumeroDoQuarto == NumeroQuartos[i])
                {
                    NumeroAlvo = i;

                    _gerenciadorQuartos.AlterarDisponibilidade(false, NumeroDoQuarto);
                    _gerenciadorPessoas.RemoverPessoasQuarto(NumeroDoQuarto);
                    _reservas.RemoveAll(r => r.NumeroQuarto == NumeroDoQuarto);
                    TitularesQuartos.RemoveAt(NumeroAlvo);
                    NumeroQuartos.RemoveAt(NumeroAlvo);
                    TipoQuarto.RemoveAt(NumeroAlvo);
                    return "Reserva do Quarto Cancelada";
                }
            }
            Console.WriteLine("Quarto Não encontrado ou Não está Ocupado, tente novamente.");




            return "";
        }


        public void AtualizarReserva()
        {

            Console.WriteLine(@"
            ╔═══════════════════════════════════════════════════════════════════════╗
            ║                                                                       ║
            ║   [1] Deseja atualizar um nova data de saida                          ║
            ║   [2] Deseja adicionar ou remover Pessoas de um mesmo quarto          ║
            ║   [ ] Pressione qualquer outra tecla voltar ao menu                   ║
            ║                                                                       ║
            ╚═══════════════════════════════════════════════════════════════════════╝
            ");
            string Opcao = Console.ReadLine();

            switch (Opcao)
            {
                //Muda data de Saida
                case "1":
                    bool Ativo = true;
                    while (Ativo) //Rodará até que se acha o quarto válido
                    {
                        Console.WriteLine(ListarQuartos());
                        Console.WriteLine("Dentre os quartos ocupados, qual você deseja modificiar a data limite de térmido da reserva");
                        int QuartoPedido = int.Parse(Console.ReadLine());


                        if (NumeroQuartos.Count > 0)
                        {
                            for (int i = 0; i < NumeroQuartos.Count; i++)
                            {
                                if (NumeroQuartos[i] == QuartoPedido)
                                {
                                    DateTime novaData;
                                    Console.WriteLine("Data de saida antiga: " + _reservas[i].DataSaida);
                                    Console.WriteLine("Escolha nova data de saida (dd/MM/yyyy):");
                                    while (!DateTime.TryParse(Console.ReadLine(), out novaData))
                                    {
                                        Console.WriteLine("Formato inválido. Digite novamente (dd/MM/yyyy):");
                                    }
                                    _reservas[i].DataSaida = novaData;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Não há quartos cadastrados");
                            Ativo = false;
                        }
                    }
                    break;
                //Adicionar ou Remover Pessoa de um quarto
                case "2":
                    Console.WriteLine("Para adicionar pressione 1, para remover pressione 2");
                    string Opcao2 = Console.ReadLine();
                    if (Opcao2 == "1")
                    {
                        Console.WriteLine("Quantas pessoas quer adicionar no quarto?");
                        int NumeroPessoasAdd = int.Parse(Console.ReadLine());

                        for (int i = 0; i < NumeroPessoasAdd; i++)
                        {
                            _gerenciadorPessoas.AdicionarUmaPessoa();
                        }
                    }
                    else if (Opcao2 == "2")
                    {
                        Console.WriteLine("De qual quarto deseja remover as pessoas");
                        int NumeroQuarto = int.Parse(Console.ReadLine());
                        _gerenciadorPessoas.RemoverPessoaEspecificaQuarto(NumeroQuarto);
                    }



                    else
                    {
                        Console.WriteLine("Opção inválida");
                    }
                        
                    break;
                default:
                    break;
            }
            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace SistemaDeReservas.Models
{
    public class GerenciadorPessoas
    {
        private readonly List<Pessoa> _pessoas;

        public GerenciadorPessoas() //Iniciar a classe com uma lista de pessoas
        {
            _pessoas = new List<Pessoa> { };
        }

        public String CadastrarPessoa(int NumeroQuarto, DateTime DataCadastro)
        {
            Console.WriteLine("Qual a quantidade de pessoas no quarto?");
            int NumeroPessoas = int.Parse(Console.ReadLine()); // Ainda tem que criar um sistema de verificaçõ se o que foi escrito é inteiro
            string Titular = "";

            for (int i = 1; i <= NumeroPessoas; i++)
            {
                Console.WriteLine("Qual o nome do do hospede " + i + " ?");
                String Nome = Console.ReadLine();
                Console.WriteLine("Qual o CPF do/da hospede " + Nome + " ?");
                String CPF = Console.ReadLine();
                Console.WriteLine("Qual o Idade do/a hospede " + Nome + " ?");
                int Idade = int.Parse(Console.ReadLine());

                _pessoas.Add(new Pessoa(Nome, Idade, CPF, NumeroQuarto, DataCadastro));
                if (i == 1)
                {
                    Titular = Nome;
                }
            }
            return Titular;
        }

        public string ListarPessoasCadastradas()
        {
            if (!_pessoas.Any())
                return "Nenhuma pessoa cadastrada.";

            var tabela = new StringBuilder();

            // Cabeçalho com Número do Quarto
            tabela.AppendLine("┌──────────────┬──────────────────┬──────────────────┬────────┬───────────────┐");
            tabela.AppendLine("│ Nº Quarto    │ Nome             │ CPF              │ Idade  │ Data Cadastro │");
            tabela.AppendLine("├──────────────┼──────────────────┼──────────────────┼────────┼───────────────┤");

            foreach (var pessoa in _pessoas.OrderBy(p => p.NumeroQuarto).ThenBy(p => p.Nome))
            {
                string cpfFormatado = pessoa.CPF;
                string nomeFormatado = pessoa.Nome.Length > 16 ?
                    pessoa.Nome.Substring(0, 13) + "..." :
                    pessoa.Nome.PadRight(16);

                tabela.AppendLine($"│ {pessoa.NumeroQuarto,-12} │ {nomeFormatado,-16} │ {cpfFormatado,-12}   │ {pessoa.Idade,6} │ {pessoa.DataCadastro:dd/MM/yyyy}    │");
            }

            tabela.AppendLine("└──────────────┴──────────────────┴──────────────────┴────────┴───────────────┘");
            return tabela.ToString();
        }

        public void RemoverPessoasQuarto(int NumeroQuarto)

        {
            _pessoas.RemoveAll(p => p.NumeroQuarto == NumeroQuarto);
        }

        public void AdicionarUmaPessoa()
        {
            try //Testando try - catch
            {
                Console.WriteLine("Digite o Nome da Nova Pessoa");
                string nome = Console.ReadLine();

                Console.WriteLine("Digite a Idade da Nova Pessoa");  
                int idade = int.Parse(Console.ReadLine());

                Console.WriteLine("Digite o CPF da Nova Pessoa");  
                string cpf = Console.ReadLine();

                Console.WriteLine("Digite o Nº do quarto");
                int numeroQuarto = int.Parse(Console.ReadLine());

                
                DateTime dataCadastro = DateTime.Now; 

                _pessoas.Add(new Pessoa(nome, idade, cpf, numeroQuarto, dataCadastro));
                
                Console.WriteLine("Pessoa adicionada com sucesso!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Erro: Formato inválido para idade ou número do quarto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }
                        

            


    }

}

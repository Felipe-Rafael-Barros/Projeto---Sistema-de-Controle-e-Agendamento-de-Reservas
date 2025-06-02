using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace SistemaDeReservas.Models
{
    public class GerenciadorQuartos
    {
        private readonly List<Quarto> _quartos;

        public GerenciadorQuartos()
        {
            // Aqui o sistema inicia com um número de quartos X, mas n pode adicionar ainda durante a execução do codigo(Adicionar a funcionalidade)
            _quartos = new List<Quarto>
            {
                new Quarto(100, "luxo 1", 500, true),
                new Quarto(101, "luxo 2", 500, true),
                new Quarto(102, "luxo 3", 500, true),
                new Quarto(103, "luxo 4", 500, true),

                new Quarto(200, "medio 1", 250, true),
                new Quarto(201, "medio 2", 250, true),
                new Quarto(202, "medio 3", 250, true),
                new Quarto(203, "medio 4", 250, true),

                new Quarto(300, "básico 1", 100, true),
                new Quarto(301, "básico 2", 100, true),
                new Quarto(302, "básico 3", 100, true),
                new Quarto(303, "básico 4", 100, true),
            };
        }

        public string ListarQuartosFormatado()
        {
            if (!_quartos.Any())
                return "Nenhum quarto cadastrado.";

            var tabela = new StringBuilder();

            // Ajustei o tamanho da tabela para incluir a coluna de status
            tabela.AppendLine("┌──────────┬──────────────┬─────────────┬────────────┐");
            tabela.AppendLine("│ Número   │ Tipo         │ Preço Diári │  Status    │");
            tabela.AppendLine("├──────────┼──────────────┼─────────────┼────────────┤");

            foreach (var quarto in _quartos.OrderBy(q => q.Numero))
            {
                string status = quarto.Disponivel ? "Disponível" : "Ocupado  ";
                tabela.AppendLine($"│ {quarto.Numero,-8} │ {quarto.Tipo,-12} │ R$ {quarto.PrecoDiaria,8} │ {status} │");
            }

            tabela.AppendLine("└──────────┴──────────────┴─────────────┴────────────┘");
            return tabela.ToString();
        }

        public int AlterarDisponibilidade(bool Cadastro, int NumeroDoQuartoRemove)
        {
            int auxiliar = 0;
            if (Cadastro == true)
            {
                int numero = 0;
                bool QuartoEncontrado = false;
                while (!QuartoEncontrado)
                {
                    Console.WriteLine("Por favor, selecione o nº do quarto desejado:");
                    numero = int.Parse(Console.ReadLine());
                    for (int i = 0; i < _quartos.Count; i++)
                    {
                        if (_quartos[i].Numero == numero && _quartos[i].Disponivel == true)
                        {
                            _quartos[i].Disponivel = !_quartos[i].Disponivel;
                            QuartoEncontrado = true;
                            Console.WriteLine("O quarto " + _quartos[i].Numero + " está disponível, por favor continue o cadastro abaixo. ");


                        }
                    }
                    if (QuartoEncontrado == false)
                    {
                        Console.WriteLine("Quarto não encontrado ou não está disponível no momento, tente novamente.");
                    }


                }
                return numero;
            }
            else if (Cadastro == false)
            {
                for (int i = 0; i < _quartos.Count; i++)
                {
                    if (_quartos[i].Numero == NumeroDoQuartoRemove)
                    {
                        _quartos[i].Disponivel = !_quartos[i].Disponivel;
                        Console.WriteLine("O quarto " + _quartos[i].Numero + " Agora está marcado como disponível com sucesso. ");


                    }
                }
            }
            else
            {
                Console.WriteLine("Erro Do parametro passado na função cadastro/remove");
            }
            return auxiliar;
        }

        public (int, string) DiariaDoQuarto(int NumeroQuarto)
        {
            int PrecoDiaria = 0;
            string Tipo = "";
            for (int i = 0; i < _quartos.Count; i++)
            {
                if (_quartos[i].Numero == NumeroQuarto)
                {
                    PrecoDiaria = _quartos[i].PrecoDiaria;
                    Tipo = _quartos[i].Tipo;
                }

            }
            return (PrecoDiaria, Tipo);
        }

        public int NumeroDeQuartosCadastrados()
        {
            int Numero = _quartos.Count;
            return Numero;
        }

        
    }
}
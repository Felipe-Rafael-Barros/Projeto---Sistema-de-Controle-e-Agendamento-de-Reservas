using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace SistemaDeReservas.Models
{
    public class GerenciadoQuartos
    {
        private readonly List<Quarto> _quartos;

        public GerenciadoQuartos()
        {
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

                new Quarto(300, "basico 1", 100, true),
                new Quarto(301, "basico 2", 100, true),
                new Quarto(302, "basico 3", 100, true),
                new Quarto(303, "basico 4", 100, true),
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
    }
}
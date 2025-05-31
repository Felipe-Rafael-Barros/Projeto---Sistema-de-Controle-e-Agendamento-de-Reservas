using SistemaDeReservas.Models;


var service = new GerenciadoQuartos();

bool SistemaAtivo = true;

while (SistemaAtivo)
{
    Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════════╗
║                                                                       ║
║       Bem Vindo ao Sistema de Controle e Cadastro de Reservas         ║
║                                                                       ║
╠═══════════════════════════════════════════════════════════════════════╣
║                                                                       ║
║   [1] Cadastrar nova reserva                                          ║
║   [2] Consultar reservas existentes                                   ║
║   [3] Atualizar reserva                                               ║
║   [4] Cancelar reserva                                                ║
║   [5] Verificar Quartos                                               ║
║   [6] Configurar valores/tarifas                                      ║
║   [7] Relatórios e estatísticas                                       ║
║   [8] Configurações do sistema                                        ║
║   [0] Sair                                                            ║
║                                                                       ║
╚═══════════════════════════════════════════════════════════════════════╝
");

    string Opção = Console.ReadLine();

    switch (Opção)
{
    case "1":
        //CadastrarReserva();
        break;
    case "2":
        //ListarReservas();
        break;
    case "3":
        //AtualizarReserva();
        break;
    case "4":
        //CancelarReserva();
        break;
    case "5":
        Console.WriteLine(service.ListarQuartosFormatado());
        Console.WriteLine("Clique em alguma tecla para voltar ao menu");
        Console.ReadLine();
        break;
    case "6":
        //ConfigurarValores();
        break;
    case "0":
        SistemaAtivo = false;
        break;

    default:
        Console.WriteLine("Opção inválida! Tente novamente.");
        break;
}
    
}
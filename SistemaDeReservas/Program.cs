using SistemaDeReservas.Models;

// Cria as instâncias uma vez no início
var GerenciadorPessoas = new GerenciadorPessoas();
var GerenciadorQuartos = new GerenciadorQuartos();
var GerenciadorReservas = new GerenciadorReservas(GerenciadorPessoas, GerenciadorQuartos);

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
║   [5] Verificar Disponibilidade dos Quartos                           ║
║   [6] Lista de Hospedes Cadastrado no Sistema                         ║
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
        GerenciadorReservas.CadastrarNovaReserva();
        break;
    case "2":
        Console.WriteLine(GerenciadorReservas.ListarReservas());
        Console.WriteLine("Clique em alguma tecla para voltar ao menu");
        Console.ReadLine();
        break;
    case "3":
        //AtualizarReserva();
        break;
    case "4":
        //CancelarReserva();
        break;
    case "5": //Lista Quartos cadastrados
        Console.WriteLine(GerenciadorReservas.ListarQuartos());
        Console.WriteLine("Clique em alguma tecla para voltar ao menu");
        Console.ReadLine();
        break;
    case "6":
        Console.WriteLine(GerenciadorReservas.ListarPessoasCadastradas());
        Console.WriteLine("Clique em alguma tecla para voltar ao menu");
        Console.ReadLine();
        break;
    case "0":
        SistemaAtivo = false;
        break;

    default:
        Console.WriteLine("Opção inválida! Tente novamente.");
        break;
}
    
}
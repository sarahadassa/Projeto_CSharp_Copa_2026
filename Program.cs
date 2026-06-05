using Copa2026;

// Garante que a pasta CSV exista
Directory.CreateDirectory(Dados.PastaCSV);

int opcao;
do
{
    Console.WriteLine("\n========== SISTEMA COPA 2026 ==========");
    Console.WriteLine("1  - Gerenciar seleções");
    Console.WriteLine("2  - Gerenciar estádios");
    Console.WriteLine("3  - Gerenciar jogos");
    Console.WriteLine("4  - Registrar resultado de jogo");
    Console.WriteLine("5  - Gerar tabela dos grupos");
    Console.WriteLine("6  - Mostrar melhores terceiros");
    Console.WriteLine("7  - Gerar mata-mata");
    Console.WriteLine("8  - Registrar resultados do mata-mata");
    Console.WriteLine("9  - Mostrar campeão");
    Console.WriteLine("10 - Relatórios");
    Console.WriteLine("11 - Salvar dados em CSV");
    Console.WriteLine("12 - Carregar dados do CSV");
    Console.WriteLine("0  - Sair");
    Console.Write("Escolha uma opção: ");

    if (!int.TryParse(Console.ReadLine(), out opcao)) opcao = -1;

    switch (opcao)
    {
        case 1:  GerenciarSelecoes.Menu();            break;
        case 2:  GerenciarEstadios.Menu();            break;
        case 3:  GerenciarJogos.Menu();               break;
        case 4:  GerenciarJogos.RegistrarPlacar();    break;
        case 5:  // TODO: Classificacao.GerarTabela();
                 Console.WriteLine("  [em desenvolvimento]"); break;
        case 6:  // TODO: Classificacao.MelhoresTerceiros();
                 Console.WriteLine("  [em desenvolvimento]"); break;
        case 7:  // TODO: MataMata.Gerar();
                 Console.WriteLine("  [em desenvolvimento]"); break;
        case 8:  // TODO: MataMata.RegistrarResultados();
                 Console.WriteLine("  [em desenvolvimento]"); break;
        case 9:  // TODO: MataMata.MostrarCampeao();
                 Console.WriteLine("  [em desenvolvimento]"); break;
        case 10: // TODO: Relatorios.Menu();
                 Console.WriteLine("  [em desenvolvimento]"); break;
        case 11: CsvHelper.SalvarTodos();             break;
        case 12: CsvHelper.CarregarTodos();           break;
        case 0:  Console.WriteLine("Até logo!");      break;
        default: Console.WriteLine("  [!] Opção inválida."); break;
    }

} while (opcao != 0);

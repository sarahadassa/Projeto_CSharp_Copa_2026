namespace Copa2026;

static class GerenciarJogos
{
    public static void Menu()
    {
        int opcao;
        do
        {
            Console.WriteLine("\n====== JOGOS ======");
            Console.WriteLine("1 - Cadastrar jogo");
            Console.WriteLine("2 - Listar jogos");
            Console.WriteLine("3 - Alterar jogo");
            Console.WriteLine("4 - Excluir jogo");
            Console.WriteLine("5 - Registrar placar");
            Console.WriteLine("0 - Voltar");
            opcao = Validacoes.LerInteiro("Escolha: ");

            switch (opcao)
            {
                case 1: Cadastrar();        break;
                case 2: Listar();           break;
                case 3: Alterar();          break;
                case 4: Excluir();          break;
                case 5: RegistrarPlacar();  break;
                case 0: break;
                default: Console.WriteLine("  [!] Opção inválida."); break;
            }
        } while (opcao != 0);
    }

    // ---------- Cadastrar ----------
    static void Cadastrar()
    {
        if (Dados.TotalJogos >= Dados.MAX_JOGOS)
        {
            Console.WriteLine("  [!] Limite de jogos atingido.");
            return;
        }

        Console.WriteLine("Fases disponíveis: Grupo | 32avos | Oitavas | Quartas | Semifinal | 3Lugar | Final");
        string fase = Validacoes.LerTexto("Fase: ");
        if (!Validacoes.FaseValida(fase)) { Console.WriteLine("  [!] Fase inválida."); return; }

        string grupo = "";
        if (fase == "Grupo")
        {
            grupo = Validacoes.LerTexto("Grupo (A-L): ").ToUpper();
            if (!Validacoes.GrupoValido(grupo)) { Console.WriteLine("  [!] Grupo inválido."); return; }
        }

        string data = Validacoes.LerTexto("Data (dd/MM/yyyy): ");
        if (data == "") return;

        GerenciarEstadios.Listar();
        int idEstadio = Validacoes.LerInteiro("ID do estádio: ");
        if (!Validacoes.EstadioExiste(idEstadio)) { Console.WriteLine("  [!] Estádio não encontrado."); return; }

        GerenciarSelecoes.Listar();
        int idTimeA = Validacoes.LerInteiro("ID Time A: ");
        if (!Validacoes.SelecaoExiste(idTimeA)) { Console.WriteLine("  [!] Seleção A não encontrada."); return; }

        int idTimeB = Validacoes.LerInteiro("ID Time B: ");
        if (!Validacoes.SelecaoExiste(idTimeB)) { Console.WriteLine("  [!] Seleção B não encontrada."); return; }

        if (idTimeA == idTimeB) { Console.WriteLine("  [!] Uma seleção não pode jogar contra ela mesma."); return; }

        int id = ProximoId();
        Dados.Jogos[Dados.TotalJogos] = new Jogo(id, fase, grupo, data, idEstadio, idTimeA, idTimeB);
        Dados.TotalJogos++;
        Console.WriteLine($"  [OK] Jogo #{id} cadastrado.");
    }

    // ---------- Listar ----------
    public static void Listar(string filtroFase = "")
    {
        Console.WriteLine("\n--- JOGOS CADASTRADOS ---");
        bool algum = false;
        for (int i = 0; i < Dados.TotalJogos; i++)
        {
            ref Jogo j = ref Dados.Jogos[i];
            if (!j.Ativo) continue;
            if (filtroFase != "" && j.Fase != filtroFase) continue;

            string nomeA = NomeSeleção(j.IdTimeA);
            string nomeB = NomeSeleção(j.IdTimeB);
            string nomeE = NomeEstadio(j.IdEstadio);
            string placar = j.Realizado ? $"{j.GolsA} x {j.GolsB}" : "a realizar";
            string pen = (j.Realizado && j.IdVencedorPenaltis != 0)
                ? $" (pen: {NomeSeleção(j.IdVencedorPenaltis)})" : "";

            Console.WriteLine($"#{j.Id,-4} [{j.Fase}{(j.Grupo!="" ? "/"+j.Grupo : "")}] " +
                              $"{j.Data}  {nomeA} vs {nomeB}  {placar}{pen}  @ {nomeE}");
            algum = true;
        }
        if (!algum) Console.WriteLine("  (nenhum jogo cadastrado)");
    }

    // ---------- Alterar ----------
    static void Alterar()
    {
        int id  = Validacoes.LerInteiro("ID do jogo para alterar: ");
        int idx = BuscarIdx(id);
        if (idx < 0) { Console.WriteLine("  [!] Jogo não encontrado."); return; }

        if (Dados.Jogos[idx].Realizado)
        {
            Console.WriteLine("  [!] Jogo já realizado. Use 'Registrar placar' para alterar o resultado.");
            return;
        }

        string data = Validacoes.LerTexto($"Nova data [{Dados.Jogos[idx].Data}]: ");
        if (data != "") Dados.Jogos[idx].Data = data;

        Console.WriteLine("  [OK] Jogo alterado.");
    }

    // ---------- Excluir ----------
    static void Excluir()
    {
        int id  = Validacoes.LerInteiro("ID do jogo para excluir: ");
        int idx = BuscarIdx(id);
        if (idx < 0) { Console.WriteLine("  [!] Jogo não encontrado."); return; }

        Dados.Jogos[idx].Ativo = false;
        Console.WriteLine($"  [OK] Jogo #{id} excluído.");
    }

    // ---------- Registrar Placar ----------
    public static void RegistrarPlacar()
    {
        int id  = Validacoes.LerInteiro("ID do jogo: ");
        int idx = BuscarIdx(id);
        if (idx < 0) { Console.WriteLine("  [!] Jogo não encontrado."); return; }

        ref Jogo j = ref Dados.Jogos[idx];

        int golsA = Validacoes.LerInteiro($"Gols {NomeSeleção(j.IdTimeA)}: ");
        if (golsA < 0) { Console.WriteLine("  [!] Placar não pode ser negativo."); return; }

        int golsB = Validacoes.LerInteiro($"Gols {NomeSeleção(j.IdTimeB)}: ");
        if (golsB < 0) { Console.WriteLine("  [!] Placar não pode ser negativo."); return; }

        j.GolsA     = golsA;
        j.GolsB     = golsB;
        j.Realizado = true;

        // Mata-mata: empate exige pênaltis
        if (j.Fase != "Grupo" && golsA == golsB)
        {
            Console.WriteLine($"  Empate! Quem venceu nos pênaltis?");
            Console.WriteLine($"  1 - {NomeSeleção(j.IdTimeA)}");
            Console.WriteLine($"  2 - {NomeSeleção(j.IdTimeB)}");
            int pen = Validacoes.LerInteiro("  Escolha: ");
            j.IdVencedorPenaltis = pen == 1 ? j.IdTimeA : j.IdTimeB;
            Console.WriteLine($"  Vencedor nos pênaltis: {NomeSeleção(j.IdVencedorPenaltis)}");
        }

        Console.WriteLine("  [OK] Resultado registrado.");
    }

    // ---------- Helpers ----------
    static int ProximoId()
    {
        int max = 0;
        for (int i = 0; i < Dados.TotalJogos; i++)
            if (Dados.Jogos[i].Id > max) max = Dados.Jogos[i].Id;
        return max + 1;
    }

    public static int BuscarIdx(int id)
    {
        for (int i = 0; i < Dados.TotalJogos; i++)
            if (Dados.Jogos[i].Id == id && Dados.Jogos[i].Ativo)
                return i;
        return -1;
    }

    static string NomeSeleção(int id)
    {
        int idx = GerenciarSelecoes.BuscarIdx(id);
        return idx >= 0 ? Dados.Selecoes[idx].Nome : $"#{id}";
    }

    static string NomeEstadio(int id)
    {
        int idx = GerenciarEstadios.BuscarIdx(id);
        return idx >= 0 ? Dados.Estadios[idx].Nome : $"#{id}";
    }
}

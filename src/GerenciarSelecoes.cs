namespace Copa2026;

static class GerenciarSelecoes
{
    // ---------- Menu ----------
    public static void Menu()
    {
        int opcao;
        do
        {
            Console.WriteLine("\n====== SELEÇÕES ======");
            Console.WriteLine("1 - Cadastrar seleção");
            Console.WriteLine("2 - Listar seleções");
            Console.WriteLine("3 - Alterar seleção");
            Console.WriteLine("4 - Excluir seleção");
            Console.WriteLine("0 - Voltar");
            opcao = Validacoes.LerInteiro("Escolha: ");

            switch (opcao)
            {
                case 1: Cadastrar(); break;
                case 2: Listar();    break;
                case 3: Alterar();   break;
                case 4: Excluir();   break;
                case 0: break;
                default: Console.WriteLine("  [!] Opção inválida."); break;
            }
        } while (opcao != 0);
    }

    // ---------- Cadastrar ----------
    static void Cadastrar()
    {
        if (Dados.TotalSelecoes >= Dados.MAX_SELECOES)
        {
            Console.WriteLine("  [!] Limite de 48 seleções atingido.");
            return;
        }

        string nome = Validacoes.LerTexto("Nome da seleção: ");
        if (nome == "") return;

        string grupo = Validacoes.LerTexto("Grupo (A-L): ").ToUpper();
        if (!Validacoes.GrupoValido(grupo))
        {
            Console.WriteLine("  [!] Grupo inválido. Use A até L.");
            return;
        }

        if (Validacoes.ContarSelecoesPorGrupo(grupo) >= 4)
        {
            Console.WriteLine($"  [!] Grupo {grupo} já tem 4 seleções.");
            return;
        }

        int id = ProximoId();
        Dados.Selecoes[Dados.TotalSelecoes] = new Selecao(id, nome, grupo);
        Dados.TotalSelecoes++;
        Console.WriteLine($"  [OK] Seleção #{id} '{nome}' cadastrada no Grupo {grupo}.");
    }

    // ---------- Listar ----------
    public static void Listar()
    {
        Console.WriteLine("\n--- SELEÇÕES CADASTRADAS ---");
        Console.WriteLine($"{"ID",-5} {"Nome",-25} {"Grupo",-7}");
        Console.WriteLine(new string('-', 40));

        bool alguma = false;
        for (int i = 0; i < Dados.TotalSelecoes; i++)
        {
            ref Selecao s = ref Dados.Selecoes[i];
            if (!s.Ativo) continue;
            Console.WriteLine($"{s.Id,-5} {s.Nome,-25} {s.Grupo,-7}");
            alguma = true;
        }
        if (!alguma) Console.WriteLine("  (nenhuma seleção cadastrada)");
    }

    // ---------- Alterar ----------
    static void Alterar()
    {
        int id = Validacoes.LerInteiro("ID da seleção para alterar: ");
        int idx = BuscarIdx(id);
        if (idx < 0) { Console.WriteLine("  [!] Seleção não encontrada."); return; }

        string nome = Validacoes.LerTexto($"Novo nome [{Dados.Selecoes[idx].Nome}]: ");
        if (nome != "") Dados.Selecoes[idx].Nome = nome;

        string grupo = Validacoes.LerTexto($"Novo grupo [{Dados.Selecoes[idx].Grupo}]: ").ToUpper();
        if (grupo != "")
        {
            if (!Validacoes.GrupoValido(grupo)) { Console.WriteLine("  [!] Grupo inválido."); return; }
            Dados.Selecoes[idx].Grupo = grupo;
        }

        Console.WriteLine("  [OK] Seleção alterada.");
    }

    // ---------- Excluir (lógica) ----------
    static void Excluir()
    {
        int id  = Validacoes.LerInteiro("ID da seleção para excluir: ");
        int idx = BuscarIdx(id);
        if (idx < 0) { Console.WriteLine("  [!] Seleção não encontrada."); return; }

        Dados.Selecoes[idx].Ativo = false;
        Console.WriteLine($"  [OK] Seleção '{Dados.Selecoes[idx].Nome}' excluída.");
    }

    // ---------- Helpers ----------
    static int ProximoId()
    {
        int max = 0;
        for (int i = 0; i < Dados.TotalSelecoes; i++)
            if (Dados.Selecoes[i].Id > max) max = Dados.Selecoes[i].Id;
        return max + 1;
    }

    public static int BuscarIdx(int id)
    {
        for (int i = 0; i < Dados.TotalSelecoes; i++)
            if (Dados.Selecoes[i].Id == id && Dados.Selecoes[i].Ativo)
                return i;
        return -1;
    }
}

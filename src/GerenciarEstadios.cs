namespace Copa2026;

static class GerenciarEstadios
{
    public static void Menu()
    {
        int opcao;
        do
        {
            Console.WriteLine("\n====== ESTÁDIOS ======");
            Console.WriteLine("1 - Cadastrar estádio");
            Console.WriteLine("2 - Listar estádios");
            Console.WriteLine("3 - Alterar estádio");
            Console.WriteLine("4 - Excluir estádio");
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

    static void Cadastrar()
    {
        if (Dados.TotalEstadios >= Dados.MAX_ESTADIOS)
        {
            Console.WriteLine("  [!] Limite de 16 estádios atingido.");
            return;
        }

        string nome = Validacoes.LerTexto("Nome do estádio: ");
        if (nome == "") return;

        string cidade = Validacoes.LerTexto("Cidade: ");
        if (cidade == "") return;

        string pais = Validacoes.LerTexto("País: ");
        if (pais == "") return;

        int cap = Validacoes.LerInteiro("Capacidade: ");
        if (cap <= 0) { Console.WriteLine("  [!] Capacidade inválida."); return; }

        int id = ProximoId();
        Dados.Estadios[Dados.TotalEstadios] = new Estadio(id, nome, cidade, pais, cap);
        Dados.TotalEstadios++;
        Console.WriteLine($"  [OK] Estádio #{id} '{nome}' cadastrado.");
    }

    public static void Listar()
    {
        Console.WriteLine("\n--- ESTÁDIOS CADASTRADOS ---");
        Console.WriteLine($"{"ID",-5} {"Nome",-35} {"Cidade",-25} {"País",-15} {"Cap.",10}");
        Console.WriteLine(new string('-', 92));

        bool algum = false;
        for (int i = 0; i < Dados.TotalEstadios; i++)
        {
            ref Estadio e = ref Dados.Estadios[i];
            if (!e.Ativo) continue;
            Console.WriteLine($"{e.Id,-5} {e.Nome,-35} {e.Cidade,-25} {e.Pais,-15} {e.Capacidade,10:N0}");
            algum = true;
        }
        if (!algum) Console.WriteLine("  (nenhum estádio cadastrado)");
    }

    static void Alterar()
    {
        int id  = Validacoes.LerInteiro("ID do estádio para alterar: ");
        int idx = BuscarIdx(id);
        if (idx < 0) { Console.WriteLine("  [!] Estádio não encontrado."); return; }

        string nome = Validacoes.LerTexto($"Novo nome [{Dados.Estadios[idx].Nome}]: ");
        if (nome != "") Dados.Estadios[idx].Nome = nome;

        string cidade = Validacoes.LerTexto($"Nova cidade [{Dados.Estadios[idx].Cidade}]: ");
        if (cidade != "") Dados.Estadios[idx].Cidade = cidade;

        string pais = Validacoes.LerTexto($"Novo país [{Dados.Estadios[idx].Pais}]: ");
        if (pais != "") Dados.Estadios[idx].Pais = pais;

        Console.Write($"Nova capacidade [{Dados.Estadios[idx].Capacidade}]: ");
        string? capStr = Console.ReadLine();
        if (!string.IsNullOrEmpty(capStr) && int.TryParse(capStr, out int cap) && cap > 0)
            Dados.Estadios[idx].Capacidade = cap;

        Console.WriteLine("  [OK] Estádio alterado.");
    }

    static void Excluir()
    {
        int id  = Validacoes.LerInteiro("ID do estádio para excluir: ");
        int idx = BuscarIdx(id);
        if (idx < 0) { Console.WriteLine("  [!] Estádio não encontrado."); return; }

        Dados.Estadios[idx].Ativo = false;
        Console.WriteLine($"  [OK] Estádio '{Dados.Estadios[idx].Nome}' excluído.");
    }

    static int ProximoId()
    {
        int max = 0;
        for (int i = 0; i < Dados.TotalEstadios; i++)
            if (Dados.Estadios[i].Id > max) max = Dados.Estadios[i].Id;
        return max + 1;
    }

    public static int BuscarIdx(int id)
    {
        for (int i = 0; i < Dados.TotalEstadios; i++)
            if (Dados.Estadios[i].Id == id && Dados.Estadios[i].Ativo)
                return i;
        return -1;
    }
}

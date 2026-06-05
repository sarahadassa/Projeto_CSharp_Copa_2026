namespace Copa2026;

// ============================================================
//  LEITURA E GRAVAÇÃO DE CSV
// ============================================================
static class CsvHelper
{
    // ======================== SALVAR ========================

    public static void SalvarTodos()
    {
        Directory.CreateDirectory(Dados.PastaCSV);
        SalvarSelecoes();
        SalvarEstadios();
        SalvarJogos();
        Console.WriteLine("  [OK] Todos os dados foram salvos em CSV.");
    }

    static void SalvarSelecoes()
    {
        using var w = new StreamWriter(Dados.CsvSelecoes);
        w.WriteLine("id;nome;grupo;ativo");
        for (int i = 0; i < Dados.TotalSelecoes; i++)
        {
            ref Selecao s = ref Dados.Selecoes[i];
            w.WriteLine($"{s.Id};{s.Nome};{s.Grupo};{s.Ativo.ToString().ToLower()}");
        }
    }

    static void SalvarEstadios()
    {
        using var w = new StreamWriter(Dados.CsvEstadios);
        w.WriteLine("id;nome;cidade;pais;capacidade;ativo");
        for (int i = 0; i < Dados.TotalEstadios; i++)
        {
            ref Estadio e = ref Dados.Estadios[i];
            w.WriteLine($"{e.Id};{e.Nome};{e.Cidade};{e.Pais};{e.Capacidade};{e.Ativo.ToString().ToLower()}");
        }
    }

    static void SalvarJogos()
    {
        using var w = new StreamWriter(Dados.CsvJogos);
        w.WriteLine("id;fase;grupo;data;idEstadio;idTimeA;idTimeB;golsA;golsB;realizado;idVencedorPenaltis;ativo");
        for (int i = 0; i < Dados.TotalJogos; i++)
        {
            ref Jogo j = ref Dados.Jogos[i];
            w.WriteLine($"{j.Id};{j.Fase};{j.Grupo};{j.Data};{j.IdEstadio};{j.IdTimeA};{j.IdTimeB};" +
                        $"{j.GolsA};{j.GolsB};{j.Realizado.ToString().ToLower()};{j.IdVencedorPenaltis};{j.Ativo.ToString().ToLower()}");
        }
    }

    // ======================== CARREGAR ========================

    public static void CarregarTodos()
    {
        CarregarSelecoes();
        CarregarEstadios();
        CarregarJogos();
        Console.WriteLine("  [OK] Dados carregados do CSV.");
    }

    static void CarregarSelecoes()
    {
        if (!File.Exists(Dados.CsvSelecoes))
        {
            Console.WriteLine($"  [!] Arquivo não encontrado: {Dados.CsvSelecoes}");
            return;
        }

        try
        {
            Dados.TotalSelecoes = 0;
            string[] linhas = File.ReadAllLines(Dados.CsvSelecoes);
            for (int i = 1; i < linhas.Length; i++) // pula cabeçalho
            {
                string[] f = linhas[i].Split(';');
                if (f.Length < 4) continue;
                Dados.Selecoes[Dados.TotalSelecoes++] = new Selecao
                {
                    Id = int.Parse(f[0]),
                    Nome = f[1],
                    Grupo = f[2],
                    Ativo = f[3] == "true"
                };
            }
            Console.WriteLine($"  {Dados.TotalSelecoes} seleções carregadas.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [ERRO] Falha ao ler {Dados.CsvSelecoes}: {ex.Message}");
        }
    }

    static void CarregarEstadios()
    {
        if (!File.Exists(Dados.CsvEstadios))
        {
            Console.WriteLine($"  [!] Arquivo não encontrado: {Dados.CsvEstadios}");
            return;
        }

        try
        {
            Dados.TotalEstadios = 0;
            string[] linhas = File.ReadAllLines(Dados.CsvEstadios);
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] f = linhas[i].Split(';');
                if (f.Length < 6) continue;
                Dados.Estadios[Dados.TotalEstadios++] = new Estadio
                {
                    Id = int.Parse(f[0]),
                    Nome = f[1],
                    Cidade = f[2],
                    Pais = f[3],
                    Capacidade = int.Parse(f[4]),
                    Ativo = f[5] == "true"
                };
            }
            Console.WriteLine($"  {Dados.TotalEstadios} estádios carregados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [ERRO] Falha ao ler {Dados.CsvEstadios}: {ex.Message}");
        }
    }

    static void CarregarJogos()
    {
        if (!File.Exists(Dados.CsvJogos))
        {
            Console.WriteLine($"  [!] Arquivo não encontrado: {Dados.CsvJogos}");
            return;
        }

        try
        {
            Dados.TotalJogos = 0;
            string[] linhas = File.ReadAllLines(Dados.CsvJogos);
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] f = linhas[i].Split(';');
                if (f.Length < 12) continue;
                Dados.Jogos[Dados.TotalJogos++] = new Jogo
                {
                    Id = int.Parse(f[0]),
                    Fase = f[1],
                    Grupo = f[2],
                    Data = f[3],
                    IdEstadio = int.Parse(f[4]),
                    IdTimeA = int.Parse(f[5]),
                    IdTimeB = int.Parse(f[6]),
                    GolsA = int.Parse(f[7]),
                    GolsB = int.Parse(f[8]),
                    Realizado = f[9] == "true",
                    IdVencedorPenaltis = int.Parse(f[10]),
                    Ativo = f[11] == "true"
                };
            }
            Console.WriteLine($"  {Dados.TotalJogos} jogos carregados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"  [ERRO] Falha ao ler {Dados.CsvJogos}: {ex.Message}");
        }
    }
}
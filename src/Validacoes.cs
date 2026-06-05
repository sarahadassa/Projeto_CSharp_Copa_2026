namespace Copa2026;

static class Validacoes
{
    public static bool GrupoValido(string grupo)
        => Array.Exists(Dados.GruposValidos, g => g == grupo.ToUpper());

    public static bool FaseValida(string fase)
        => Array.Exists(Dados.FasesValidas, f => f == fase);

    public static bool SelecaoExiste(int id)
    {
        for (int i = 0; i < Dados.TotalSelecoes; i++)
            if (Dados.Selecoes[i].Id == id && Dados.Selecoes[i].Ativo)
                return true;
        return false;
    }

    public static bool EstadioExiste(int id)
    {
        for (int i = 0; i < Dados.TotalEstadios; i++)
            if (Dados.Estadios[i].Id == id && Dados.Estadios[i].Ativo)
                return true;
        return false;
    }

    public static int ContarSelecoesPorGrupo(string grupo)
    {
        int count = 0;
        for (int i = 0; i < Dados.TotalSelecoes; i++)
            if (Dados.Selecoes[i].Ativo && Dados.Selecoes[i].Grupo == grupo.ToUpper())
                count++;
        return count;
    }

    // Lê inteiro com mensagem; retorna -1 se inválido
    public static int LerInteiro(string prompt)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int val)) return val;
        Console.WriteLine("  [!] Valor inválido.");
        return -1;
    }

    // Lê string não vazia
    public static string LerTexto(string prompt)
    {
        Console.Write(prompt);
        string? val = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(val)) return val;
        Console.WriteLine("  [!] Valor não pode ser vazio.");
        return "";
    }
}

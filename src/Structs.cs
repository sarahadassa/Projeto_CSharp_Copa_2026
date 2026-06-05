namespace Copa2026;

struct Selecao
{
    public int    Id;
    public string Nome;
    public string Grupo;   // "A" até "L"
    public bool   Ativo;   // exclusão lógica

    public Selecao(int id, string nome, string grupo)
    {
        Id    = id;
        Nome  = nome;
        Grupo = grupo.ToUpper();
        Ativo = true;
    }
}

struct Estadio
{
    public int    Id;
    public string Nome;
    public string Cidade;
    public string Pais;
    public int    Capacidade;
    public bool   Ativo;

    public Estadio(int id, string nome, string cidade, string pais, int capacidade)
    {
        Id         = id;
        Nome       = nome;
        Cidade     = cidade;
        Pais       = pais;
        Capacidade = capacidade;
        Ativo      = true;
    }
}

struct Jogo
{
    public int    Id;
    public string Fase;               // "Grupo", "32avos", "Oitavas", "Quartas", "Semifinal", "3Lugar", "Final"
    public string Grupo;              // "A"–"L" (somente fase de grupos)
    public string Data;               // dd/MM/yyyy
    public int    IdEstadio;
    public int    IdTimeA;
    public int    IdTimeB;
    public int    GolsA;
    public int    GolsB;
    public bool   Realizado;
    public int    IdVencedorPenaltis; // 0 = sem pênaltis
    public bool   Ativo;

    public Jogo(int id, string fase, string grupo, string data, int idEstadio, int idTimeA, int idTimeB)
    {
        Id                 = id;
        Fase               = fase;
        Grupo              = grupo;
        Data               = data;
        IdEstadio          = idEstadio;
        IdTimeA            = idTimeA;
        IdTimeB            = idTimeB;
        GolsA              = 0;
        GolsB              = 0;
        Realizado          = false;
        IdVencedorPenaltis = 0;
        Ativo              = true;
    }
}

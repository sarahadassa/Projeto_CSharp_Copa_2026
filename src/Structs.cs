namespace Copa2026;

// ============================================================
//  STRUCTS PRINCIPAIS DO SISTEMA COPA 2026
// ============================================================

struct Selecao
{
    public int Id;
    public string Nome;
    public string Grupo;   // "A" até "L"
    public bool Ativo;   // exclusão lógica

    public Selecao(int id, string nome, string grupo)
    {
        Id = id;
        Nome = nome;
        Grupo = grupo.ToUpper();
        Ativo = true;
    }
}

struct Estadio
{
    public int Id;
    public string Nome;
    public string Cidade;
    public string Pais;
    public int Capacidade;
    public bool Ativo;

    public Estadio(int id, string nome, string cidade, string pais, int capacidade)
    {
        Id = id;
        Nome = nome;
        Cidade = cidade;
        Pais = pais;
        Capacidade = capacidade;
        Ativo = true;
    }
}

struct Jogo
{
    public int Id;
    public string Fase;               // "Grupo", "32avos", "Oitavas", "Quartas", "Semifinal", "3Lugar", "Final"
    public string Grupo;              // "A"–"L" (somente fase de grupos)
    public string Data;               // dd/MM/yyyy
    public int IdEstadio;
    public int IdTimeA;
    public int IdTimeB;
    public int GolsA;
    public int GolsB;
    public bool Realizado;
    public int IdVencedorPenaltis; // 0 = sem pênaltis
    public bool Ativo;

    public Jogo(int id, string fase, string grupo, string data, int idEstadio, int idTimeA, int idTimeB)
    {
        Id = id;
        Fase = fase;
        Grupo = grupo;
        Data = data;
        IdEstadio = idEstadio;
        IdTimeA = idTimeA;
        IdTimeB = idTimeB;
        GolsA = 0;
        GolsB = 0;
        Realizado = false;
        IdVencedorPenaltis = 0;
        Ativo = true;
    }
}

// Representa a linha de um time na tabela de classificação
// Usada para ordenar e exibir os grupos e melhores terceiros
struct Classificacao
{
    public int IdSelecao;   // referência ao vetor de seleções
    public string NomeSelecao;
    public string Grupo;       // "A" até "L"
    public int Posicao;     // 1º, 2º, 3º ou 4º no grupo
    public int Jogos;
    public int Vitorias;
    public int Empates;
    public int Derrotas;
    public int GolsPro;     // gols marcados
    public int GolsContra;
    public int SaldoGols;   // GolsPro - GolsContra
    public int Pontos;      // V*3 + E*1
}
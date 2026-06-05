namespace Copa2026;

static class Dados
{
    // --- Limites ---
    public const int MAX_SELECOES = 48;
    public const int MAX_ESTADIOS = 16;
    public const int MAX_JOGOS    = 200; // grupos + todas as fases mata-mata

    // --- Vetores principais ---
    public static Selecao[] Selecoes = new Selecao[MAX_SELECOES];
    public static Estadio[] Estadios = new Estadio[MAX_ESTADIOS];
    public static Jogo[]    Jogos    = new Jogo[MAX_JOGOS];

    // --- Contadores ---
    public static int TotalSelecoes = 0;
    public static int TotalEstadios = 0;
    public static int TotalJogos    = 0;

    // --- Matriz de classificação: [idxSeleção, coluna] ---
    // col 0=J  1=V  2=E  3=D  4=GP  5=GC  6=SG  7=PTS
    public static int[,] Tabela = new int[MAX_SELECOES, 8];

    // --- Grupos válidos ---
    public static readonly string[] GruposValidos = { "A","B","C","D","E","F","G","H","I","J","K","L" };

    // --- Fases válidas ---
    public static readonly string[] FasesValidas = { "Grupo","32avos","Oitavas","Quartas","Semifinal","3Lugar","Final" };

    // --- Caminhos dos CSV ---
    public static string PastaCSV     = "csv";
    public static string CsvSelecoes  => Path.Combine(PastaCSV, "selecoes.csv");
    public static string CsvEstadios  => Path.Combine(PastaCSV, "estadios.csv");
    public static string CsvJogos     => Path.Combine(PastaCSV, "jogos.csv");
    public static string CsvClassific => Path.Combine(PastaCSV, "classificacao.csv");
    public static string CsvMataMata  => Path.Combine(PastaCSV, "mata_mata.csv");
    public static string CsvRelatorio => Path.Combine(PastaCSV, "relatorio_final.csv");
}

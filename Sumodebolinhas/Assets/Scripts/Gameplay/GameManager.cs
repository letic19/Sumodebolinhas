using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Bolinhas escolhidas na seleção")]
    public BolinhaData bolinhaJogador1;
    public BolinhaData bolinhaJogador2;

    [Header("Placar da partida (rounds vencidos)")]
    public int vitoriasJogador1 = 0;
    public int vitoriasJogador2 = 0;

    [Header("Configuração dos rounds")]
    [Tooltip("Quantos rounds um jogador precisa vencer pra ganhar a partida. 2 = melhor de 3.")]
    [SerializeField] private int roundsParaVencerPartida = 2;

    [Header("Resultado final (lido pela cena de Vitória)")]
    public string vencedor;
    public BolinhaData bolinhaVencedora;

    [Header("Instâncias em jogo (preenchido em runtime)")]
    // A cena "GUI" é separada da cena "Gameplay", então nada nela consegue
    // arrastar uma referência de Bolinha no Inspector. Em vez disso, cada
    // Bolinha se registra aqui assim que nasce, e quem precisar (ex: CooldownBar)
    // busca a referência certa através do GameManager.
    public Bolinha bolinhaInstanciaJogador1;
    public Bolinha bolinhaInstanciaJogador2;

    public void RegistrarBolinha(PlayerInputHandler.PlayerType tipo, Bolinha instancia)
    {
        if (tipo == PlayerInputHandler.PlayerType.Player1)
            bolinhaInstanciaJogador1 = instancia;
        else
            bolinhaInstanciaJogador2 = instancia;
    }

    // ---------------- OBSERVER ----------------
    // Bolinha escuta OnRoundReiniciado pra se reposicionar/resetar no início de cada round.
    // GameUI escuta OnRoundVencido pra atualizar o placar na tela.
    // VictoryScene/VictoryUI podem escutar OnPartidaVencida se quiserem reagir sem depender só da troca de cena.
    public event Action<PlayerInputHandler.PlayerType> OnRoundVencido;
    public event Action OnRoundReiniciado;
    public event Action<PlayerInputHandler.PlayerType> OnPartidaVencida;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IniciarGameplay()
    {
        // Zera o placar da partida toda vez que uma nova partida começa
        vitoriasJogador1 = 0;
        vitoriasJogador2 = 0;
        vencedor = string.Empty;
        bolinhaVencedora = null;
        bolinhaInstanciaJogador1 = null;
        bolinhaInstanciaJogador2 = null;

        SceneManager.LoadScene("Gameplay");
        SceneManager.LoadScene("GUI", LoadSceneMode.Additive);
    }

    /// <summary>
    /// Chamado pela Bolinha (via DeathZone) quando ela cai da arena.
    /// Decide quem venceu o round, atualiza o placar e decide se a partida acabou.
    /// </summary>
    public void RegistrarQueda(PlayerInputHandler.PlayerType jogadorQueCaiu)
    {
        PlayerInputHandler.PlayerType vencedorDoRound =
            jogadorQueCaiu == PlayerInputHandler.PlayerType.Player1
                ? PlayerInputHandler.PlayerType.Player2
                : PlayerInputHandler.PlayerType.Player1;

        if (vencedorDoRound == PlayerInputHandler.PlayerType.Player1)
            vitoriasJogador1++;
        else
            vitoriasJogador2++;

        Debug.Log($"Round vencido por: {vencedorDoRound} | Placar -> J1: {vitoriasJogador1} | J2: {vitoriasJogador2}");

        OnRoundVencido?.Invoke(vencedorDoRound);

        bool jogador1VenceuPartida = vitoriasJogador1 >= roundsParaVencerPartida;
        bool jogador2VenceuPartida = vitoriasJogador2 >= roundsParaVencerPartida;

        if (jogador1VenceuPartida || jogador2VenceuPartida)
        {
            FinalizarPartida(vencedorDoRound);
        }
        else
        {
            // Ainda não acabou a partida: avisa as bolinhas pra reiniciarem o round
            OnRoundReiniciado?.Invoke();
        }
    }

    private void FinalizarPartida(PlayerInputHandler.PlayerType vencedorPartida)
    {
        if (vencedorPartida == PlayerInputHandler.PlayerType.Player1)
        {
            vencedor = "JOGADOR 1 VENCEU!";
            bolinhaVencedora = bolinhaJogador1;
        }
        else
        {
            vencedor = "JOGADOR 2 VENCEU!";
            bolinhaVencedora = bolinhaJogador2;
        }

        OnPartidaVencida?.Invoke(vencedorPartida);

        SceneManager.LoadScene("Vitoria");
    }

    public void VoltarSelecao()
    {
        Time.timeScale = 1f;
        vitoriasJogador1 = 0;
        vitoriasJogador2 = 0;
        SceneManager.LoadScene("CharacterSelect");
    }
}
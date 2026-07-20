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
    [Tooltip("Quantos rounds compõem a partida. A partida só termina depois que todos forem jogados.")]
    [SerializeField] private int totalDeRounds = 3;

    private int roundsJogados = 0;

    [Header("Resultado final (lido pela cena de Vitória)")]
    public string vencedor;
    public BolinhaData bolinhaVencedora;

    [Header("Instâncias em jogo (preenchido em runtime)")]
 
    public Bolinha bolinhaInstanciaJogador1;
    public Bolinha bolinhaInstanciaJogador2;

    public void RegistrarBolinha(PlayerInputHandler.PlayerType tipo, Bolinha instancia)
    {
        Debug.Log($"[GameManager] RegistrarBolinha chamado! Tipo: {tipo} | Instância: {instancia.name} | GameManager instance ID: {GetInstanceID()}");

        if (tipo == PlayerInputHandler.PlayerType.Player1)
            bolinhaInstanciaJogador1 = instancia;
        else
            bolinhaInstanciaJogador2 = instancia;
    }

    
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
        
        vitoriasJogador1 = 0;
        vitoriasJogador2 = 0;
        roundsJogados = 0;
        vencedor = string.Empty;
        bolinhaVencedora = null;
        bolinhaInstanciaJogador1 = null;
        bolinhaInstanciaJogador2 = null;

        SceneManager.LoadScene("Gameplay");
        SceneManager.LoadScene("GUI", LoadSceneMode.Additive);
    }

    
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

        roundsJogados++;

        Debug.Log($"[GameManager] Round {roundsJogados}/{totalDeRounds} vencido por: {vencedorDoRound} | Placar -> J1: {vitoriasJogador1} | J2: {vitoriasJogador2}");

        OnRoundVencido?.Invoke(vencedorDoRound);

        if (roundsJogados >= totalDeRounds)
        {
            Debug.Log("[GameManager] Todos os rounds foram jogados, finalizando a partida.");
            FinalizarPartida();
        }
        else
        {
            
            OnRoundReiniciado?.Invoke();
        }
    }

    private void FinalizarPartida()
    {
        PlayerInputHandler.PlayerType vencedorPartida =
            vitoriasJogador1 >= vitoriasJogador2
                ? PlayerInputHandler.PlayerType.Player1
                : PlayerInputHandler.PlayerType.Player2;

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

        Debug.Log($"[GameManager] Partida finalizada. Vencedor: {vencedor} | Carregando cena Vitoria...");

        OnPartidaVencida?.Invoke(vencedorPartida);

        SceneManager.LoadScene("Vitoria");
    }

    public void VoltarSelecao()
    {
        Time.timeScale = 1f;
        vitoriasJogador1 = 0;
        vitoriasJogador2 = 0;
        roundsJogados = 0;
        SceneManager.LoadScene("CharacterSelect");
    }
}
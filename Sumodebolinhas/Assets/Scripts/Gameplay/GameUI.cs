using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Ícones que representam rounds vencidos")]
    [Tooltip("Coloque 2 elementos aqui se a partida for 'melhor de 3' (2 vitórias necessárias).")]
    [SerializeField] private Image[] roundsJogador1;
    [SerializeField] private Image[] roundsJogador2;

    private void OnEnable()
    {
        // Observer: a UI escuta o GameManager em vez de ficar checando toda hora no Update
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnRoundVencido += AtualizarPlacar;
        }

        AtualizarPlacar(default);
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnRoundVencido -= AtualizarPlacar;
        }
    }

    private void AtualizarPlacar(PlayerInputHandler.PlayerType _)
    {
        if (GameManager.Instance == null)
            return;

        for (int i = 0; i < roundsJogador1.Length; i++)
        {
            roundsJogador1[i].enabled = i < GameManager.Instance.vitoriasJogador1;
        }

        for (int i = 0; i < roundsJogador2.Length; i++)
        {
            roundsJogador2[i].enabled = i < GameManager.Instance.vitoriasJogador2;
        }
    }
}
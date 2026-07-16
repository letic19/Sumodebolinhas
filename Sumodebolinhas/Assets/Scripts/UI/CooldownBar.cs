using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    [Tooltip("De qual jogador essa barra mostra o cooldown")]
    [SerializeField] private PlayerInputHandler.PlayerType jogador;

    [SerializeField] private Slider slider;

    private Bolinha bolinha;

    private void Start()
    {
        BuscarBolinha();
    }

    private void Update()
    {
        // A bolinha pode ainda não existir no primeiro frame, ou pode ter sido
        // recriada (ex: voltou pra seleção e começou outra partida) — então
        // tenta buscar de novo até achar.
        if (bolinha == null)
        {
            BuscarBolinha();
            if (bolinha == null)
                return;
        }

        slider.value = bolinha.GetPorcentagemRecarga();
    }

    private void BuscarBolinha()
    {
        if (GameManager.Instance == null)
            return;

        bolinha = jogador == PlayerInputHandler.PlayerType.Player1
            ? GameManager.Instance.bolinhaInstanciaJogador1
            : GameManager.Instance.bolinhaInstanciaJogador2;
    }
}
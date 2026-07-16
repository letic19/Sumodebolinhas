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
        Debug.Log($"[CooldownBar] Start rodou! GameObject: {gameObject.name} | Jogador configurado: {jogador} | GameManager existe: {GameManager.Instance != null}");
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

        float valor = bolinha.GetPorcentagemRecarga();
        Debug.Log($"[CooldownBar - {jogador}] bolinha: {bolinha.name} | valor: {valor}");
        slider.value = valor;
    }

    private void BuscarBolinha()
    {
        if (GameManager.Instance == null)
            return;

        bolinha = jogador == PlayerInputHandler.PlayerType.Player1
            ? GameManager.Instance.bolinhaInstanciaJogador1
            : GameManager.Instance.bolinhaInstanciaJogador2;

        Debug.Log($"[CooldownBar - {jogador}] BuscarBolinha rodou. GameManager instance ID: {GameManager.Instance.GetInstanceID()} | Encontrou bolinha: {bolinha != null}");
    }
}
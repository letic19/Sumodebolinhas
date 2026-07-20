using TMPro;
using UnityEngine;

public class MoedasUI : MonoBehaviour
{
    [Tooltip("De qual jogador essa UI mostra as moedas")]
    [SerializeField] private PlayerInputHandler.PlayerType jogador;

    [SerializeField] private TMP_Text textoMoedas;

    private Bolinha bolinha;

    private void Start()
    {
        BuscarBolinha();
    }

    private void Update()
    {
       
        if (bolinha == null)
        {
            BuscarBolinha();
            if (bolinha == null)
                return;
        }

        textoMoedas.text = bolinha.GetMoedas().ToString();
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
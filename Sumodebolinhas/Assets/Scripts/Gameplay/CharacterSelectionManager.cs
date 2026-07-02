using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("Dados das Bolinhas")]
    [SerializeField] private BolinhaData[] bolinhas;

    [Header("Interface Jogador 1")]
    [SerializeField] private TMP_Text nomeBolinhaP1;
    [SerializeField] private Image imagemBolinhaP1;

    [Header("Interface Jogador 2")]
    [SerializeField] private TMP_Text nomeBolinhaP2;
    [SerializeField] private Image imagemBolinhaP2;

    private int indiceJogador1 = 0;
    private int indiceJogador2 = 0;

    private bool jogador1Confirmou = false;
    private bool jogador2Confirmou = false;

    private void Start()
    {
        AtualizarInterfaceJogador1();
        AtualizarInterfaceJogador2();
    }

    // ---------------- JOGADOR 1 ----------------

    public void ProximoJogador1()
    {
        indiceJogador1++;

        if (indiceJogador1 >= bolinhas.Length)
            indiceJogador1 = 0;

        AtualizarInterfaceJogador1();
    }

    public void AnteriorJogador1()
    {
        indiceJogador1--;

        if (indiceJogador1 < 0)
            indiceJogador1 = bolinhas.Length - 1;

        AtualizarInterfaceJogador1();
    }

    private void AtualizarInterfaceJogador1()
    {
        Debug.Log("Quantidade: " + bolinhas.Length);
        Debug.Log("Índice: " + indiceJogador1);

        if (nomeBolinhaP1 == null)
        {
            Debug.LogError("NomeBolinhaP1 não foi configurado!");
            return;
        }

        if (bolinhas[indiceJogador1] == null)
        {
            Debug.LogError("Elemento da bolinha é nulo!");
            return;
        }

        nomeBolinhaP1.text = bolinhas[indiceJogador1].name;

        if (imagemBolinhaP1 != null && bolinhas[indiceJogador1].icone != null)
        {
            imagemBolinhaP1.sprite = bolinhas[indiceJogador1].icone;
        }

        Debug.Log("Atualizou Jogador 1");
    }

    public void ConfirmarJogador1()
    {
        jogador1Confirmou = true;

        Debug.Log("Jogador 1 escolheu: " + bolinhas[indiceJogador1].name);

        VerificarInicio();
    }

    // ---------------- JOGADOR 2 ----------------

    public void ProximoJogador2()
    {
        indiceJogador2++;

        if (indiceJogador2 >= bolinhas.Length)
            indiceJogador2 = 0;

        AtualizarInterfaceJogador2();
    }

    public void AnteriorJogador2()
    {
        indiceJogador2--;

        if (indiceJogador2 < 0)
            indiceJogador2 = bolinhas.Length - 1;

        AtualizarInterfaceJogador2();
    }

    private void AtualizarInterfaceJogador2()
    {
        nomeBolinhaP2.text = bolinhas[indiceJogador2].name;

        if (bolinhas[indiceJogador2].icone != null)
            imagemBolinhaP2.sprite = bolinhas[indiceJogador2].icone;
    }

    public void ConfirmarJogador2()
    {
        jogador2Confirmou = true;

        Debug.Log("Jogador 2 escolheu: " + bolinhas[indiceJogador2].name);

        VerificarInicio();
    }

    // ---------------- VERIFICA ----------------

    private void VerificarInicio()
    {
        if (jogador1Confirmou && jogador2Confirmou)
        {
            Debug.Log("Os dois jogadores confirmaram!");
        }
    }
}
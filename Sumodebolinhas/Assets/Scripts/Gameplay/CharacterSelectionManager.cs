using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("Dados das Bolinhas")]
   [SerializeField] private BolinhaDatabase bancoDeBolinhas;

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

    [Header("Painéis")]
    [SerializeField] private GameObject painelJogador1;
    [SerializeField] private GameObject painelJogador2;

    private void Start()
    {
        AtualizarInterfaceJogador1();
    }

    // ---------------- JOGADOR 1 ----------------

    public void ProximoJogador1()
    {
        indiceJogador1++;

        if (indiceJogador1 >= bancoDeBolinhas.bolinhas.Length)
            indiceJogador1 = 0;

        AtualizarInterfaceJogador1();
    }

    public void AnteriorJogador1()
    {
        indiceJogador1--;

        if (indiceJogador1 < 0)
            indiceJogador1 = bancoDeBolinhas.bolinhas.Length - 1;

        AtualizarInterfaceJogador1();
    }

    private void AtualizarInterfaceJogador1()
    {
        Debug.Log("Quantidade: " + bancoDeBolinhas.bolinhas.Length);
        Debug.Log("Índice: " + indiceJogador1);

        

        nomeBolinhaP1.text = bancoDeBolinhas.bolinhas[indiceJogador1].name;

        if (imagemBolinhaP1 != null && bancoDeBolinhas.bolinhas[indiceJogador1].icone != null)
        {
            imagemBolinhaP1.sprite = bancoDeBolinhas.bolinhas[indiceJogador1].icone;
        }

        Debug.Log("Atualizou Jogador 1");
    }

    public void ConfirmarJogador1()
    {
        jogador1Confirmou = true;

        Debug.Log("Jogador 1 escolheu: " + bancoDeBolinhas.bolinhas[indiceJogador1].name);

        AtualizarInterfaceJogador2();

        painelJogador1.SetActive(false);
        painelJogador2.SetActive(true);
    }

    // ---------------- JOGADOR 2 ----------------

    public void ProximoJogador2()
    {
        indiceJogador2++;

        if (indiceJogador2 >= bancoDeBolinhas.bolinhas.Length)
            indiceJogador2 = 0;

        AtualizarInterfaceJogador2();
    }

    public void AnteriorJogador2()
    {
        indiceJogador2--;

        if (indiceJogador2 < 0)
            indiceJogador2 = bancoDeBolinhas.bolinhas.Length - 1;

        AtualizarInterfaceJogador2();
    }

    private void AtualizarInterfaceJogador2()
    {
        nomeBolinhaP2.text = bancoDeBolinhas.bolinhas[indiceJogador2].name;

        if (bancoDeBolinhas.bolinhas[indiceJogador2].icone != null)
            imagemBolinhaP2.sprite = bancoDeBolinhas.bolinhas[indiceJogador2].icone;
    }

    public void ConfirmarJogador2()
    {
        jogador2Confirmou = true;

        Debug.Log("Jogador 2 escolheu: " + bancoDeBolinhas.bolinhas[indiceJogador2].name);

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
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        Debug.Log("CharacterSelectionManager: " + gameObject.name);

        AtualizarInterfaceJogador1();
        
    }
    
    private void Awake()
    {
        Debug.Log("Awake: " + gameObject.name);
    }

    // ---------------- JOGADOR 1 ----------------

    public void ProximoJogador1()
    {
        if (bancoDeBolinhas == null)
        {
            Debug.LogError("Banco de Bolinhas é NULL!");
            return;
        }

        if (bancoDeBolinhas.bolinhas == null)
        {
            Debug.LogError("Array de bolinhas é NULL!");
            return;
        }

        Debug.Log("Quantidade: " + bancoDeBolinhas.bolinhas.Length);

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
        if (nomeBolinhaP1 == null)
        {
            Debug.LogError("NomeBolinhaP1 é NULL");
            return;
        }

        if (imagemBolinhaP1 == null)
        {
            Debug.LogError("ImagemBolinhaP1 é NULL");
            return;
        }

        if (bancoDeBolinhas == null)
        {
            Debug.LogError("BancoDeBolinhas é NULL");
            return;
        }

        if (bancoDeBolinhas.bolinhas == null)
        {
            Debug.LogError("Array de bolinhas é NULL");
            return;
        }

        if (indiceJogador1 >= bancoDeBolinhas.bolinhas.Length)
        {
            Debug.LogError("Índice inválido");
            return;
        }

        if (bancoDeBolinhas.bolinhas[indiceJogador1] == null)
        {
            Debug.LogError("Bolinha do índice " + indiceJogador1 + " é NULL");
            return;
        }

        nomeBolinhaP1.text = bancoDeBolinhas.bolinhas[indiceJogador1].name;
        imagemBolinhaP1.sprite = bancoDeBolinhas.bolinhas[indiceJogador1].icone;

        Debug.Log("Atualizou Jogador 1");
    }

    public void ConfirmarJogador1()
    {
        jogador1Confirmou = true;

        GameManager.Instance.bolinhaJogador1 =
            bancoDeBolinhas.bolinhas[indiceJogador1];

        Debug.Log("Jogador 1 escolheu: " + bancoDeBolinhas.bolinhas[indiceJogador1].name);

        AtualizarInterfaceJogador2();

        painelJogador1.SetActive(false);
        painelJogador2.SetActive(true);
    }



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

        GameManager.Instance.bolinhaJogador2 =
            bancoDeBolinhas.bolinhas[indiceJogador2];

        Debug.Log("Jogador 2 escolheu: " + bancoDeBolinhas.bolinhas[indiceJogador2].name);

        VerificarInicio();
    }


    private void VerificarInicio()
    {
        if (jogador1Confirmou && jogador2Confirmou)
        {
            Debug.Log("Os dois jogadores confirmaram!");

            SceneManager.LoadScene("Gameplay");
        }
    }
}
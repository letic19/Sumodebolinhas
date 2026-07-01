using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("Dados das Bolinhas")]
    [SerializeField] private BolinhaData[] bolinhas;

    [Header("Interface Jogador 1")]
    [SerializeField] private TMP_Text nomeBolinha;
    [SerializeField] private Image imagemBolinha;

    private int indiceAtual = 0;

    private void Start()
    {
        AtualizarInterface();
    }

    public void Proximo()
    {
        indiceAtual++;

        if (indiceAtual >= bolinhas.Length)
            indiceAtual = 0;

        AtualizarInterface();
    }

    public void Anterior()
    {
        indiceAtual--;

        if (indiceAtual < 0)
            indiceAtual = bolinhas.Length - 1;

        AtualizarInterface();
    }

    private void AtualizarInterface()
    {
        nomeBolinha.text = bolinhas[indiceAtual].name;

        if (bolinhas[indiceAtual].icone != null)
            imagemBolinha.sprite = bolinhas[indiceAtual].icone;
    }
}

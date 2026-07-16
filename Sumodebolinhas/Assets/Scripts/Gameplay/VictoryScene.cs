using TMPro;
using UnityEngine;

public class VictoryScene : MonoBehaviour
{
    [SerializeField] private TMP_Text textoVitoria;
    [SerializeField] private TMP_Text textoBolinhaVencedora;

    private void Start()
    {
        if (GameManager.Instance == null)
            return;

        textoVitoria.text = GameManager.Instance.vencedor;

        if (textoBolinhaVencedora != null && GameManager.Instance.bolinhaVencedora != null)
        {
            textoBolinhaVencedora.text = "Bolinha: " + GameManager.Instance.bolinhaVencedora.name;
        }
    }

    // Ligar esse método no botão da cena de Vitória
    public void VoltarParaSelecao()
    {
        GameManager.Instance.VoltarSelecao();
    }
}
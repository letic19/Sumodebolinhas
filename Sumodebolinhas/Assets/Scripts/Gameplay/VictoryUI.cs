using TMPro;
using UnityEngine;

public class VictoryUI : MonoBehaviour
{
    public static VictoryUI Instance;

    [SerializeField] private GameObject painelVitoria;
    [SerializeField] private TMP_Text textoVitoria;

    private void Awake()
    {
        Instance = this;

        painelVitoria.SetActive(false);
    }

    public void MostrarVitoria(string vencedor)
    {
        painelVitoria.SetActive(true);

        textoVitoria.text = vencedor;

        Time.timeScale = 0f;
    }
}
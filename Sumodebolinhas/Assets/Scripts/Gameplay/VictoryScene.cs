using TMPro;
using UnityEngine;

public class VictoryScene : MonoBehaviour
{
    [SerializeField] private TMP_Text textoVitoria;

    private void Start()
    {
        textoVitoria.text = GameManager.Instance.vencedor;
    }
}
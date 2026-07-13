using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image[] vidasJogador1;
    [SerializeField] private Image[] vidasJogador2;

    private void Update()
    {
        for (int i = 0; i < vidasJogador1.Length; i++)
        {
            vidasJogador1[i].enabled = i < GameManager.Instance.vidasJogador1;
        }

        for (int i = 0; i < vidasJogador2.Length; i++)
        {
            vidasJogador2[i].enabled = i < GameManager.Instance.vidasJogador2;
        }
    }
}
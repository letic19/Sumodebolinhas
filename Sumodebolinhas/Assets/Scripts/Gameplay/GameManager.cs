using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string vencedor;

    public BolinhaData bolinhaJogador1;
    public BolinhaData bolinhaJogador2;

    public int vidasJogador1 = 3;
    public int vidasJogador2 = 3;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
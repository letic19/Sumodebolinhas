using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public string vencedor;


    public BolinhaData bolinhaJogador1;
    public BolinhaData bolinhaJogador2;


    // Vida dentro do round
    public int vidasJogador1 = 3;
    public int vidasJogador2 = 3;


    // Vitórias da partida
    public int vitoriasJogador1 = 0;
    public int vitoriasJogador2 = 0;



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



    public void IniciarGameplay()
    {
        SceneManager.LoadScene("Gameplay");

        SceneManager.LoadScene(
            "GUI",
            LoadSceneMode.Additive
        );
    }



    public void VoltarSelecao()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("CharacterSelect");
    }
}
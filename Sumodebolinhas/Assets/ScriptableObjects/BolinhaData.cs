using UnityEngine;

[CreateAssetMenu(fileName = "BolinhaData", menuName = "Game/Bolinha")]
public class BolinhaData : ScriptableObject
{
    public string bolinhaNome;

    public Sprite sprite;

    public float tamanho = 1f;

    public float velocidade = 5f;

    public float forcaEmpurrao = 10f;

    public Color corPadrao = Color.white;
}
using UnityEngine;

[CreateAssetMenu(menuName = "Sumo/Bolinha Data")]
public class BolinhaData : ScriptableObject
{
    public float velocidade;
    public float forcaEmpurrao;
    public float tamanho;

    public Sprite icone;
}
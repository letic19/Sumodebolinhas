using UnityEngine;

[CreateAssetMenu(menuName = "Bolinha")]
public class BolinhaData : ScriptableObject
{
    [Header("Atributos")]
    public float velocidade;
    public float forcaEmpurrao;
    public float tamanho;

    [Header("Menu")]
    public Sprite icone;

    [Header("Aparência")]
    public Material material;      // Se existir, usa este material
    public Color cor = Color.white; // Caso não tenha material

    [Header("Aparência Alternativa (usada quando os 2 jogadores escolhem a mesma bolinha)")]
    public Material materialAlternativo;
    public Color corAlternativa = Color.gray;
}
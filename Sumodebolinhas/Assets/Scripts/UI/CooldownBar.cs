using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    [SerializeField] private Bolinha bolinha;
    [SerializeField] private Slider slider;

    private void Update()
    {
        slider.value = bolinha.GetPorcentagemRecarga();
    }
}
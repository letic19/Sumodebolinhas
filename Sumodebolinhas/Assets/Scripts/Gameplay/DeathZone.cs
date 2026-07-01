using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Bolinha bolinha = other.GetComponent<Bolinha>();

        if (bolinha != null)
        {
            Debug.Log(bolinha.name + " caiu!");
        }
    }
}
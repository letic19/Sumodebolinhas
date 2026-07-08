using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Bolinha bolinha = other.GetComponent<Bolinha>();

        if (bolinha != null)
        {
            bolinha.ColetarMoeda();

            Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private float tempoSpawn = 5f;

    [SerializeField] private float limiteX = 15f;
    [SerializeField] private float limiteZ = 15f;

    private void Start()
    {
        InvokeRepeating(
            nameof(SpawnCoin),
            2f,
            tempoSpawn
        );
    }

    private void SpawnCoin()
    {
        float x = Random.Range(-limiteX, limiteX);
        float z = Random.Range(-limiteZ, limiteZ);

        Vector3 posicao = new Vector3(
     x,
     3.32f,
     z
 );

        Instantiate(
            coinPrefab,
            posicao,
            Quaternion.identity
        );
    }
}

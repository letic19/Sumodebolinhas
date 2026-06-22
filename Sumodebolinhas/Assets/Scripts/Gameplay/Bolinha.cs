using UnityEngine;

public class Bolinha : MonoBehaviour
{
    [SerializeField] private BolinhaData bolinhaData;

    private Rigidbody rb;

    private Vector2 moveInput;

    private PlayerInputHandler inputHandler;
    
    [SerializeField] private Bolinha inimigo;
    
    [SerializeField] private float distanciaMaximaEmpurrao = 8f;
    [SerializeField] private float multiplicadorMaximo = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        transform.localScale = Vector3.one * bolinhaData.tamanho;

        inputHandler = GetComponent<PlayerInputHandler>();
    }

    private void OnEnable()
    {
        if (inputHandler != null)
        {
            inputHandler.OnMove += ReceberMovimento;
            inputHandler.OnPush += EmpurrarInimigo;
        }
    }

    private void OnDisable()
    {
        if (inputHandler != null)
        {
            inputHandler.OnMove -= ReceberMovimento;
            inputHandler.OnPush -= EmpurrarInimigo;
        }
    }

    private void ReceberMovimento(Vector2 direcao)
    {
        moveInput = direcao;
    }

    private void FixedUpdate()
    {
        Vector3 movimento = new Vector3(
            moveInput.x,
            0,
            moveInput.y
        );

        rb.AddForce(
            movimento * bolinhaData.velocidade,
            ForceMode.Acceleration
        );
    }
    private void EmpurrarInimigo()
    {
        Debug.Log(gameObject.name + " empurrou");
        if (inimigo == null)
            return;

        Rigidbody rbInimigo = inimigo.GetComponent<Rigidbody>();

        Vector3 direcao = inimigo.transform.position - transform.position;

        direcao.y = 0f;

        direcao.Normalize();

        float distancia = Vector3.Distance(
            transform.position,
            inimigo.transform.position
        );

        float forcaFinal = 8f / Mathf.Max(distancia, 1f);
        
        Debug.Log("Distância: " + distancia);
        Debug.Log("Força: " + forcaFinal);

        rbInimigo.AddForce(
            direcao * forcaFinal,
            ForceMode.Impulse
        );
    }
}
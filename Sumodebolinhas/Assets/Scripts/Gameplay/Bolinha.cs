using UnityEngine;

public class Bolinha : MonoBehaviour
{
    [SerializeField] private BolinhaData bolinhaData;

    private Rigidbody rb;

    private Vector2 moveInput;

    private PlayerInputHandler inputHandler;

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
        }
    }

    private void OnDisable()
    {
        if (inputHandler != null)
        {
            inputHandler.OnMove -= ReceberMovimento;
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

        rb.linearVelocity = movimento * bolinhaData.velocidade;
    }
}
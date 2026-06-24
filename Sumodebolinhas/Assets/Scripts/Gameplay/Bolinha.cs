using UnityEngine;

public class Bolinha : MonoBehaviour
{
    [SerializeField] private BolinhaData bolinhaData;

    private Rigidbody rb;
    private Vector2 moveInput;
    private PlayerInputHandler inputHandler;

    [SerializeField] private Bolinha inimigo;

    private int moedas = 0;

    private float bonusForca = 0f;
    private float bonusResistencia = 0f;

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

        float velocidadeAtual = Mathf.Max(
            bolinhaData.velocidade - (moedas * 0.5f),
            bolinhaData.velocidade * 0.5f
        );

        rb.AddForce(
            movimento * velocidadeAtual,
            ForceMode.Acceleration
        );
    }

    private void EmpurrarInimigo()
    {
        if (inimigo == null)
            return;

        Rigidbody rbInimigo = inimigo.GetComponent<Rigidbody>();

        Vector3 direcao =
            inimigo.transform.position - transform.position;

        direcao.y = 0f;
        direcao.Normalize();

        float distancia = Vector3.Distance(
            transform.position,
            inimigo.transform.position
        );

        float forcaFinal =
            (8f + bonusForca) /
            Mathf.Max(distancia, 1f);

        float resistenciaInimigo =
            1f + inimigo.bonusResistencia;

        rbInimigo.AddForce(
            direcao * (forcaFinal / resistenciaInimigo),
            ForceMode.Impulse
        );

        Debug.Log(gameObject.name + " empurrou");
        Debug.Log("Distância: " + distancia);
        Debug.Log("Força Final: " + forcaFinal);
    }

    public void ColetarMoeda()
    {
        moedas++;

        bonusForca += 1f;
        bonusResistencia += 0.5f;

        Debug.Log(
            gameObject.name +
            " Moedas: " +
            moedas
        );
    }
}
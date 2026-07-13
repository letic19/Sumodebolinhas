using UnityEngine;

public class Bolinha : MonoBehaviour
{
    [SerializeField] private BolinhaData bolinhaData;
    [SerializeField] private PlayerInputHandler.PlayerType playerType;
    [SerializeField] private Transform pontoRespawn;
    
    private Rigidbody rb;
    private Vector2 moveInput;
    private PlayerInputHandler inputHandler;

    [SerializeField] private Bolinha inimigo;
    [SerializeField] private float tempoRecargaEmpurrao = 3f;

    private bool podeEmpurrar = true;
    private float tempoAtualRecarga = 0f;

    private int moedas = 0;

    private float bonusForca = 0f;
    private float bonusResistencia = 0f;

    private void Awake()
    {
        Debug.Log("Jogador: " + playerType);

        if (playerType == PlayerInputHandler.PlayerType.Player1)
        {
            bolinhaData = GameManager.Instance.bolinhaJogador1;
        }
        else
        {
            bolinhaData = GameManager.Instance.bolinhaJogador2;
        }

        Debug.Log("Bolinha carregada: " + bolinhaData.name);

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
        
        if (!podeEmpurrar)
        {
            tempoAtualRecarga += Time.fixedDeltaTime;

            if (tempoAtualRecarga >= tempoRecargaEmpurrao)
            {
                podeEmpurrar = true;
                tempoAtualRecarga = 0f;
            }
        }
    }

    private void EmpurrarInimigo()
    {
        if (inimigo == null)
            return;

        if (!podeEmpurrar)
            return;

        podeEmpurrar = false;

        Rigidbody rbInimigo = inimigo.GetComponent<Rigidbody>();

        Vector3 direcao = inimigo.transform.position - transform.position;
        direcao.y = 0;
        direcao.Normalize();

        float distancia = Vector3.Distance(
            transform.position,
            inimigo.transform.position
        );

        float forcaFinal = bolinhaData.forcaEmpurrao + bonusForca;

        float resistenciaInimigo = 1f + inimigo.bonusResistencia;

        rbInimigo.linearVelocity = Vector3.zero;

        rbInimigo.AddForce(
            direcao * (forcaFinal / resistenciaInimigo),
            ForceMode.VelocityChange
        );

        Debug.Log("Força Base: " + bolinhaData.forcaEmpurrao);
        Debug.Log("Bônus: " + bonusForca);
        Debug.Log("Força Final: " + forcaFinal);
        Debug.Log("Distância: " + distancia);
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
    
    public float GetPorcentagemRecarga()
    {
        if (podeEmpurrar)
            return 1f;

        return tempoAtualRecarga / tempoRecargaEmpurrao;
    }

    public void Morrer()
    {
        Debug.Log(">>> Morrer foi chamado!");
        
        if (playerType == PlayerInputHandler.PlayerType.Player1)
        {
            GameManager.Instance.vidasJogador1--;
        }
        else
        {
            GameManager.Instance.vidasJogador2--;
        }

        Debug.Log("Vidas J1: " + GameManager.Instance.vidasJogador1);
        Debug.Log("Vidas J2: " + GameManager.Instance.vidasJogador2);

        if (GameManager.Instance.vidasJogador1 <= 0)
        {
            VictoryUI.Instance.MostrarVitoria("JOGADOR 2 VENCEU!");
            return;
        }

        if (GameManager.Instance.vidasJogador2 <= 0)
        {
            VictoryUI.Instance.MostrarVitoria("JOGADOR 1 VENCEU!");
            return;
        }

        // Respawn
        transform.position = pontoRespawn.position;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
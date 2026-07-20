using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bolinha : MonoBehaviour
{
    [SerializeField] private BolinhaData bolinhaData;
    [SerializeField] private PlayerInputHandler.PlayerType playerType;
    [SerializeField] private Transform pontoRespawn;

    private Rigidbody rb;
    private Vector2 moveInput;

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


        if (bolinhaData == null)
        {
            Debug.LogError("BolinhaData não encontrado!");
            return;
        }


        Debug.Log("Bolinha carregada: " + bolinhaData.name);


        rb = GetComponent<Rigidbody>();

        transform.localScale = Vector3.one * bolinhaData.tamanho;


        AplicarAparencia();
               
        GameManager.Instance.RegistrarBolinha(playerType, this);
    }



   
   
    private void AplicarAparencia()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer == null)
            return;

        bool jogadoresEscolheramAMesmaBolinha =
            GameManager.Instance.bolinhaJogador1 == GameManager.Instance.bolinhaJogador2;

        bool usarAparenciaAlternativa =
            jogadoresEscolheramAMesmaBolinha &&
            playerType == PlayerInputHandler.PlayerType.Player2;

        Material materialEscolhido = usarAparenciaAlternativa
            ? bolinhaData.materialAlternativo
            : bolinhaData.material;

        Color corEscolhida = usarAparenciaAlternativa
            ? bolinhaData.corAlternativa
            : bolinhaData.cor;

        if (materialEscolhido != null)
        {
            renderer.material = materialEscolhido;
        }
        else
        {
            renderer.material.color = corEscolhida;
        }
    }



    private void OnEnable()
    {     
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnRoundReiniciado += Reiniciar;
        }
    }



    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnRoundReiniciado -= Reiniciar;
        }
    }



    private void ReceberMovimento(Vector2 direcao)
    {
        moveInput = direcao;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        ReceberMovimento(context.ReadValue<Vector2>());
    }

    public void OnPush(InputAction.CallbackContext context)
    {
        if (context.performed) EmpurrarInimigo();
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


        if (rbInimigo == null)
            return;



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
        Debug.Log("Bônus Força: " + bonusForca);
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





    public int GetMoedas()
    {
        return moedas;
    }




    public float GetPorcentagemRecarga()
    {
        if (podeEmpurrar)
            return 1f;


        return tempoAtualRecarga / tempoRecargaEmpurrao;
    }




  
    public void Morrer()
    {
        Debug.Log(">>> " + gameObject.name + " caiu da arena!");

        GameManager.Instance.RegistrarQueda(playerType);
    }




  
    public void Reiniciar()
    {
        moedas = 0;
        bonusForca = 0f;
        bonusResistencia = 0f;

        podeEmpurrar = true;
        tempoAtualRecarga = 0f;

        moveInput = Vector2.zero;

        if (pontoRespawn != null)
        {
            transform.position = pontoRespawn.position;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
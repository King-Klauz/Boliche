using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ball; //pegando o objeto da bola de boliche
    Rigidbody rb;
    public AudioSource ballAudio;
    public Text scoreUI; //Variável onde vai ser exibido o placar na tela do jogo 
    int score; //Variável que vai amazenar o valor de potuação a cada jogada
    public Text ScoreTotal; //variável parar exiber o valor total depois que os turnos acabarem

    GameObject ScorePlacar; //Pegando o Canvas do Placar normal
    GameObject ScorefinalPlacar; //Pegando Canvas do Placar Final
    GameObject CollisionCamera;
    GameObject SliderBar;

    GameObject[] pinos; //array para pegar todos os pinos do jogo e armazena-los juntos
    Vector3[] positionPinos; //array para pegar a posição de todos os pinos do jogo e reseta-los para um novo turno

    public int turn; // variavel para saber quantos turnos são permitidos até acabar o jogo

    [SerializeField]
    float power; //força aplicada na bola
    float currentPower;
    float powerVar;

    [SerializeField]
    private Slider powerBar;
    [SerializeField]
    private ChoosDirection chooseDir;

    bool isShooting; //flag para saber se a bola foi jogada 
    bool isGoingRight;

    void Start()
    {
        currentPower = 0;
        powerVar = power;
        powerBar.maxValue = power;
        score = 0;
        isShooting = false;
        isGoingRight = true;
        turn = 5;
        CollisionCamera = GameObject.FindGameObjectWithTag("CollisionCamera");
        CollisionCamera.SetActive(false);
        SliderBar = GameObject.FindGameObjectWithTag("PowerBar");
        SliderBar.SetActive(true);
        ScorefinalPlacar = GameObject.FindGameObjectWithTag("PlacarFinal");
        ScorefinalPlacar.SetActive(false);//desativando placar final da tela (ativar só no final do jogo)
        ScorePlacar = GameObject.FindGameObjectWithTag("Placar");
        ScorePlacar.SetActive(true);
        rb = ball.GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 50;
        pinos = GameObject.FindGameObjectsWithTag("Pino"); //pegando a tag dos pinos
        positionPinos = new Vector3[pinos.Length];

        for(int i = 0; i < pinos.Length; i++)
        {
            positionPinos[i] = pinos[i].transform.position; // pegando todas as posições dos pinos e armazenando para o reset 
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        
        ContarPinosCaidos(); //função para contar quantos pontos o jogador fez
        ChangeCamera();
        //turn--;


        /*if (Input.GetKeyDown(KeyCode.Return)) // Joga a bola depois de apertar "Enter" no teclado
        {
            //rb.AddForce(Vector3.forward * power);
            
            //isShooting = true;
        }*/

        if(Input.GetKey(KeyCode.Space)) // Reseta a posição de todos os pinos e da bola
        {
            score = 0;                //resetando os valores  
            isShooting = false;       //necessarios pra
            isGoingRight = true;      //uma nova jogada
            CollisionCamera.SetActive(false);
            SliderBar.SetActive(true);
            ResetPinos();
        }

        if (Input.GetKeyDown(KeyCode.Return) && !isShooting)
        {
            chooseDir.Chosen = true;
        }

        if (Input.GetKey(KeyCode.Return) && !isShooting)
        {
            PowerVariation();
        }

        if (Input.GetKeyUp(KeyCode.Return) && !isShooting)
        {
            rb.freezeRotation = false;
            chooseDir.ArrowObj.SetActive(false);
            rb.AddForce(chooseDir.ArrowObj.transform.forward * currentPower);
            isShooting = true;
            SliderBar.SetActive(false);
            ballAudio.Play();
        }

    }

    void FixedUpdate()
    {
        if (!isShooting) //chama a função de jogar a bola se ela não estiver em movimento para frente
        {
            MoveBall();
        }
    }


    void MoveBall() //FUNÇÃO PARA MOVIMENTAR A BOLA DE UM LADO PARA O OUTRO NA HORA DE JOGAR
    {
        if (Input.GetKey("a") && transform.position.x < 2.0f && !isShooting)
        {
            rb.MovePosition(rb.transform.position + (Vector3.right*Time.deltaTime));
        }

        if (Input.GetKey("d") && transform.position.x > -2.0f && !isShooting)
        {
            rb.MovePosition(rb.transform.position + (Vector3.left * Time.deltaTime));
        }
    }

    void ContarPinosCaidos()
    {
        for (int i = 0; i < pinos.Length; i++)
        {
           
            if (pinos[i].transform.eulerAngles.z > 5 && pinos[i].transform.eulerAngles.z < 355 && pinos[i].activeSelf)//verificando a rotação que o pino irá fazer, se estiver
            {                                                                                                         //dentro da condição é conciderado um ponto;                      
                score++;
                pinos[i].SetActive(false); //o pino que for derrubado não terá mais seus atributos contados, evitando que ele fique indicando que esta caindo para sempre, então desativamos ele.
            }
        }
        scoreUI.text = score.ToString(); //pegando o valor do score e depois colocando na tela
    }

    void ResetPinos() //resetando os pinos
    {
        for(int i=0; i < pinos.Length; i++) //resetando todos os pinos
        {
            pinos[i].SetActive(true);
            pinos[i].transform.position = positionPinos[i];
            pinos[i].GetComponent<Rigidbody>().velocity = Vector3.zero;         //nessas tres linhas resetamos a fisica do pino para que ele não fique estático, assim como no começo do turno
            pinos[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pinos[i].transform.rotation = Quaternion.identity;
        }

        ball.transform.position = new Vector3(0.068f, 0.18f, -8.5f); //pegando a posição inicial da bola e setando ela manualmente
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero; ////nessas tres linhas resetamos a fisica da Bola para que ela fique estática, assim como no começo do turno
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.transform.rotation = Quaternion.identity;
        chooseDir.ArrowObj.SetActive(true);
        chooseDir.Chosen = false;
        powerBar.value = 0;
        currentPower = 0;
    }

    public void PowerVariation()
    {
        currentPower += Time.deltaTime * powerVar;
        if (currentPower >= power)
        {
            powerVar *= -1;
        }
        else if (currentPower <= 0)
        {
            powerVar *= -1;
        }
        powerBar.value = currentPower;
    }

    public void ChangeCamera()
    {
        //print(rb.transform.position.z);
        if (rb.transform.position.z > 4.0f)
        {
            CollisionCamera.SetActive(true);
        }
    }
}

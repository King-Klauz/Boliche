using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ball;
    Rigidbody rb;
    public AudioSource ballAudio;
    public Text scoreUI;
    int score;
    public Text ScoreTotal;

    GameObject ScorePlacar;
    GameObject ScorefinalPlacar;

    GameObject[] pinos;
    Vector3[] positionPinos;

    public int turn;

    [SerializeField]
    float force;

    bool isShooting;
    bool isGoingRight;

    void Start()
    {
        score = 0;
        isShooting = false;
        isGoingRight = true;
        turn = 5;
        ScorefinalPlacar = GameObject.FindGameObjectWithTag("PlacarFinal");
        ScorefinalPlacar.SetActive(false);
        ScorePlacar = GameObject.FindGameObjectWithTag("Placar");
        ScorePlacar.SetActive(true);
        rb = ball.GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 50;
        pinos = GameObject.FindGameObjectsWithTag("Pino");
        positionPinos = new Vector3[pinos.Length];

        for(int i = 0; i < pinos.Length; i++)
        {
            positionPinos[i] = pinos[i].transform.position;
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        
        ContarPinosCaidos();
        //turn--;
              
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            rb.AddForce(Vector3.forward * force);
            ballAudio.Play();
            isShooting = true;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            score = 0;
            isShooting = false;
            isGoingRight = true;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            ResetPinos();
        }

        if(!isShooting)
        {
            MoveBall();
        }
    }


    void MoveBall()
    {
        if(isGoingRight)
        {
            ball.transform.Translate(Vector3.right * Time.deltaTime);
        }
        else
        {
            ball.transform.Translate(Vector3.left * Time.deltaTime);
        }

        if (ball.transform.position.x > 0.5f)
        {
            isGoingRight = false;
        }


        if(ball.transform.position.x < -0.5f)
        {
            isGoingRight = true;
        }

    }

    void ContarPinosCaidos()
    {
        for (int i = 0; i < pinos.Length; i++)
        {
           
            if (pinos[i].transform.eulerAngles.z > 5 && pinos[i].transform.eulerAngles.z < 355 && pinos[i].activeSelf)
            {
                score++;
                pinos[i].SetActive(false);
            }
        }
        scoreUI.text = score.ToString();
    }

    void ResetPinos()
    {
        for(int i=0; i < pinos.Length; i++)
        {
            pinos[i].SetActive(true);
            pinos[i].transform.position = positionPinos[i];
            pinos[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pinos[i].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            pinos[i].transform.rotation = Quaternion.identity;
        }

        ball.transform.position = new Vector3(0.068f, 0.18f, -8.5f);
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        ball.transform.rotation = Quaternion.identity;
    }
}

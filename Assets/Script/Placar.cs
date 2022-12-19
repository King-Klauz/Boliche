using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Placar : MonoBehaviour
{
    public static int derrubados;
    public Text Score;
    public bool play;
    public bool aux;

    void Start()
    {
        play = true;
        derrubados = 0;
        Score.text = ".";

    }

    // Update is called once per frame
    void Update()
    {
        if(play){
            Score.text = "derrubou " +derrubados+ " pinos.";
        }
    }
}

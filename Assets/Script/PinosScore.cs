using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinosScore : MonoBehaviour
{
    public bool caiu;
    public Transform rotacao_do_pino;
    public int derrubados_aux;

    // Start is called before the first frame update
    void Start()
    {
        caiu = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(caiu == false){
            if (rotacao_do_pino.eulerAngles.x >= 90f || rotacao_do_pino.eulerAngles.x <= -90f){
                caiu = true;
                Placar.derrubados++;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ResetBall : MonoBehaviour
{
    [SerializeField]
    private GameObject strikeText, spareText;
    private int numberOfPlays;
    private Vector3 originalPos;
    private Vector3 origialRot;
    public MovimentoBola movBola;
    private float countDown = 7;
    private bool ignoreCoroutine;

    private void Start()
    {
        originalPos = transform.position;
        origialRot = transform.eulerAngles;
    }

    private void Update()
    {
        if(transform.position.y <= -1) {
            ResetBallPos();
            ignoreCoroutine = true;
        }
    }

    public void ResetBallPos()
    {
        transform.position = originalPos;
        transform.eulerAngles = origialRot;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().freezeRotation = true;
        movBola.ChooseDir.ArrowObj.SetActive(true);
        movBola.ChooseDir.Chosen = false;
        movBola.play = false;
        numberOfPlays++;
        VerifyStrike();
    }

    public void VerifyStrike()
    {
        if(Placar.derrubados >= 10 && numberOfPlays < 1)
        {
            strikeText.SetActive(true);
        }
        else if(Placar.derrubados >= 10)
        {
            spareText.SetActive(true);
        }
    }

    public void StartResetBallRoutine()
    {
        StartCoroutine(StartCountDown());
    }

    public IEnumerator StartCountDown()
    {
        countDown = 7;
        while (countDown > 0)
        {
            countDown -= Time.deltaTime;
            yield return null;
        }
        if (!ignoreCoroutine)
        {
            ResetBallPos();
        }
        else
        {
            ignoreCoroutine = false;
        }

    }

}

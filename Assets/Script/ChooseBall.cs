using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBall : MonoBehaviour
{
    private const int MAX_INDEX_BALLS = 2;
    private Color[] colors;
    private int[] weights;
    private int currentIndex;

    // Update is called once per frame
    private void Start()
    {
        colors = new Color[MAX_INDEX_BALLS + 1];
        weights = new int[MAX_INDEX_BALLS + 1];
        CreateBalls();
    }
    void Update()
    {

        ChangeBall();
    }

    void CreateBalls()
    {
        colors[0] = Color.white;
        colors[1] = Color.red;
        colors[2] = Color.green;

        weights[0] = 6;
        weights[1] = 7;
        weights[2] = 8;

    }

    void ChangeBall()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentIndex > 0)
            {
                currentIndex--;
                GetComponent<MeshRenderer>().material.color = colors[currentIndex];
                GetComponent<Rigidbody>().mass = weights[currentIndex];
            }
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentIndex < MAX_INDEX_BALLS)
            {
                currentIndex++;
                GetComponent<MeshRenderer>().material.color = colors[currentIndex];
                GetComponent<Rigidbody>().mass = weights[currentIndex];
            }
        }
    }
}

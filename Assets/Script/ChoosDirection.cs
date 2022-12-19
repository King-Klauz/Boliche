using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosDirection : MonoBehaviour
{
    [SerializeField]
    private GameObject Arrow;

    [SerializeField]
    private float rotSpeed;

    private float rot;


    private bool isRotatingLeft, chosen;

    private void Start()
    {
        rot = Arrow.transform.eulerAngles.y;
    }

    private void Update()
    {
        if (!chosen)
        {
            if (Arrow.transform.rotation.y > -30 && isRotatingLeft)
            {
                rot -= Time.deltaTime * rotSpeed;
                Arrow.transform.rotation = Quaternion.Euler(0, rot, 0);
                if (Arrow.transform.rotation.y <= -.3f)
                {
                    isRotatingLeft = false;
                }
            }
            else if (Arrow.transform.rotation.y < 30 && !isRotatingLeft)
            {
                rot += Time.deltaTime * rotSpeed;
                Arrow.transform.rotation = Quaternion.Euler(0, rot, 0);
                if (Arrow.transform.rotation.y >= .3f)
                {
                    isRotatingLeft = true;
                }
            }
        }
    }

    public bool Chosen
    {
        get { return chosen; }
        set { chosen = value; }
    }

    public GameObject ArrowObj
    {
        get { return Arrow; }
        set { Arrow = value; }
    }

}

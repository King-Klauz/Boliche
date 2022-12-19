using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimentoBola : MonoBehaviour
{
    [SerializeField]
    private Slider powerBar;
    [SerializeField]
    private ChoosDirection chooseDir;
    private Rigidbody rb;
    public bool play = false;
    public float power, currentPower, powerVar;
    private List<Vector3> pinPositions;
    private List<Quaternion> pinRotations;
    private Vector3 ballPosition;

    public ChoosDirection ChooseDir => chooseDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentPower = 0;
        powerVar = power;
        powerBar.maxValue = powerVar;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("a") && transform.position.x < 2.0f && !play)
        {
            transform.Translate(0.1f, 0.0f, 0.0f);
        }

        if (Input.GetKey("d") && transform.position.x > -2.0f && !play)
        {
            transform.Translate(-0.1f, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown("w") && !play)
        {
            chooseDir.Chosen = true;
        }

        if (Input.GetKey("w") && !play)
        {
            PowerVariation();
        }

        if (Input.GetKeyUp("w") && !play)
        {
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<ResetBall>().StartResetBallRoutine();
            chooseDir.ArrowObj.SetActive(false);
            rb.AddForce(chooseDir.ArrowObj.transform.forward * -currentPower);
            play = true;
        }
    }
    public void PowerVariation()
    {
        currentPower += Time.deltaTime * powerVar;
        if(currentPower >= power) {
            powerVar *= -1;
        }
        else if(currentPower <= 0) {
            powerVar *= -1;
        }
        powerBar.value = currentPower;
    }
}

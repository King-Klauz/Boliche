using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBola : MonoBehaviour
{

    private Rigidbody rb;
    private bool play = false;
    public float power;
    private List<Vector3> pinPositions;
    private List<Quaternion> pinRotations;
    private Vector3 ballPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("a") && transform.position.x < 2.0f && !play){
        transform.Translate(0.1f, 0.0f, 0.0f); }
        
        if (Input.GetKey("d") && transform.position.x > -2.0f && !play){
        transform.Translate(-0.1f, 0.0f, 0.0f);}

        if (Input.GetKey("w") && !play){
        rb.AddForce(new Vector3(0.0f, 0.0f, -Mathf.Abs(power)));
        play = true;
        }
    }
}

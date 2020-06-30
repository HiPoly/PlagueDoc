using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private Rigidbody rb;

    [Range(-15f, 15f)]
    public float moveleftright;
    [Range(-15f, 15f)]
    public float movefowardback;
    [Range(0f,1f)]
    public float tiltinterval;
    [Range(0f, 20f)]
    public float tiltlimit;

    private float timer;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if ( timer > (tiltinterval * 2))
        {
            timer = 0f;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(-transform.forward * moveleftright);
        rb.AddForce(-transform.right * movefowardback);

        Quaternion rotation = (transform.rotation);

        if (timer < tiltinterval)
        {
            transform.rotation = Quaternion.Euler(rotation[0] + tiltlimit, rotation[1] +0f, rotation[2] + 0f);
        }

        if (timer > tiltinterval)
        {
            transform.rotation = Quaternion.Euler(rotation[0] - tiltlimit, rotation[1] + 0f, rotation[2] + 0f);
        }
    }
}

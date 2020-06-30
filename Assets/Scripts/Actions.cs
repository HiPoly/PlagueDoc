using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    //The doc's bounciness
    //speed of return to upright
    float smoothing = 5.0f;
    //maximum rotation angle
    float tiltAngle = 10f;

    //speed
    float docSpeed = 1.125f;

    public int stationNumber = 2;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (stationNumber != 1)
            {
                stationNumber = stationNumber - 1;
                transform.position = transform.position + new Vector3(-1 * docSpeed, 0, 0);
                transform.Rotate(0.0f, 0.0f, -1 * tiltAngle);
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (stationNumber != 3)
            {
                stationNumber = stationNumber + 1;
                transform.position = transform.position + new Vector3(+1 * docSpeed, 0, 0);
                transform.Rotate(0.0f, 0.0f, 1 * tiltAngle);
            }
        }

        //add a thing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stationNumber == 1)
            {
                Debug.Log("red");
            }
            if (stationNumber == 2)
            {
                Debug.Log("green");
            }
            if (stationNumber == 3)
            {
                Debug.Log("blue");
            }
        }

        //Tilty Stuff
        Quaternion target = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothing);
    }
}


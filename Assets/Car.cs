using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float timer;
    public Vector3 origion;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        origion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        timer += Time.deltaTime;
        if (timer > 6f)
        {
            transform.position = origion;
            timer = 0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float timer;
    public Vector3 origion;
    public float speed;
    public Renderer paint;
    private float respawn;
    public bool direction;
    // Start is called before the first frame update
    void Start()
    {
        origion = transform.position;
        respawn = Random.Range(3f, 8f);
    }

    // Update is called once per frame
    void Update()
    {  if (direction == true)
        {
            transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
        }

        if (direction == false)
        {
            transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
        }

        timer += Time.deltaTime;
        if (timer > respawn )
        {
            transform.position = origion;
            timer = 0f;

            Color RandomCarColour = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)
            );
            paint.material.color = RandomCarColour;

            respawn = Random.Range(3f, 8f);
        }
    }
}

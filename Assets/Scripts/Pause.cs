using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool paused = false;
    public GameObject pausemenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                paused = true;
                Time.timeScale = 0.0f;
                pausemenu.SetActive(true);
            } else 
            {
                paused = false;
                Time.timeScale = 1.0f;
                pausemenu.SetActive(false);
            }

        }
    }
}

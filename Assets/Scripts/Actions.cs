using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text ShowOrder1;
    public Text ShowOrder2;
    public Text ShowOrder3;

    public Renderer Redblock;
    public Renderer Greenblock;
    public Renderer Blueblock;
    public bool takeorder;
    public float timerr;
    public float timelimit;
    void Enable()
    {
        
    }

    // Drug making system
    public List<int> drug;
    public int drugfails; //1 is success, any more is a fail, I realise this is dumb, sorry
    void Update()
    {
        if(timelimit > 1f)
        {
            timelimit -= (Time.deltaTime / 50);
        }
 
        if ((takeorder == true) & (timerr >timelimit))
        {
            ShowOrder1.text = "";
            ShowOrder2.text = "";
            ShowOrder3.text = "";
            takeorder = false;
        }
        timerr += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (stationNumber != 1)
            {
                stationNumber -= 1;
                transform.position = transform.position + new Vector3(-1 * docSpeed, 0, 0);
                transform.Rotate(0.0f, 0.0f, -1 * tiltAngle);
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (stationNumber != 4)
            {
                stationNumber += 1;
                transform.position = transform.position + new Vector3(+1 * docSpeed, 0, 0);
                transform.Rotate(0.0f, 0.0f, 1 * tiltAngle);
            }
        }

        //add a thing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            

            if (stationNumber == 1)
            {
                drug.Add(1);
                Redblock.material.color = Color.yellow;
            }
            if (stationNumber == 2)
            {
                drug.Add(2);
                Greenblock.material.color = Color.yellow;
            }
            if (stationNumber == 3)
            {
                drug.Add(3);
                Blueblock.material.color = Color.yellow;
            }
            if (stationNumber == 4)
            {
                if (drug.Count == 3f)
                {
                    drugfails = 1;
                    if (drug[0] != NPCoder[0])
                    {
                        drugfails += 1;
                    }

                    if (drug[1] != NPCoder[1])
                    {
                        drugfails += 1;
                    }

                    if (drug[2] != NPCoder[2])
                    {
                        drugfails += 1;
                       
                    }
                }
         
                if (drug.Count != 3f)
                {
                    drugfails += 3;
                }


                drug.Clear();
                NPCoder.Clear();
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            Redblock.material.color = Color.red;
            Greenblock.material.color = Color.green;
            Blueblock.material.color = Color.blue;
        }
            //Tilty Stuff
            Quaternion target = Quaternion.Euler(0, 180, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothing);
    }


    public List<int> NPCoder;
    public void RecieveOrder(int order1,int order2,int order3)
    {
        takeorder = true;
        timerr = 0f;
        NPCoder.Add(order1);
        NPCoder.Add(order2);
        NPCoder.Add(order3);
      
            if (order1 == 1)
            {
                ShowOrder1.text = "Red";
            }
            if (order1 == 2)
            {
                ShowOrder1.text = "Green";
            }

            if (order1 == 3)
            {
                ShowOrder1.text = "Blue";
            }

            if (order2 == 1)
            {
                ShowOrder2.text = "Red";
            }
            if (order2 == 2)
            {
                ShowOrder2.text = "Green";
            }

            if (order2 == 3)
            {
                ShowOrder2.text = "Blue";
            }

            if (order3 == 1)
            {
                ShowOrder3.text = "Red";
            }
            if (order3 == 2)
            {
                ShowOrder3.text = "Green";
            }

            if (order3 == 3)
            {
                ShowOrder3.text = "Blue";
            }
  
      


    }
}


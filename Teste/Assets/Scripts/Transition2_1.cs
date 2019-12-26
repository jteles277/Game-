using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition2_1 : MonoBehaviour
{

    public bool InPos;


    public bool GoCam;



    public Player PlayerScript;

    public GameObject Player;
    public GameObject Cam;

    public Transform NewPosPlayer;
    public Transform NewPosCam;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (Cam.transform.position != NewPosCam.position)
        {

            InPos = false;

        }
        else
        {
            InPos = true;
        }



        if (InPos == false)
        {

            if (GoCam == true)
            {

                Cam.transform.position = Vector2.MoveTowards(Cam.transform.position, NewPosCam.position, 18 * Time.deltaTime);

            }


        }
        else
        {
            PlayerScript.Teleported = false;
            GoCam = false;

        }








    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player")
        {

            PlayerScript.Teleported = true;

            Player.transform.position = NewPosPlayer.position;



            //Cam.transform.position = Vector2.MoveTowards(Cam.transform.position, NewPosCam.position, 10 * Time.deltaTime);
            GoCam = true;



        }




    }
}

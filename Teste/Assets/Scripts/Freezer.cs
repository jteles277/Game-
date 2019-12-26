using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Freezer : MonoBehaviour
{
    public float duration;

    public float corrTime;

 
    public bool Frezing;

    public bool IsZero;


    public float StartVel;



   public float timeBtwAttack;

    public float startTimeBtwAttack;



    void Start()
    {
        StartVel = Time.timeScale;

       

    }

    // Update is called once per frame
    void Update()
    {

        corrTime = Time.timeScale;

        //if (Input.GetKeyDown("space"))
        //{
           // Freeze();
           
        //}
        //if (Input.GetKeyUp("space"))
        //{
        //UnFreeze();

        //}


        if (corrTime == 0)
        {




            StartCoroutine (UnFreeze());





        }
        else
        {
            IsZero = false;
        }



    }

    public void Freeze()
    {

        Time.timeScale = 0f;
        Frezing = true;

    }
    IEnumerator UnFreeze()
    {
        yield return new WaitForSecondsRealtime(0.12f);

        Time.timeScale = 1;
        Frezing = false;

    }


}

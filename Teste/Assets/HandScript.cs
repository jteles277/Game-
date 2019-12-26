using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    [SerializeField] private Animator anim;

    //Handle Player
    public GameObject Player;
    public Player Player_script;



    public void Update()
    {


        if (Player_script.HoldingWeapond == true)
        {

           
        }
        else
        {
            Player_script.Throwable = false;
            anim.SetBool("Animate", false);
        }



    }

    public void ChangeState()
    {


        if (Player_script.HoldingWeapond == true)
        {

            if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
            {

                anim.SetBool("Animate",true );
            }
            else
            {
                anim.SetBool("Animate",false);
            }
            



            if (Player_script.Throwable == false)
            {
                Player_script.Throwable = true;
            }
            else
            {
                Player_script.Throwable = false;
            }
        }
        
       


    }

    
}

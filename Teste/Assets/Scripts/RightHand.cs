using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{

    public Player Player;

    public Troll Troll;

    public Transform PosInStaff;
    public Transform PosInSpear;
    public Transform PosInAxe;

    public Transform PosInGoblinSpear;


    public Renderer Rend;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Player!= null)
        { 
            if (Player.Holding2 == true)
            {


                transform.position = PosInStaff.position;


            }

            if (Player.Holding1 == true)
            {


                transform.position = PosInSpear.position;


            }

            if (Player.Holding3 == true)
            {


                transform.position = PosInAxe.position;


            }


            if (Player.HoldingWeapond == true)
            {

                Rend.enabled = true;



            }
            else
            {
                Rend.enabled = false;
            }

        }


        if (Troll != null)
        {
               

                    Rend.enabled = true;
                    transform.position = PosInGoblinSpear.position;


                
              


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{
    public Renderer Rend;
    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Player.HoldingWeapond == false)
        {

            Rend.enabled = true;
           


        }else
        {
            Rend.enabled = false;
        }
    }
}

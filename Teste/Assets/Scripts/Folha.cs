using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folha : MonoBehaviour
{


    public Animator anim;







    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        

        if (col.gameObject.tag == "Objects")
        {
            anim.SetTrigger("Mexer");
        }
        if (col.gameObject.tag == "Player")
        {
            anim.SetTrigger("Mexer");
        }
        


    }
}

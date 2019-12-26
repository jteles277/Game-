using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{

    public GameObject Particle;

    public Animator Camanim;

    public float Damage;


    public Animator anim;

    public Rigidbody2D rb;

    [SerializeField] enum TrollA { Troll };
    [SerializeField] TrollA Troll1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == "Enemy")
        {



            Debug.Log(col);

            Collider2D EnemysToDamage = col;


           

            col.gameObject.GetComponent(Troll1.ToString()).GetComponent<Troll>().Hit(Damage, Particle);
           







            Debug.Log("hit");
        }

        if (col.gameObject.tag == "Objects")
        {
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Thrown"))
            {
                Camanim.SetTrigger("LitleShake");

                Debug.Log("Shock");
                rb.velocity = Vector2.zero;

            }

        }
        if (col.gameObject.tag == "WeapondObjects")
        {
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Thrown"))
            {
                Camanim.SetTrigger("LitleShake");

                Debug.Log("Shock");
                rb.velocity = Vector2.zero;

            }

        }


    }
    
}

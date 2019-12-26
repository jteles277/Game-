using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualSpearScript : MonoBehaviour
{
    public Animator Camanim;

    public float Damage;


    public Animator anim;

    public Rigidbody2D rb;

    [SerializeField] enum PlayerA { Player };
    [SerializeField] PlayerA Player1;
   


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

        if (col.gameObject.tag == "Player")
        {



            Debug.Log(col);

            Collider2D EnemysToDamage = col;




            col.gameObject.GetComponent(Player1.ToString()).GetComponent<Player>().Hit(Damage);








            Debug.Log("hit");
        }

       


    }
}

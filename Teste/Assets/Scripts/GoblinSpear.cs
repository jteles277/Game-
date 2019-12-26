using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSpear : MonoBehaviour
{



    public Animator Camanim;





    public Transform TrollPos;

    private State state;

    public Transform PlayerPos;

    public Player Player;

    [SerializeField] enum PlayerA { Player };
    [SerializeField] PlayerA Player1;

    public float offset;

    public Troll TrollScript;

    public Rigidbody2D rb;

    public float AttackTime;

    public float timeBtwAttack;
    public float startTimeBtwAttack;

    public float timeBtwDamage;
    public float startTimeBtwDamage;

    public float Force;

    public float Damage = 1;

    public Animator anim;

    private enum State
    {

       

        WithPlayer,

        Attack,


    }


    // Start is called before the first frame update
    void Start()
    {


        
        state = State.WithPlayer;

    }
    private void TryPlayerGrabShield()
    {


        

            
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;

        state = State.WithPlayer;


    }


    void FixedUpdate()
    {
        

        switch (state)
        {
            case State.Attack:


                transform.position = Vector3.MoveTowards(transform.position, TrollPos.position, 1);

                break;
        }



    }


    void LateUpdate()
    {

        switch (state)
        {
            case State.WithPlayer:

                transform.position = TrollPos.position;

                break;
        }


    }

    public void Rotate()
    {


        Vector2 ThrowDir = new Vector2(PlayerPos.position.x - transform.position.x, PlayerPos.position.y - transform.position.y).normalized;
        float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

    }



    // Update is called once per frame
    void Update()
    {
        if (TrollScript.InAttackRange)
        {
            if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {

            }
            else
            {
                    if (timeBtwAttack <= 0)
                    {
                            Rotate();



                            anim.SetTrigger("Attack");

                            Vector2 ThrowDir = new Vector2(PlayerPos.position.x - transform.position.x, PlayerPos.position.y - transform.position.y).normalized;
                            rb.isKinematic = false;
                            //rb.AddForce(ThrowDir.normalized * Force, ForceMode2D.Impulse);

                            Invoke("TryPlayerGrabShield", AttackTime);

                            state = State.Attack;

                            timeBtwAttack = startTimeBtwAttack;
                
                    }
                     else
                     {

                            Rotate();
                            timeBtwAttack -= Time.deltaTime;

                     }

            }



        }
       

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {





            Collider2D EnemysToDamage = col;


           

            col.gameObject.GetComponent(Player1.ToString()).GetComponent<Player>().Hit(Damage);


           







        }



    }

}

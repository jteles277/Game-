using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{

    public GameObject Particle;
    //Particles 

    [Header("Particles - Counters")]
    //Counters

    public float startTimeBtwSpawn;
    public float timeBtwSpawn;

    [Header("Particles - References")]
    //References

    public GameObject HitParticle;
    public GameObject Explosion;

    [Header("Particles - Spawners")]
    //Spawners

    public Transform Hitparticle;
    public Transform SpearHitparticle;



    public float Dist;
    public float step;


    public float offset;
    public Animator anim;
    
    public float MouseX;
    public float MouseY;

    public float thrustGo;
    public float thrustBack;

    //Physics Reference
    private Rigidbody2D rb;



    //Handle adefend
    public GameObject my_Player;
    Player my_Player_script;

    public float Damage = 79;

    public GameObject Troll;
    Troll Troll_script;

    [SerializeField] enum TrollA { Troll };
    [SerializeField] TrollA Troll1;



    //Throwing System

    [SerializeField] private Transform PlayerPos;


    private State state;

    public float GrabDistance;


    public float distFrom;

    public float throwForce;

    public bool Throw;


    public float AttackTime;

    public float AttackForce;

    public bool InJoyArea;

    private enum State
    {

        Thrown,

        WithPlayer,

        Attack,
       

    }



    // Start is called before the first frame update
    void Start()
    {
        Troll_script = Troll.GetComponent<Troll>();

        my_Player_script = my_Player.GetComponent<Player>();

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();


        //Throwing System



    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.Thrown:

                if ( rb.velocity.x < 0.01 && rb.velocity.x < 0.01 && rb.velocity.x > -0.01 && rb.velocity.x > -0.01)

                {
                        TryPlayerGrabShield();
                }

                break;
        }

        switch (state)
        {
            case State.Attack:

                if (Input.GetMouseButton(0) && !InJoyArea)                
                {
                    Rotate();

                }
                transform.position = Vector3.MoveTowards(transform.position, PlayerPos.position, thrustBack);

                break;
        }



    }


    void LateUpdate()
    {

        switch (state)
        {
            case State.WithPlayer:

                transform.position = PlayerPos.position;

                break;
        }


    }



    private void TryPlayerGrabShield()
    {


        if (Vector2.Distance(transform.position, PlayerPos.position) < GrabDistance)
        {

            state = State.WithPlayer;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Debug.Log("Catched");

        }

    }



    // Update is called once per frame
    void Update()
    {

        //SetSpearDirections
        Vector3 Dir = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y, transform.position.z).normalized;
        Vector3 DirBack = new Vector3(PlayerPos.transform.position.x - transform.position.x, PlayerPos.transform.position.y - transform.position.y, transform.position.z);

        Dist = Vector3.Distance(transform.position, PlayerPos.position);

        //SetStep

        step = Dist * thrustBack * Time.deltaTime;






        if (MouseX < 1.5 && MouseY < -0.9 && MouseX > 0.6 && MouseY > -1.8)
        {
            InJoyArea = true;
        }
         else
         {
             InJoyArea = false;
         }








        if (state == State.WithPlayer && !InJoyArea)   //quando a esta com o player
        {
            if (Input.GetMouseButtonUp(0))            //quando larga
            {
                timeBtwSpawn = startTimeBtwSpawn;
                anim.SetBool("Charging", false);
            }

            if (Input.GetMouseButton(0))                //enquanto segura 
            {
                Rotate();
                if (timeBtwSpawn > 0)
                {

               
                    timeBtwSpawn -= Time.deltaTime;
                    Throw = false;

                    anim.SetBool("Charging", true);



                }
                 else                                   
                 {
                    anim.SetBool("Charging", false);
                    Throw = true;
                 }
            }
            else                                               
            {
                
                timeBtwSpawn = startTimeBtwSpawn;
                anim.SetBool("Charging", false);
            }
        }
        else                                               //enquanto n esta com o player
        {
            Throw = false;
            timeBtwSpawn = startTimeBtwSpawn;
            anim.SetBool("Charging", false);
        }

        //Throwing System

        Vector2 ThrowDir = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y).normalized;

        distFrom = Vector2.Distance(transform.position, PlayerPos.position);

        if (Input.GetMouseButtonUp(0) && Throw && MouseX < 2.7 && MouseY < 2.48 && MouseX > 0.84 && MouseY > -1.534 && !InJoyArea)
        {
            if (state == State.WithPlayer)
            {
                Debug.Log("Thrown");

                Rotate();

                rb.isKinematic = false;
                rb.AddForce(ThrowDir.normalized * throwForce, ForceMode2D.Impulse);


                state = State.Thrown;
            }
        }else if (Input.GetMouseButtonDown(0) && !Throw  && state == State.WithPlayer && !InJoyArea)
        {


            Rotate();

            rb.isKinematic = false;
            rb.AddForce(ThrowDir.normalized * AttackForce, ForceMode2D.Impulse);


            Debug.Log("Attack");
            Invoke("TryPlayerGrabShield", AttackTime);

          


            state = State.Attack;

        }















        


        //if (Dist > 0.02f)
        //{
        // transform.position = Player.position;
        //}


        //if (Input.GetKeyDown(KeyCode.Z) && Dist < 0.07f)
        //{
        //anim.SetTrigger("Attack");
        //Rotate();
        // rb.AddForce(Dir * thrustGo, ForceMode2D.Force); // o vetor dir tem de ser neutralized


        //}

        //MouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        //transform.position = Vector3.MoveTowards(transform.position, Player.position, step);


        MouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        MouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;



    }






    //Throwing System













    void Rotate()
    {
        Vector3 AttackDir = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y, transform.position.z);
        float angle = Mathf.Atan2(AttackDir.y, AttackDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent(Troll1.ToString()) != null)
        {



            Troll_script.Hit(Damage, Particle);

            Debug.Log("hit");
        }

        if (col.gameObject.tag == "Objects")
        {
            Debug.Log("Shock");
            rb.velocity = Vector2.zero;

        }


    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{

    //if (collision.gameObject.tag == "Objects")
    //{
    //Debug.Log("Shock");
    //rb.velocity = Vector2.zero;

    //}

    //}

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PlayerPos.position, GrabDistance);


    }

    void Ready()
    {

        Instantiate(Explosion, SpearHitparticle.position, Quaternion.identity);

    }

}

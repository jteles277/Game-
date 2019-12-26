using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHands : MonoBehaviour
{

    public GameObject Particle;
    //Particles 

    [Header("Particles - References")]
    //References

    public GameObject HitParticle;

    [Header("Particles - Spawners")]
    //Spawners

    public Transform Hitparticle;




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

    public bool CantThrow;

    public float throwForce;

    private enum State
    {
       WithPlayer,
       Thrown,
      

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

                TryPlayerGrabShield();

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


        if (Vector2.Distance(transform.position, PlayerPos.position) < GrabDistance && rb.velocity.x < 0.01 && rb.velocity.x < 0.01 && rb.velocity.x > -0.01 && rb.velocity.x > -0.01)
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
        

        //Throwing System

        distFrom = Vector2.Distance(transform.position, PlayerPos.position);

        if (Input.GetMouseButtonDown(0) && MouseX < 2.7 && MouseY < 2.48 && MouseX > 0.84 && MouseY > -1.534)
        {
            if (state == State.WithPlayer)
            { 
                Debug.Log("Thrown");

                Rotate();
                Vector2 ThrowDir = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y).normalized;

                
                    //transform.position = new Vector2( PlayerPos.position.x + ThrowDir.x * GrabDistance * 1.2f, PlayerPos.position.y + ThrowDir.y * GrabDistance * 1.2f);
                

                
                rb.isKinematic = false;
                rb.AddForce(ThrowDir.normalized * throwForce, ForceMode2D.Impulse);
                

                

                state = State.Thrown;
            }
        }















        //SetSpearDirections
        Vector3 Dir = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y, transform.position.z).normalized;
                        Vector3 DirBack = new Vector3(PlayerPos.transform.position.x - transform.position.x, PlayerPos.transform.position.y - transform.position.y, transform.position.z);

                        Dist = Vector3.Distance(transform.position, PlayerPos.position);

        //SetStep

                        step = Dist * thrustBack * Time.deltaTime;


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

}

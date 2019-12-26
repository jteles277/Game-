using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTest : MonoBehaviour
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

    private Vector2 startingPoint;
    private int leftTouch = 99;
    private int RightTouch = 66;

    public Camera Cam;

    public GameObject circle;
    public List<TouchLocation> touches = new List<TouchLocation>();

    public bool HoldingTouch;

    public bool WithPlayer;

    public float posicao;

    public float MouseX;

    public float MouseY;


    //SlowDownTime

        public TimeManager TimeManager;


    public int Numero;
  

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


        Numero = -1;

        //Throwing System

            TimeManager.SlowDown();

    }
    private void ComeBack()
    {

        transform.position = Vector3.MoveTowards(transform.position, PlayerPos.position, thrustBack);


    }
    void FixedUpdate()
    {
        switch (state)
        {
            case State.Thrown:

                if (rb.velocity.x < 0.01 && rb.velocity.x < 0.01 && rb.velocity.x > -0.01 && rb.velocity.x > -0.01)

                {
                    TryPlayerGrabShield();
                }

                break;
        }

        switch (state)
        {
            case State.Attack:

                if (Input.GetMouseButton(0) && !InJoyArea)                                                              //See this
                {
                    
                }
                ComeBack();
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

        if(PlayerPos != null)
        { 


                if (Vector2.Distance(transform.position, PlayerPos.position) < GrabDistance && my_Player_script.HoldingWeapond == false)
                {

                    state = State.WithPlayer;
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true;
                    // Debug.Log("Catched");

                }
                 else if (Vector2.Distance(transform.position, PlayerPos.position) < GrabDistance && my_Player_script.Holding1 == true)
                 {

                     state = State.WithPlayer;
                     rb.velocity = Vector2.zero;
                     rb.isKinematic = true;
                     // Debug.Log("Catched");
                 }


        }
    }



    // Update is called once per frame
    void Update()
    {



        MouseX = Input.mousePosition.x;

        MouseY = Input.mousePosition.y;




        if (state == State.WithPlayer)
        {
            WithPlayer = true;

        }
        else if (state == State.Attack)
        {
            WithPlayer = true;
        }
        else
        {
            WithPlayer = false;
        }





        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            Vector2 touchPos = new Vector2(t.position.x, t.position.y);

            if (t.phase == TouchPhase.Began)
            {

                posicao = touchPos.x;

                if (touchPos.x < 404.1 && touchPos.y < 399.7 && touchPos.x > 8.9 && touchPos.y > 8.9)
                {

                     Debug.Log(touchPos);

                     leftTouch = t.fingerId;
                     startingPoint = touchPos;

                }



                    //if (touchPos.x < 1.37 && touchPos.y < -1.1 && touchPos.x > 0.36 && touchPos.y > -1.95)
                    //{
                    // Debug.Log(touchPos);

                    // leftTouch = t.fingerId;
                    // startingPoint = touchPos;

                    //}
                    //else if (touchPos.x < 1.37 && touchPos.y < -7.48 && touchPos.x > 0.36 && touchPos.y > -8.43)
                    //{

                    //leftTouch = t.fingerId;
                    //startingPoint = touchPos;

                    //}
                else
                {
                   
                    Debug.Log(Input.touchCount);


                    touches.Add(new TouchLocation(t.fingerId, createCircle(t)));
                    Numero = Numero + 1;

                    HoldingTouch = true;

                    //Debug.Log(touchPos);
                    RightTouch = t.fingerId;
                    
                    TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId == t.fingerId);

                    thisTouch.circle.transform.position = getTouchPosition(t.position);
                    Vector2 circlePos = thisTouch.circle.transform.position;

                    if (!Throw && state == State.WithPlayer)
                    {




                        anim.SetTrigger("Attack");


                        Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;
                        rb.isKinematic = false;
                        rb.AddForce(ThrowDir.normalized * AttackForce, ForceMode2D.Impulse);


                        //Debug.Log("Attack");
                        Invoke("TryPlayerGrabShield", AttackTime);


                        float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);


                        state = State.Attack;

                    }
                }



            }
            if (t.phase == TouchPhase.Moved && leftTouch == t.fingerId)
            {
                Vector2 offset = touchPos - startingPoint;
                Vector2 direction = Vector2.ClampMagnitude(offset, 1f);

                //Debug.Log(direction);
            }
            else if (t.phase == TouchPhase.Moved)
            {
                TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId == t.fingerId);
                thisTouch.circle.transform.position = getTouchPosition(t.position);


                if (HoldingTouch && state == State.WithPlayer && thisTouch.circle.name == "Touch-1")
                {
                    Vector2 circlePos = thisTouch.circle.transform.position;
                    Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;
                    float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

                }
                else if (HoldingTouch && state == State.WithPlayer && thisTouch.circle.name == "Touch1")
                {
                    Vector2 circlePos = thisTouch.circle.transform.position;
                    Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;
                    float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

                }
            }
            if (t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                leftTouch = 99;
            }
            else if (t.phase == TouchPhase.Ended)
            {
                TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId == t.fingerId);
                thisTouch.circle.transform.position = getTouchPosition(t.position);
                Vector2 circlePos = thisTouch.circle.transform.position;

                if (state == State.WithPlayer && Throw && thisTouch.circle.name == "Touch-1")
                {
                    Debug.Log("Thrown");

                    anim.SetTrigger("Throw");

                    Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;

                    rb.isKinematic = false;
                    rb.AddForce(ThrowDir.normalized * throwForce, ForceMode2D.Impulse);

                    float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);


                    state = State.Thrown;
                }
                else if (state == State.WithPlayer && Throw && thisTouch.circle.name == "Touch1")
                {
                    Debug.Log("Thrown");

                    anim.SetTrigger("Throw");

                    Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;

                    rb.isKinematic = false;
                    rb.AddForce(ThrowDir.normalized * throwForce, ForceMode2D.Impulse);

                    float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);


                    state = State.Thrown;
                }



                HoldingTouch = false;











                Destroy(thisTouch.circle);
                touches.RemoveAt(touches.IndexOf(thisTouch));
                Numero = Numero - 1;

               RightTouch = 66;



            }


            if (HoldingTouch && state == State.WithPlayer)
            {
                
               
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
            if (state != State.WithPlayer)
            {
                Throw = false;
             

            }
            if (!HoldingTouch)
            {
                anim.SetBool("Charging", false);

            }

            ++i;

        }



       
    }


    Vector2 getTouchPosition(Vector2 touchPosition)
    {

        return Cam.ScreenToWorldPoint(new Vector2(touchPosition.x, touchPosition.y));

    }

    GameObject createCircle(Touch t)
    {
        GameObject c = Instantiate(circle) as GameObject;

      


        c.name = "Touch" + Numero;

        

        c.transform.position = getTouchPosition(t.position);
        return c;

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
        if (col.gameObject.tag == "Enemy")
        {



            col.gameObject.GetComponent(Troll1.ToString()).GetComponent<Troll>().Hit(Damage, Particle);

            Debug.Log("hit");
        }

        if (col.gameObject.tag == "Objects")
        {

            if (state == State.Thrown)
            {
                
                Debug.Log("Shock");
                rb.velocity = Vector2.zero;

            }
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
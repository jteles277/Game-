using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeManager : MonoBehaviour
{


    [SerializeField] private GameObject gameOverUI;


    public bool ShowGrabIcon;

    public bool ThouchingIcon;

    public float GrabIconRange;

    public GameObject PrefabGrabIcon;

    public IconHandler IconHandler_script;

    public IconHandler IconHandler2_script;

    //Particles 

    [Header("Particles - Counters")]
    //Counters

    public float startTimeBtwSpawn;
    public float timeBtwSpawn;

    public float startTimeBtwAttack;
    public float timeBtwAttack;

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
    public Rigidbody2D rb;



    //Handle 
    public GameObject Player;
    public Player Player_script;



    public float Damage = 79;


    public Troll TrollScript;

    public GameObject Troll;
    Troll Troll_script;


    public GameObject SpearParticle;
    SpearParticle SpearParticle_script;






    [SerializeField] enum TrollA { Troll };
    [SerializeField] TrollA Troll1;



    //Throwing System

    [SerializeField] private Transform PlayerPos;

    [SerializeField] private Transform DashingAssistantPos;


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

    public float rotationSpeed;


    public int Numero;


    //SlowDownTime

        public TimeManager TimeManager;


    


    


    public bool Attacking;




    private enum State
    {

        Thrown,

        WithPlayer,

        Attack,


    }



    // Start is called before the first frame update
    void Start()
    {

       

                                                                                                                //PrefabGrabIcon = GameObject.FindGameObjectsWithTag("Icon");


        Troll_script = Troll.GetComponent<Troll>();

        SpearParticle_script = SpearParticle.GetComponent<SpearParticle>();


        gameOverUI.SetActive(false);

        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();


        Numero = -1;

        //Throwing System
             TimeManager.SlowDown();


    }
    
    void FixedUpdate()
    {

        
        PrefabGrabIcon = GameObject.FindWithTag("Icon");



        if (ShowGrabIcon == true)
        {

            gameOverUI.SetActive(true);

        }
        else
        {

            gameOverUI.SetActive(false);

        }







        if(state == State.Thrown)
        {

            anim.SetBool("Thrown", true);

        }
        else
        {

            anim.SetBool("Thrown", false);

        }








        



        switch (state)
        {
            case State.Thrown:

                

                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * rb.velocity.magnitude);

                if (rb.velocity.x < 0.01 && rb.velocity.x < 0.01 && rb.velocity.x > -0.01 && rb.velocity.x > -0.01)

                {
                    //TryPlayerGrabShield();                              aqui meter para poder aparecer o icon 


                    if (Vector2.Distance(transform.position, PlayerPos.position) < GrabDistance && Player_script.HoldingWeapond == false)
                    {

                        ShowGrabIcon = true;

                    }
                    else if (Vector2.Distance(transform.position, PlayerPos.position) < GrabDistance && WithPlayer == true)
                    {

                        ShowGrabIcon = true;

                    }
                    else
                    {

                        ShowGrabIcon = false;

                    }


                }

                break;
        }

        switch (state)
        {
            case State.Attack:

               

                if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                {
                    transform.position = PlayerPos.position;
                    TryPlayerGrabShield();
                }
                else
                {
                    
                }

                
                break;
        }



    }
   

    void LateUpdate()
    {

        switch (state)
        {
            case State.WithPlayer:

                if (PlayerPos != null)
                {

                    ShowGrabIcon = false;



                        if (HoldingTouch == false && anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
                        {
                           transform.eulerAngles = new Vector3(0, 0, -90);
                        }


                   

                   
                   
                    transform.position = PlayerPos.position;

                }


                break;
        }


    }



    private void TryPlayerGrabShield()
    {

        if (PlayerPos != null)
        {


            if (Vector2.Distance(transform.position, PlayerPos.position) < GrabDistance && Player_script.HoldingWeapond == false)
            {

                         //ShowGrabIcon = true;

                //if (Input.GetKeyUp("space"))
                //{

                    state = State.WithPlayer;
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true;
                    Debug.Log("Catched");
                    ShowGrabIcon = false;
                //}

                

            }
            else if (Vector2.Distance(transform.position, PlayerPos.position) < GrabDistance && WithPlayer == true)
            {

               

                    state = State.WithPlayer;
                    rb.velocity = Vector2.zero;
                    rb.isKinematic = true;
                    Debug.Log("Catched");


                
            }
            else
            {

                //ShowGrabIcon = false;

            }




        }

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            if (IconHandler_script.FindClosestEnemy().transform.position == gameOverUI.transform.position)
            {

                TryPlayerGrabShield();
            }

        }
        if (Input.GetKeyUp("v"))
        {

            anim.SetBool("Charging", false);

            Debug.Log(state);

            anim.SetTrigger("Attack");


            state = State.Attack;
           


           





        }

        MouseX = Input.mousePosition.x;
        MouseY = Input.mousePosition.y;


        if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {

            Attacking = true;

        }
        else
        {
            Attacking = false;
        }





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

        
        








        if (timeBtwAttack <= 0)
        {




            

        }
        else
        {

            timeBtwAttack -= Time.deltaTime;
        }




        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            Vector2 touchPos = new Vector2(t.position.x, t.position.y);

            

                if (t.phase == TouchPhase.Began)
                {

              


                if (IconHandler_script.FindClosestEnemy() != null)
                { 

                    if (getTouchPosition(touchPos).x < IconHandler_script.FindClosestEnemy().transform.position.x + GrabIconRange && getTouchPosition(touchPos).y < IconHandler_script.FindClosestEnemy().transform.position.y + GrabIconRange && getTouchPosition(touchPos).x > IconHandler_script.FindClosestEnemy().transform.position.x - GrabIconRange && getTouchPosition(touchPos).y > IconHandler_script.FindClosestEnemy().transform.position.y - GrabIconRange)
                        {


                            if(IconHandler_script.FindClosestEnemy().transform.position == gameOverUI.transform.position)
                            { 

                                TryPlayerGrabShield();
                            }


                        }
                    else if (IconHandler2_script.FindClosestEnemy() != null)
                    {
                        if (getTouchPosition(touchPos).x < IconHandler2_script.FindClosestEnemy().transform.position.x + GrabIconRange && getTouchPosition(touchPos).y < IconHandler2_script.FindClosestEnemy().transform.position.y + GrabIconRange && getTouchPosition(touchPos).x > IconHandler2_script.FindClosestEnemy().transform.position.x - GrabIconRange && getTouchPosition(touchPos).y > IconHandler2_script.FindClosestEnemy().transform.position.y - GrabIconRange)
                        {
                            if (IconHandler2_script.FindClosestEnemy().transform.position == gameOverUI.transform.position)
                            {

                                TryPlayerGrabShield();
                            }

                        }
                        else if (touchPos.x < 404.1 && touchPos.y < 399.7 && touchPos.x > 8.9 && touchPos.y > 8.9)
                        {

                            Debug.Log(touchPos);

                            leftTouch = t.fingerId;
                            startingPoint = touchPos;

                        }
                        else if (touchPos.x < 1011 && touchPos.y < 220 && touchPos.x > 846.9 && touchPos.y > 27.6)
                        {

                        }
                        else
                        {




                            touches.Add(new TouchLocation(t.fingerId, createCircle(t)));

                            Numero = Numero + 1;



                            HoldingTouch = true;

                            //Debug.Log(touchPos);
                            RightTouch = t.fingerId;
                            startingPoint = touchPos;
                            TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId == t.fingerId);


                            Vector2 circlePos = thisTouch.circle.transform.position;

                            //my_Player_script.Dash(circlePos);

                            if (state == State.WithPlayer && Player_script.Throwable)
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

                            else if (!Throw && state == State.WithPlayer && Player_script.Throwable == false)
                            {


                                anim.SetTrigger("Attack");




                                Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;
                                rb.isKinematic = false;
                                rb.AddForce(ThrowDir.normalized * AttackForce, ForceMode2D.Impulse);


                                Debug.Log("Attack");
                                Invoke("TryPlayerGrabShield", AttackTime);




                                int ran = Random.Range(0, 2);


                                if (ran == 0)
                                {
                                    offset = 90;
                                }
                                else
                                {
                                    offset = -90;
                                }


                                float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);


                                state = State.Attack;

                            }
                        }
                    }
                    else if (touchPos.x < 404.1 && touchPos.y < 399.7 && touchPos.x > 8.9 && touchPos.y > 8.9)
                    {

                        Debug.Log("Errou");

                        leftTouch = t.fingerId;
                        startingPoint = touchPos;





                    }
                    else if (touchPos.x < 1011 && touchPos.y < 220 && touchPos.x > 846.9 && touchPos.y > 27.6)
                    {

                    }
                    else
                    {




                        touches.Add(new TouchLocation(t.fingerId, createCircle(t)));

                        Numero = Numero + 1;



                        HoldingTouch = true;

                        //Debug.Log(touchPos);
                        RightTouch = t.fingerId;
                        startingPoint = touchPos;
                        TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId == t.fingerId);


                        Vector2 circlePos = thisTouch.circle.transform.position;

                        Player_script.Dash(circlePos);

                        if (state == State.WithPlayer && Player_script.Throwable)
                        {
                            Debug.Log("Thrown");

                            anim.SetBool("Thrown", true);

                            Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;

                            rb.isKinematic = false;
                            rb.AddForce(ThrowDir.normalized * throwForce, ForceMode2D.Impulse);

                            float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                            transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);


                            state = State.Thrown;
                        }
                        else if (!Throw && state == State.WithPlayer)
                        {


                            if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                            {

                            }
                            else
                            {

                                Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;

                                //anim.SetBool("Thrown", true);

                                float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);



                            }




                        }
                    }

                }
                else if (IconHandler2_script.FindClosestEnemy() != null)
                {
                    if (getTouchPosition(touchPos).x < IconHandler2_script.FindClosestEnemy().transform.position.x + GrabIconRange && getTouchPosition(touchPos).y < IconHandler2_script.FindClosestEnemy().transform.position.y + GrabIconRange && getTouchPosition(touchPos).x > IconHandler2_script.FindClosestEnemy().transform.position.x - GrabIconRange && getTouchPosition(touchPos).y > IconHandler2_script.FindClosestEnemy().transform.position.y - GrabIconRange)
                    {
                        if (IconHandler2_script.FindClosestEnemy().transform.position == gameOverUI.transform.position)
                        {

                            TryPlayerGrabShield();
                        }

                    }
                    else if (touchPos.x < 404.1 && touchPos.y < 399.7 && touchPos.x > 8.9 && touchPos.y > 8.9)
                    {

                        Debug.Log(touchPos);

                        leftTouch = t.fingerId;
                        startingPoint = touchPos;

                    }
                    else if (touchPos.x < 1011 && touchPos.y < 220 && touchPos.x > 846.9 && touchPos.y > 27.6)
                    {

                    }
                    else
                    {




                        touches.Add(new TouchLocation(t.fingerId, createCircle(t)));

                        Numero = Numero + 1;



                        HoldingTouch = true;

                        //Debug.Log(touchPos);
                        RightTouch = t.fingerId;
                        startingPoint = touchPos;
                        TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId == t.fingerId);


                        Vector2 circlePos = thisTouch.circle.transform.position;

                        //my_Player_script.Dash(circlePos);

                        if (state == State.WithPlayer && Player_script.Throwable)
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

                        else if (!Throw && state == State.WithPlayer && Player_script.Throwable == false)
                        {


                            anim.SetTrigger("Attack");




                            Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;
                            rb.isKinematic = false;
                            rb.AddForce(ThrowDir.normalized * AttackForce, ForceMode2D.Impulse);


                            Debug.Log("Attack");
                            Invoke("TryPlayerGrabShield", AttackTime);




                            int ran = Random.Range(0, 2);


                            if (ran == 0)
                            {
                                offset = 90;
                            }
                            else
                            {
                                offset = -90;
                            }


                            float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                            transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);


                            state = State.Attack;

                        }
                    }
                }

                else if (touchPos.x < 404.1 && touchPos.y < 399.7 && touchPos.x > 8.9 && touchPos.y > 8.9)
                {

                    Debug.Log("Errou");

                    leftTouch = t.fingerId;
                    startingPoint = touchPos;





                }
                else if (touchPos.x < 1011 && touchPos.y < 220 && touchPos.x > 846.9 && touchPos.y > 27.6)
                {

                }
                else
                {




                    touches.Add(new TouchLocation(t.fingerId, createCircle(t)));

                    Numero = Numero + 1;



                    HoldingTouch = true;

                    //Debug.Log(touchPos);
                    RightTouch = t.fingerId;
                    startingPoint = touchPos;
                    TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId == t.fingerId);


                    Vector2 circlePos = thisTouch.circle.transform.position;

                    Player_script.Dash(circlePos);


                    if (!Throw && state == State.WithPlayer)
                    {


                        if (state == State.WithPlayer && Player_script.Throwable)
                        {
                            Debug.Log("Thrown");

                            anim.SetBool("Thrown", true);

                            Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;

                            rb.isKinematic = false;
                            rb.AddForce(ThrowDir.normalized * throwForce, ForceMode2D.Impulse);

                            float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                            transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);


                            state = State.Thrown;
                        }
                        
                        else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                        {

                        }
                        else
                        {

                            Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;

                            //anim.SetBool("Thrown", true);

                            float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                            transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);



                        }




                    }
                }



               
                }
            if (t.phase == TouchPhase.Moved && leftTouch == t.fingerId)
            {
                Vector2 offset = touchPos - startingPoint;
                Vector2 direction = Vector2.ClampMagnitude(offset, 1f);

                Debug.Log(direction);
            }
            else if (t.phase == TouchPhase.Moved)
            {
                TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId == t.fingerId);
                thisTouch.circle.transform.position = getTouchPosition(t.position);

                Vector2 circlePos = thisTouch.circle.transform.position;

                Debug.Log("sry");
                if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                {

                }
                else
                {
                    if (HoldingTouch && state == State.WithPlayer && thisTouch.circle.name == "Touch-1" )
                    {
                        // Vector2 circlePos = thisTouch.circle.transform.position;
                        Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;
                        float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);


                        Debug.Log("Rotate");

                    }

                    else if (HoldingTouch && state == State.WithPlayer && thisTouch.circle.name == "Touch0" )
                    {
                        //Vector2 circlePos = thisTouch.circle.transform.position;
                        Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;
                        float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

                        Debug.Log("Rotate");

                    }
                }
            }
            if (t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                leftTouch = 99;
            }
            else if (t.phase == TouchPhase.Ended)
            {
                TouchLocation thisTouch = touches.Find(TouchLocation => TouchLocation.touchId == t.fingerId);
                Vector2 circlePos = thisTouch.circle.transform.position;

                if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                {

                }
                else
                {
                    if (state == State.WithPlayer && Throw && thisTouch.circle.name == "Touch-1")
                    {
                        Debug.Log("Thrown");

                        anim.SetBool("Thrown", true);

                        Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;

                        rb.isKinematic = false;
                        rb.AddForce(ThrowDir.normalized * throwForce, ForceMode2D.Impulse);

                        float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);


                        state = State.Thrown;
                    }
                    else if (state == State.WithPlayer && Throw && thisTouch.circle.name == "Touch0" )
                    {
                        Debug.Log("Thrown");

                        anim.SetBool("Thrown", true);

                        Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;

                        rb.isKinematic = false;
                        rb.AddForce(ThrowDir.normalized * throwForce, ForceMode2D.Impulse);

                        float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);


                        state = State.Thrown;
                    }

                    if (!Throw && state == State.WithPlayer)
                    {


                        if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                        {

                        }
                        else
                        {
                            Debug.Log(state);

                            anim.SetTrigger("Attack");


                            state = State.Attack;
                            Vector2 ThrowDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;
                            float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
                            transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);


                            Debug.Log("Attack");





                            int ran = Random.Range(0, 2);






                            transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);



                            //anim.SetTrigger("Attack");




                        }















                    }


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
  

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PlayerPos.position, GrabDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(230, 399, 1), new Vector3(278, 399, 2));

        if (IconHandler_script.FindClosestEnemy() != null)
        {
            Gizmos.DrawWireSphere(IconHandler_script.FindClosestEnemy().transform.position, GrabIconRange);

            
        }
        
        //foreach (GameObject GrabIcon in PrefabGrabIcon)
        //{
        //if (GrabIcon != null)
        //{
        //Gizmos.DrawWireSphere(GrabIcon.transform.position, GrabIconRange);
        //}
        //}

    }

    void Ready()
    {

        Instantiate(Explosion, SpearHitparticle.position, Quaternion.identity);

    }






}

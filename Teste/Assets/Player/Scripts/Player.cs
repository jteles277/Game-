using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public GameObject[] Weaponds;

    public bool Throwable;
   



    public bool Teleported;


    public bool Dead;

    public float healthRun;
  
    public float healthPer;
    
   
    
    public Slider healthBar;
    


    [SerializeField] private float vida;

    [SerializeField] private float MaxHealth;






    // Movement


    [Header(" Movement - Atributes")]
        // Atributes   

                public float movementBaseSpeed;
                public float DashLenght;

        [Header("Movement - Statistics")]
        // Statistics 
         
               public Vector2 movementDirection;
               public float movementSpeed;

        [Header("Movement - State")]
        // State

                public bool Running;
                public bool Dashing;


        //References

                public GameObject ShadeRun;
                public GameObject Shade;
                public GameObject Body;
                public Joystick Joystick;
                public LayerMask layerMask;


    //References

   
         [Header("References - RB")]
         //RB

             public Rigidbody2D rb;

             public GameObject DA;
           
         [Header("References - Animator")]
         //Animator

                   public Animator anim;
                   //public Animator anim;
                   public Animator Camanim;

         [Header("References - Spear")]
         //Spears

                    public GameObject my_SpearTest;
                    SpearTest my_SpearTest_script;
                    
                    public GameObject my_Staff;
                    Staff my_Staff_script;

                    public GameObject AxeManager;
                    AxeManager AxeManager_script;

                    public bool Holding1;
                    public bool Holding2;
                    public bool Holding3;
                    public bool HoldingWeapond;
         
    //Particles 

         [Header("Particles - References")]
         //References

                     public GameObject Dust;
                     public GameObject FinalBlood;
                     public GameObject Blood;

         [Header("Particles - Spawners")]
         //Spawners

                   public Transform particle;

         [Header("Particles - Counters")]
         //Counters

                   public float startTimeBtwSpawn;
                   public float timeBtwSpawn;

                   public float timeBtwDash;
                   public float startTimeBtwDash;

                   public float timeBtwDash2;
                   public float startTimeBtwDash2;


    public bool Attacking;



  


    void Start()
    {

        Weaponds = GameObject.FindGameObjectsWithTag("lamina");

       

        Dead = false; 

        healthPer = 1;

        healthRun = vida = MaxHealth;

        anim = GetComponent<Animator>();

        my_SpearTest_script = my_SpearTest.GetComponent<SpearTest>();

        my_Staff_script = my_Staff.GetComponent<Staff>();

        AxeManager_script = AxeManager.GetComponent<AxeManager>();


        
    }

    
    void Update()
    {

       
        ProcessInputs();
        Animate();
        
        //RunParticles();
        CheckIfHolding();

        if (timeBtwDash <= 0)
        {
            
        }
        else
        {
            timeBtwDash -= Time.deltaTime;
        }


        if (timeBtwDash2 <= 0)
        {


            Dashing = false;





                timeBtwDash2 = startTimeBtwDash2;



            
           
            
        }
        else
        {
            if (Dashing == true)
            {
                timeBtwDash2 -= Time.deltaTime;
            }
        }

       





    }
    void FixedUpdate()
    {

        MoveAndRunCheck();



        healthPer = healthRun / MaxHealth;
        healthBar.value = healthPer;
       

        

        if (vida < healthRun)
        {
            healthRun -= 0.01f;
        
        }


        if (vida <= 0)
        {

            if (!Dead)
            {

                Die();

            }


        }



    }


    private bool CanMove(Vector2 dir, float distance)
    {
        return Physics2D.Raycast(transform.position, dir, distance, layerMask).collider == null;
    }


    private bool TryMove(Vector2 baseMoveDir, float distance)
    {

        Vector3 moveDir = baseMoveDir;
        bool canMove = CanMove(moveDir, distance);
        if (!canMove)
        {

            //moveDir = new Vector2(baseMoveDir.x, 0f).normalized;
            //canMove = moveDir.x != 0f && CanMove(moveDir, distance);
            Debug.Log("No Dash");


            if (!canMove)
            {

                // moveDir = new Vector2(0f, baseMoveDir.y).normalized;
                // canMove = moveDir.y != 0f && CanMove(moveDir, distance);
                Debug.Log("No Dash");


            }
        }
        if (canMove)
        {
            //Dash
            return true;
            
        }
        else
        {
            return false;
        }

    }




    public void Dash (Vector2 circlePos)
    {
        

        if(timeBtwDash <= 0)
        { 
            if (HoldingWeapond == false)
            {

                // Debug.Log("Dash");
                // rb.velocity = Vector2.zero;
                // rb.velocity += new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized * 30;

                DA.transform.position = transform.position;

                Vector2 dashDir = new Vector2(circlePos.x - transform.position.x, circlePos.y - transform.position.y).normalized;

                Vector2 Pos = transform.position;

                Dashing = true;

                if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Running"))
                {
                    Instantiate(ShadeRun, Body.transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(Shade, Body.transform.position, Quaternion.identity);
                }

                Camanim.SetTrigger("LitleZoom");

                transform.position = dashDir * DashLenght * Time.deltaTime + Pos;

                //TryMove(dashDir, DashLenght);

                //Debug.Log(circlePos);

               
                timeBtwDash = startTimeBtwDash;
            }
        }
        else
        {
            timeBtwDash -= Time.deltaTime;
        }
    }

    void ProcessInputs()
    {
        movementDirection = new Vector2(Joystick.Horizontal, Joystick.Vertical).normalized;
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }


    void MoveAndRunCheck()
    {

        rb.velocity = movementDirection * movementSpeed * movementBaseSpeed;

        if (Attacking)
        {

            rb.velocity = rb.velocity * 0;

            
        }


        if (this.anim.GetCurrentAnimatorStateInfo(0).IsTag("Running"))
        {
            Running = true;
        }
        else
        {
            Running = false;
        }
    }

    void Animate()
    {
        if ( movementSpeed != 0)
        {
            if(Attacking == false)
            { 

                    anim.SetBool("Running", true);

            }
            else
            {

                anim.SetBool("Running", false);

            }
        }
         
          else
          {

              anim.SetBool("Running", false);

          }
    }

    void RunParticles()
    {
        if (Running == true)
        {
            if (timeBtwSpawn <= 0)
            {


                timeBtwSpawn = startTimeBtwSpawn;

                Instantiate(Dust, particle.position, Quaternion.identity);


            }
            else
            {

                timeBtwSpawn -= Time.deltaTime;
            }
        }
    }

    void CheckIfHolding()
    {

        if (my_SpearTest_script.WithPlayer == true)
        {

            Holding1 = true;

        }
        else
        {
            Holding1 = false;
        }

        if (AxeManager_script.WithPlayer == true)
        {

            Holding3 = true;

        }
        else
        {
            Holding3 = false;
        }


        if (my_Staff_script.WithPlayer == true)
        {

            Holding2 = true;

        }
        else
        {
            Holding2 = false;
        }

        if (Holding1 == true )
        {

            HoldingWeapond = true;

        }
        else if (Holding2 == true)
        {

            HoldingWeapond = true;

        }
        else if (Holding3 == true)
        {

            HoldingWeapond = true;

        }
        else if (Holding1 == false && Holding2 == false && Holding3 == false)
        {

            HoldingWeapond = false;

        }


        

        if (HoldingWeapond)
        {

            anim.SetBool("CarryingWeapond", true);

        }
        else
        {

            anim.SetBool("CarryingWeapond", false);

        }
        






        if (AxeManager_script.Attacking == true)
        {

            Attacking = true;

        }
        else
        {

            Attacking = false;

        }





















    }

    public void Hit(float Damage)
    {
              
        vida -= Damage;
        //Debug.Log(vida);

        Instantiate(Blood, particle.position, Quaternion.identity);
        Camanim.SetTrigger("LitleShake");

    }

    public void Die()
    {
        Dead = true;

        Instantiate(FinalBlood, particle.position, Quaternion.identity);

        Debug.Log("Is Dead");
        FindObjectOfType<GameManager>().EndGame();
        Camanim.SetTrigger("Shake");

    }
}

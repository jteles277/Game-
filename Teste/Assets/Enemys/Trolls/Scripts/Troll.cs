using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonoBehaviour
{

    public Freezer _freezer;

    public Transform SpawnPointPos;


   public float FollowSpeed;


    //Particles 

    [Header("Particles - References")]
    //References

    public GameObject HitParticle;
    public GameObject HitParticle2;
    public GameObject HitParticle2W;
    public GameObject Explosion;


    [Header("Particles - Spawners")]
    //Spawners

    public Transform Hitparticle;





    //Animation 

    //Reference
    
   public Animator Spearanim;
    private Animator anim;
    public Rigidbody2D rb;

    //Health System

    [SerializeField] private HealthBar HealthBar;
    [SerializeField] private float helthPer;
    [SerializeField] private float helthPerdelay;
    [SerializeField] private float health;
    [SerializeField] private float healthRun;
    [SerializeField] private float healthRundelay;
    [SerializeField] private float Maxhealth;



    //SlowDownTime

        public TimeManager TimeManager;

    //Interact with player


    private bool followPlayer;
    public float followRange;
    public float AttackRange;

    public float playerDifference;

    public Transform PlayerPos;
    public Player PlayerScript;


    public bool Sawn;

    Vector2 targetPosition;
    public float PatrolSpeed;

   
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;


    public bool InAttackRange;

    //Handle 

    [SerializeField] private GoblinSpear GoblinSpear;


   public bool Holding;





    public Animator Camanim;





    // Start is called before the first frame update
    void Start()
    {
        healthRundelay = healthRun = health = Maxhealth ;

        targetPosition = transform.position;

        ChangeDirection();


        anim = GetComponent<Animator>();

        Sawn = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (PlayerScript.Teleported == true)
        {

            Sawn = false;

        }

        


        HealthBar.SetSizedelayed(helthPerdelay);
        HealthBar.SetSize(helthPer);
        helthPer = healthRun / Maxhealth;
        helthPerdelay = healthRundelay / Maxhealth;

        if (health < healthRun)
        {                                                  //Heath Managment
            healthRun -= 10f;
        }
        if (health < healthRundelay)
        {
            healthRundelay -= 3f;
        }

        if(PlayerPos != null)
        { 


                if (Vector2.Distance(transform.position, PlayerPos.position) < followRange)
                {

                    if (Vector2.Distance(transform.position, PlayerPos.position) < AttackRange )
                    {
                        //Attack
                         anim.SetBool("Walking", false);
                        rb.velocity = Vector2.zero;

                        InAttackRange = true;

                        //Sawn = false;

                

                    }
                     else
                     {

                        //GoblinSpear_script.Rotate();
                       if (Spearanim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                       {

                       }
                        else
                        {



                                if (GoblinSpear != null)
                                {
                                    Holding = true;
                                    GoblinSpear.Rotate();
                                }
                                else
                                {
                                    Holding = false;
                                }

                                InAttackRange = false;

                                Sawn = true;
                                anim.SetBool("Walking", true);
                                rb.velocity = new Vector2(PlayerPos.position.x - transform.position.x, PlayerPos.position.y - transform.position.y).normalized * FollowSpeed; //* Time.deltaTime;



                        }

                    
                     }

                }


       
         else if (Sawn == true)
         {
             GoblinSpear.Rotate();
                anim.SetBool("Walking", true);
             rb.velocity = new Vector2(PlayerPos.position.x - transform.position.x, PlayerPos.position.y - transform.position.y).normalized * FollowSpeed; //* Time.deltaTime;
            }
          else
          {
            //Patrol
            rb.velocity = Vector2.zero;
            if ((Vector2)transform.position != targetPosition)
            {

                    anim.SetBool("Walking", true);

                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, PatrolSpeed * Time.deltaTime);

            }
            else
            {

                    //Invoke("ChangeDirection", Random.Range(0.5f, 1));
                    anim.SetBool("Walking", false);
                    Invoke("ChangeDirection", 0.3f);
            }


            InAttackRange = false;

          }

        }









    }

    public void ChangeDirection()
    {
        if (SpawnPointPos != null)
        { 
            targetPosition = GetRandomPosition();
        }
    }
    public void Stop()
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("Walking", false);
    }
 
    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);




            //if (SpawnPointPos != null)
        return new Vector2(SpawnPointPos.position.x + randomX, SpawnPointPos.position.y + randomY);


    }


    public void Hit(float Damage, GameObject Particle)
    {
        //Camanim.SetTrigger("SmallerShake");
        health -= Damage;

        //Sawn = true;

        anim.SetTrigger("hited");

        Instantiate(Particle, Hitparticle.position, Quaternion.identity);

        //Instantiate(HitParticle, Hitparticle.position, Quaternion.identity);
        //Instantiate(HitParticle2, Hitparticle.position, Quaternion.identity);

        if (health <= 0)
        {
            Die();
        }

                //TimeManager.SlowDown();

          //      _freezer.Freeze() ;
        
    }
    public void Die()
    {

        Instantiate(HitParticle2W, Hitparticle.position, Quaternion.identity);
        Instantiate(Explosion, Hitparticle.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followRange);
        Gizmos.DrawWireSphere(transform.position, AttackRange);


        if(SpawnPointPos != null)
        { 


            Gizmos.DrawWireSphere(SpawnPointPos.position, maxX);
            Gizmos.DrawWireSphere(SpawnPointPos.position, maxY);


        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingAssistant : MonoBehaviour
{

    [SerializeField] private Transform PlayerPos;
    public Rigidbody2D rb;

    //Particles 

    [Header("Particles - References")]
            //References

                    public GameObject Dust;

            //Counters

                   public float startTimeBtwSpawn;
                   public float timeBtwSpawn;

    //Handle 
    public GameObject Player;
    Player Player_script;

    // Start is called before the first frame update
    void Start()
    {
        Player_script = Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_script.Dashing == true)
        { 
            if (timeBtwSpawn <= 0)
            {


                

                Instantiate(Dust, transform.position, Quaternion.identity);
                timeBtwSpawn = startTimeBtwSpawn;

            }
             else
             {

                 timeBtwSpawn -= Time.deltaTime;
             }
        }


        if(PlayerPos != null)
        { 
             rb.velocity = new Vector2(PlayerPos.position.x - transform.position.x, PlayerPos.position.y - transform.position.y) * 100 * Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teste : MonoBehaviour
{
    [SerializeField] private float Health;

    [SerializeField] private float MaxHealth;


    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Hit(float Damage)
    {


        Health -= Damage;
        Debug.Log(Health);





        


    }
}

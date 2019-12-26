using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeStuff : MonoBehaviour
{
    public GameObject Axe;
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Axe.transform.position.x - transform.position.x, Axe.transform.position.y - transform.position.y) * 500 * Time.deltaTime;
    }
}

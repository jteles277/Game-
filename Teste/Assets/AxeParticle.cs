using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeParticle : MonoBehaviour
{


    public Transform Weapond;

    public float offset;


    // Start is called before the first frame update
    void Start()
    {
        Vector2 ThrowDir = new Vector2(transform.position.x - Weapond.position.x, transform.position.y - Weapond.position.y).normalized;




        float angle = Mathf.Atan2(ThrowDir.y, ThrowDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + offset);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

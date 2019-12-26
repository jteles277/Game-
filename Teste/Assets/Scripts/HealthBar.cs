using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    private Transform bardelay1;
    private Transform bar1;


    // Start is called before the first frame update
    void Start()
    {
        





        bar1 = transform.Find("Bar");
        bardelay1 = transform.Find("BarDelay");
    }


    public void SetSize(float sizeNormalized)
    {


        bar1.localScale = new Vector3(sizeNormalized, 1f);


    }
    public void SetSizedelayed(float sizeNormalized)
    {


        bardelay1.localScale = new Vector3(sizeNormalized, 1f);


    }
}

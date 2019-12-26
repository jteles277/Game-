using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconHandler : MonoBehaviour
{
    public float GrabIconRange;
    public GameObject Player;
    public bool Ocupado;

    public GameObject FindClosestEnemy()
    {

        

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Icon");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = Player.transform.position;

        Ocupado = false;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && Ocupado == false)
            {
                closest = go;
                distance = curDistance;

                Ocupado = false;
            }
            else
            {
                Ocupado = true;
            }
        }
        return closest;
       

    }
    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;

        

        if (FindClosestEnemy() != null)
        {
            Gizmos.DrawWireSphere(FindClosestEnemy().transform.position, GrabIconRange);
        }

       

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerOrdener : MonoBehaviour
{

    [SerializeField]
    private int sortingOrderBase = 5000;

    [SerializeField]
    private int offset = 0;

    private Renderer myRenderer;

    public Transform FeetPos;

    private void Awake()
    {

        myRenderer = gameObject.GetComponent<Renderer>();

    }
    private void LateUpdate()
    {

        myRenderer.sortingOrder = (int)(sortingOrderBase - (100 * FeetPos.position.y - offset));

    }

    


    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, (float)offset / 100);
       

    }




}







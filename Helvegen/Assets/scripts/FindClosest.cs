using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour
{
   
    Controller controller;
    UnitController uController;
    
    Transform w_pos;
   Transform ew_pos;
    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> warriors= new List<GameObject>();
    public GameObject closestEnemy;

    private void Start()
    {
        
    }

    public GameObject FindEnemy()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.forward, Color.white);
        if (Physics.Raycast(transform.position, Vector3.forward, out hit))
        {
            Debug.Log(hit.collider.gameObject.name + " identificado !!!!!!!!!!!!!!!");
        }

        return hit.collider.gameObject;
    }
}

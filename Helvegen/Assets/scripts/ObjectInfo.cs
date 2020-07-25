using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectInfo : MonoBehaviour
{

    public bool isSelected = false;

    public string objectname;

   private InputManager manager;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        objectname = gameObject.name;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && isSelected)
        {
            MoveUnit();
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
    }

    public void MoveUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.tag == "ground")
            {
                agent.destination = hit.point;
            
            }
        }
    }



}

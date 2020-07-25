using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class UnitController : MonoBehaviour
{

    public bool startBattle = false;

    List<GameObject> warriors = new List<GameObject>();
    bool isDragging=false;
    GetSelected get_selected;
    private NavMeshAgent agent;

    public List<Transform> selectedUnits = new List<Transform>();
   
    Vector3 iniMousePosition;

    
    RaycastHit hit;

    private void Start()
    {
        get_selected = GetComponent<GetSelected>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnGUI()
    {
        if (isDragging)
        {
            var rect = ScreenHelper.GetScreenRect(iniMousePosition, Input.mousePosition);
            ScreenHelper.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.8f, 0.1f));
            ScreenHelper.DrawScreenRectBorder(rect, 1, Color.green);
        }
        

    }

    private void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            iniMousePosition = Input.mousePosition;
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(camRay, out hit))
            {
                if (hit.transform.CompareTag("selectable"))
                {

                    hit.collider.gameObject.GetComponent<GetSelected>().selected = true;
                    warriors.Add(hit.collider.gameObject);

                    Select(hit.transform, Input.GetKey(KeyCode.LeftShift));
                 
                }
                else if (hit.transform.CompareTag("ground") && !isDragging)
                {
                   Deselect();
                }
                             
            }
            if (Input.GetMouseButton(0))
            {
                
                    isDragging = true;
                
            }
        }

       if (Input.GetMouseButtonUp(0))
        {
            

            foreach (var selectables in FindObjectsOfType<CapsuleCollider>())
            {
                if (Selecionados(selectables.transform))
                {
                    selectables.GetComponent<GetSelected>().selected = true;
                    Select(selectables.transform, true);
                }
            }
            isDragging = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            MoveUnits();
        }

       
    }

    private void Select(Transform unit, bool multiSelect = false)
    {
        if (!multiSelect)
        {
            Deselect();
            selectedUnits.Add(unit);
          
        }
        selectedUnits.Add(unit);
        unit.Find("Highlight").gameObject.SetActive(true);
        
    }

    private void Deselect()
    {
        for(int i=0;i<selectedUnits.Count; i++)
        {
            selectedUnits[i].Find("Highlight").gameObject.SetActive(false);

            selectedUnits[i].GetComponent<GetSelected>().selected = false;


        }
        selectedUnits.Clear();
    }

    private bool Selecionados(Transform transform)
    {
        if (!isDragging)
        {
            return false;
        }
        else
        {
            var camera = Camera.main;
            var viewport = ScreenHelper.GetViewportBounds(camera, iniMousePosition, Input.mousePosition);
            return viewport.Contains(camera.WorldToViewportPoint(transform.position));


        }

    }

    private void MoveUnits()
    {
      
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerWarrior : MonoBehaviour
{
    
    GetSelected slc;
    private float speed;
    public bool charge = false;
    public GameObject close;
    private bool alive=true;
    public float health = 100;
    FindClosest closest;
    public bool selected;
    Vector3 clickPosition;
    NavMeshAgent agent;
    public GameObject enemy;
   public GameObject marker;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       
        //close = closest.closestEnemy;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        slc = GetComponent<GetSelected>();
      
    }

    // Update is called once per frame
    void Update()
    {
        selected = slc.selected;

      
        if (health <= 0)
        {

            alive = false;
            selected = false;
            
            anim.SetBool("Dead", true);

            new WaitForSeconds(2f);

            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<CapsuleCollider>());


        }

        // transform.LookAt(close.transform);

        if (Input.GetMouseButtonDown(1) && selected)
        {
            
            anim.SetBool("Attack", false);
            anim.SetBool("wWalking", true);
            MoveUnit();
           
        }

        if (alive)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.forward * 100, Color.white);
            if (Physics.Raycast(transform.position, Vector3.forward * 100, out hit))
            {
                Debug.Log(hit.collider.gameObject.name + " identificado !!!!!!!!!!!!!!!");
                if (hit.collider.gameObject.tag == "enemyWarrior")
                {
                    Alive(hit.collider.gameObject) ;
                }

            }
            
        }

    }

    public void MoveUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "ground")
            {

                transform.LookAt(hit.point);
                agent.destination = hit.point;
                               
                Instantiate(marker, hit.point, Quaternion.identity);
                

            }
           
        }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemyWarrior")
        {
            anim.SetBool("Attack", true);
            speed = 0f;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "enemyWarrior" )
        {
            Debug.Log("Oh u gas");
            transform.LookAt(collision.gameObject.transform);
            anim.SetBool("Attack", true);
            anim.SetBool("wWalking", false);
            var rand = Random.Range(1, 5);
            speed = 0f;
            health -= rand * Time.deltaTime ;
            Debug.Log("Carinha health: " + health);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "enemyWarrior" )
        {
            transform.LookAt(collision.gameObject.transform);
            anim.SetBool("Attack", false);
            Debug.Log("hit");
           

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Marker")
        {
            Debug.Log("trigger activated");

            anim.SetBool("wWalking", false);
            new WaitForSeconds(0.2f);
            Destroy(GameObject.FindWithTag("Marker"));
        }
    }

    private void Alive(GameObject enemy)
    {
        if (charge)
        {
            if (Vector3.Distance(gameObject.transform.position, enemy.transform.position) <= 0.5f)
            {

                transform.LookAt(enemy.transform);
                speed = 0;
                anim.SetBool("wWalking", false);

            }
            else
            {
                anim.SetBool("Attack", false);
                anim.SetBool("wWalking", true);
                transform.LookAt(enemy.transform);
                transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
                speed = 5f;

            }
        }
    }
  
}

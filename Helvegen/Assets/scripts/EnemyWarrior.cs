using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarrior : MonoBehaviour
{

    public bool charge = false;
    private bool alive = true;
    public float health=100;
  
    private float dist;
    public float speed = 5f;
    
    public GameObject warrior;


    Animator anim;

    private void Start()
    {
    
        anim = GetComponent<Animator>();
    }
    void Update()
    {
               if (health <= 0)
        {
            alive = false;
            speed = 0f;
            anim.SetBool("Dead", true);

            new WaitForSeconds(2f);

            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<SphereCollider>());
            Destroy(gameObject.GetComponent<BoxCollider>());
            new WaitForSeconds(6f);
          

        }

            if (alive) {
            RaycastHit hit;
            Debug.DrawRay(transform.position, -Vector3.forward * 100, Color.red);
            if (Physics.Raycast(transform.position, -Vector3.forward * 100, out hit))
            {
                Debug.Log(hit.collider.gameObject.name + " identificado !!!!!!!!!!!!!!!");
                if (hit.collider.gameObject.tag == "selectable")
                {
                    Debug.Log(hit.collider.gameObject.name + " ESTE LOG AQUI");
                    Alive(hit.collider.gameObject);
                }
            }
          
        }
        

    }

       private void OnCollisionEnter(Collision collision)
    {
        speed = 0f;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "selectable")
        {
        
            transform.LookAt(collision.gameObject.transform);
            anim.SetBool("Attack", true);
            var rand = Random.Range(1, 5);
            health -= rand * Time.deltaTime;
            speed = 0f;
            anim.SetBool("eWalking", false);
            Debug.Log("Enemy health: " + health);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "selectable")
        {
           
            transform.LookAt(collision.gameObject.transform);
            anim.SetBool("eWalking", false);
            anim.SetBool("Attack", false);
            


        }
    }

    private void Alive(GameObject warrior)
    {
        if (charge)
        {
            Debug.Log("TEMOS CHARGE E SEI LA O QUE");
            if (Vector3.Distance(gameObject.transform.position, warrior.transform.position) <= 1f)
            {

                Debug.Log("Perto");

                transform.LookAt(warrior.transform);
                speed = 0f;
                anim.SetBool("eWalking", false);

            }
            else
            {

                Debug.Log("Distante");
                anim.SetBool("Attack", false);
                anim.SetBool("eWalking", true);
                transform.LookAt(warrior.transform);
                transform.position = Vector3.MoveTowards(transform.position, warrior.transform.position, speed * Time.deltaTime);
                speed = 5f;

            }
        }
    }
   


}

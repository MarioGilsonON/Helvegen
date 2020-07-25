using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Controller : MonoBehaviour
{
    private int warriorCounter;
    public GameObject warrior;
    public GameObject enemy;
 
    List<GameObject> warriors = new List<GameObject>();
    public int vRand;

    Vector3 vec;

   public List<GameObject> enemies = new List<GameObject>();
    private void Start()
    {
        enemy = GetComponent<GameObject>();
    }
    private void Awake()
    {
        vRand = Random.Range(20, 50);


        for (var i = 0; i < vRand; i++)
        {
            vec.x++;
           
           enemies.Add(enemySpawn());
           

        }
           
        
    }

    private void Update()
    {
        SpawnArmy();
    }



    //Menu -----------------------------------------------


    //Battle ---------------------------------------------




    public GameObject enemySpawn()
    {
      var newPos = enemy.transform.position + vec;



           GameObject newEnemy = Instantiate(enemy, newPos, enemy.transform.rotation);
        

        return newEnemy;

    }


    public void SpawnArmy()
    {
        
        RaycastHit hit;
        if (GameObject.FindGameObjectWithTag("StartBattle") != null) {

            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(camRay, out hit))
            {         

                if (Input.GetMouseButtonDown(0))
                {
                    if(warriorCounter < enemies.Count)
                    {
                        GameObject newWarrior = Instantiate(warrior, hit.point, Quaternion.identity);
                        warriors.Add(newWarrior);
                        warriorCounter++;
                    }
                    else { Debug.Log("Limite de warriors esgotado"); }

                }
                
            }
        } else
        {
            charge();
        
        }
        
    
    }

    void StartBattle()
    {

        GameObject.FindGameObjectWithTag("StartBattle");
    }

   
    void charge()
    {
        foreach (var enemyw in enemies)
        {

            Debug.Log(" O inimigo " + enemyw.name + " está em movimento");
            enemyw.GetComponent<EnemyWarrior>().charge = true;


        }
        foreach (var warrior in warriors)
        {

            warrior.GetComponent<PlayerWarrior>().charge = true;

        }
    }
}

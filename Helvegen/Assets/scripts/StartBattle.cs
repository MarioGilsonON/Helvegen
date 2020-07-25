using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    private UnitController uc;
    public  GameObject obj;

    public bool enemy_charge = false;

    void Start()
    {
        uc = GetComponent<UnitController>();
    }
    public void Charge()
      {
        
        uc.startBattle = true;
       Destroy();
    }

    void Destroy()
    {
        GameObject.Destroy(obj);
    }
}

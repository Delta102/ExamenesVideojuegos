using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject zombie;
    float tiempo = 0;
    void Start()
    {
        
    }

    void Update()
    {
        tiempo += Time.deltaTime;

        if(tiempo >= 3)
            Generacion();
        
    }

    private void Generacion(){
        var portalPosition=transform.position+new Vector3(3, 0,0);
        var gb = Instantiate(zombie, portalPosition, Quaternion.identity) as GameObject;
        var controller= gb.GetComponent<ZombieController>();
        tiempo = 0;
    }
}

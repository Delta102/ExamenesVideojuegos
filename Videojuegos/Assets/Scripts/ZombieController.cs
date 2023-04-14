using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    Rigidbody2D rb;
    
    bool estado = true;

    const int velocidad = 5;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(estado) Caminar();
    }

    private void Caminar()
    {
        rb.velocity = new Vector2(-velocidad, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.gameObject.tag == "Player") estado = false;
    }
}

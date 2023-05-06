using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    GameManagerController gameManager;

    Rigidbody2D rb;
    
    bool estado = true;

    const int velocidad = 5;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManagerController>();
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

        if(other.gameObject.tag == "Player"){
            gameManager.PerderVida();
            if(gameManager.Vida() == 0) 
                rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
    }
}

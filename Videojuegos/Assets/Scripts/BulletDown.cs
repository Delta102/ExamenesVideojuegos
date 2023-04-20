using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDown : MonoBehaviour
{
    int velocidadX = 11;
    int velocidadY = 10;
    float realVelocity;
    Rigidbody2D rb;
    //float realVelocity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocidadX, -velocidadY);
    }

    public void SetRightDirection(){
        realVelocity=velocidadX;
    }

    public void SetLeftDirection(){
        realVelocity=-velocidadX;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
        if (other.gameObject.tag == "Zombie") Destroy(other.gameObject);
    }
}

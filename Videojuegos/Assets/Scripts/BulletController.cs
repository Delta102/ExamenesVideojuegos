using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject bulletUp;
    public GameObject bulletDown;
    float velocity=15;
    float realVelocity;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.velocity=new Vector2(realVelocity,0);

        if(Input.GetKeyUp(KeyCode.X))
            Direccion();
    }

    private void Direccion(){
        if(rb.velocity.x > 0){
            var bulletUpPosition=transform.position+new Vector3(3, 0,0);
            var gb = Instantiate(bulletUp, bulletUpPosition, Quaternion.identity) as GameObject;
            var controller= gb.GetComponent<BulletUp>();
            controller.SetRightDirection();

            var bulletDownPosition=transform.position+new Vector3(3, 0,0);
            var gb2 = Instantiate(bulletDown, bulletDownPosition, Quaternion.identity) as GameObject;
            var controller2= gb2.GetComponent<BulletDown>();
            controller2.SetRightDirection();
        }
        if(rb.velocity.x < 0){
            Debug.Log("Izquierda");
            var bulletUpPosition=transform.position+new Vector3(-3, 0,0);
            var gb = Instantiate(bulletUp, bulletUpPosition, Quaternion.identity) as GameObject;
            var controller= gb.GetComponent<BulletUp>();
            controller.SetLeftDirection();

            var bulletDownPosition=transform.position+new Vector3(-3, 0,0);
            var gb2 = Instantiate(bulletDown, bulletDownPosition, Quaternion.identity) as GameObject;
            var controller2= gb2.GetComponent<BulletDown>();
            controller2.SetLeftDirection();
        }
        
    }

    public void SetRightDirection(){
        realVelocity=velocity;
    }

    public void SetLeftDirection(){
        realVelocity=-velocity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
        if (other.gameObject.tag == "Zombie") Destroy(other.gameObject);
    }
}

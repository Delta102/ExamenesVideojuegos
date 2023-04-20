using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public GameObject bullet;

    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator animator;

    //VARIABLES INTERCAMBIABLES
    bool vivo = true;
    bool isGrounded;
    //bool saltado = false;
    bool isTouchingWall;
    int velocidad = 10;
    float jumpForce = 15;
    int saltos = 0;
    //VARIBLES EST�TICOS
    const int estadoQuieto = 0;
    const int estadoCaminar = 1;
    const int estadoAtaque = 2;
    const int estadoThrow = 3;
    const int estadoSlide = 4;
    const int estadoDead = 5;
    const int estadoSaltoUp = 6;
    const int estadoSaltoFall = 7;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vivo) {
            Renderizado();
            Caminar();
            Salto();
            Ataque();
            Throw();
            Slide();
            Muerte();
        }
    }

    private void Caminar()
    {
        if (rb.velocity != Vector2.zero)
            CambiarAnimacion(estadoCaminar);

        if (Input.GetKey(KeyCode.RightArrow))
            rb.velocity = new Vector2(velocidad, rb.velocity.y);

        else if (Input.GetKey(KeyCode.LeftArrow))
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            CambiarAnimacion(estadoQuieto);
        }

    }
    
    private void disparo(int a){
        if(a>0){
            var bulletPosition=transform.position+new Vector3(a, 0,0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller= gb.GetComponent<BulletController>();
            controller.SetRightDirection();
        }
        if(a<0){
            var bulletPosition=transform.position+new Vector3(a, 0,0);
            var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
            var controller= gb.GetComponent<BulletController>();
            controller.SetLeftDirection();
        }
    }

    private void Salto() {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;

            if (rb.velocity.y > 0)
                CambiarAnimacion(estadoSaltoUp);

            saltos = 0;
        }
        else if (!isGrounded && isTouchingWall && Input.GetKeyDown(KeyCode.Space) && saltos < 5)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            CambiarAnimacion(estadoSaltoUp);

            saltos++;
        }

        if (rb.velocity.y < 0 && !isGrounded)
            CambiarAnimacion(estadoSaltoFall);
                
    }


    private void Ataque()
    {
        if (Input.GetKey(KeyCode.Z))
            CambiarAnimacion(estadoAtaque);
    }

    private void Muerte()
    {
        if (Input.GetKey(KeyCode.L)) {
            vivo = false;
            CambiarAnimacion(estadoDead);
        }
    }

    private void Throw()
    {
        if (Input.GetKey(KeyCode.X))
            CambiarAnimacion(estadoThrow);
    }

    private void Slide()
    {
        if (Input.GetKey(KeyCode.C))
            CambiarAnimacion(estadoSlide);
    }

    private void CambiarAnimacion(int estado)
    {
        animator.SetInteger("Estado", estado);
    }

    private void Renderizado()
    {
        if (rb.velocity.x < 0){
            sr.flipX = true;
            if(Input.GetKeyUp(KeyCode.F)) disparo(-3);
        }
            

        if (rb.velocity.x >= 0){
            sr.flipX = false;
            if(Input.GetKeyUp(KeyCode.F)) disparo(3);
        }
            
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground") isGrounded = true;
        
        if(other.gameObject.tag == "Pared") isTouchingWall = true;
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Sobre") velocidad += 5;
        if(other.gameObject.tag == "Zombie") {
            vivo = false;
            CambiarAnimacion(estadoDead);
            Debug.Log("Estás muerto");
        }
    }
}

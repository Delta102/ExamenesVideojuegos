using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerMovementButtons : MonoBehaviour
{
    // COMPONENTES DE PLAYER
    public TMP_Text llaveTxt;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    Vector2 direccion;
    GameManagerController gameManager;
    
    public GameObject bullet;

    // VARIABLES INTERCAMBIABLES
    float velocity = 10f;
    int jumpForce = 10;
    bool isGrounded;
    bool llaveActivada = false;
    bool enPortal = false;

    //VARIABLES ESTÁTICAS
    const int estadoQuieto = 0;
    const int estadoCaminar = 1;
    const int estadoSaltoUp = 6;
    const int estadoSaltoFall = 7;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManagerController>();
    }

    void Update()
    {
        CambiarTexto();
    }

    void FixedUpdate() {
        Renderizado();
        
        transform.Translate(direccion * velocity * Time.deltaTime);
        
        if (rb.velocity.y < 0)
            ChangeAnimation(estadoSaltoFall);
    }

    void Renderizado(){
        if (gameManager.Vida()>0){
            if (rb.velocity.x < 0)
                sr.flipX = true;
            if (rb.velocity.x > 0)
                sr.flipX = false;
        }
    }

    // WALKING
    public void LeftWalk(){
        if (gameManager.Vida()>0){
            direccion = Vector2.left;
            sr.flipX = true;
            ChangeAnimation(estadoCaminar);
        }
    }

    public void RightWalk(){
        if (gameManager.Vida()>0){
            direccion = Vector2.right;
            sr.flipX = false;
            ChangeAnimation(estadoCaminar);
        }
    }

    public void StopWalk(){
        if (gameManager.Vida()>0){
            direccion = Vector2.zero;
            ChangeAnimation(estadoQuieto);
        }
    }

    public void Jump(){
        if (gameManager.Vida()>0){
            if(isGrounded){
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
            if (rb.velocity.y > 0)
                ChangeAnimation(estadoSaltoUp);
        }
    }

    public void Shoot(){
        if (gameManager.Vida()>0){
            if(rb.velocity.x >= 0){
                var bulletPosition=transform.position+new Vector3(3, 0,0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller= gb.GetComponent<BulletController>();
                controller.SetRightDirection();
                
            }
            if(rb.velocity.x < 0){
                var bulletPosition=transform.position+new Vector3(-3, 0,0);
                var gb = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var controller= gb.GetComponent<BulletController>();
                controller.SetLeftDirection();
            }
        }
    }

    public void ActivarPortal(){
        if(llaveActivada)
            Debug.Log("LLave Activada");
        else
            Debug.Log("Llave No Activada");

        if(enPortal)
            Debug.Log("Estoy en el Portal");
        else
            Debug.Log("No estoy en el Portal");



        if(gameManager.CantidadZombies() >= 3 && llaveActivada && enPortal){
            SceneManager.LoadScene("ASDF");
        }
        else 
            Debug.Log("No puede Ingresar al Portal");
    }

    void ChangeAnimation(int estado)
    {
        animator.SetInteger("Estado", estado);
    }

    void CambiarTexto(){
        if(llaveActivada)
            llaveTxt.text = "Llave activada";
        else
            llaveTxt.text = "Llave desactivada";
    }

    // MÉTODOS PROPIOS DE UNITY
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground") isGrounded = true;
        if(other.gameObject.tag == "Recolector"){
            llaveActivada = true;
            gameManager.Activacion();
            Destroy(other.gameObject);
        } 
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "PortalCambio")
            enPortal = true;
        else
            enPortal = false;
    }
}
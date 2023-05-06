using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerController : MonoBehaviour
{
    public TMP_Text vidaTxt;
    public TMP_Text zombiesTxt;
    int vida;
    int cantidadBalas;
    int cantidadZombies;
    bool activacion;
    public int vidaZombie;
    void Start()
    {
        vida = 2;
        vidaZombie = 2;
        //cantidadBalas = 10000000;
        cantidadZombies = 0;
        CambiarTexto();
    }

    // public int CantidadBalas(){
    //     return cantidadBalas;
    // }

    public int Vida(){
        return vida;
    }

    public int CantidadZombies(){
        return cantidadZombies;
    }
    
    public bool Activacion(){
        return activacion;
    }
    public int VidaZombie(){
        return vidaZombie;
    }

    public void PerderVidaZombie(){
        vidaZombie--;
    }
    
    public void PerderVida(){
        vida --;
        CambiarTexto();
    }

    public void Llave(){
        activacion = true;
        CambiarTexto();
    }

    // public void PerderBalas(){
    //     cantidadBalas--;
    //     CambiarTexto();
    // }

    public void GanarBalas(){
        cantidadBalas += 5;
        CambiarTexto();
    }

    public void ConteoZombies(){
        cantidadZombies++;
        CambiarTexto();
    }

    private void CambiarTexto(){
        //balasTxt.text = "Balas: " + cantidadBalas;
        vidaTxt.text = "Vidas: " + vida;
        zombiesTxt.text = "Puntos: " + cantidadZombies;
    }
}

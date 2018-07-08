using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PantallaAterrizaje : MonoBehaviour {

    // Use this for initialization
    //FALTA HACER LA PANTALLA DE ATERRIZAJE

    public GameObject pantallaAterrizaje;
    public GameObject aterrizajeConseguido;
    public GameObject aterrizajeFallido;
    public Text textPuntaje;
    
	void Start () {
        pantallaAterrizaje.SetActive(false);
        aterrizajeConseguido.SetActive(false);
        aterrizajeFallido.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (UI_Controller.Get().buenAterrizaje == true)
        {
            aterrizajeConseguido.SetActive(true);
            aterrizajeFallido.SetActive(false);
        }
        if (UI_Controller.Get().buenAterrizaje == false)
        {
            aterrizajeFallido.SetActive(true);
            aterrizajeConseguido.SetActive(false);
        }
        if (GameManager.Get().tipoTextoPuntaje == 0)
        {
            textPuntaje.text = "          ";
        }
        if (GameManager.Get().tipoTextoPuntaje == 1)//puntaje x2
        {
            textPuntaje.text = "60 Puntos";
        }
        if (GameManager.Get().tipoTextoPuntaje == 2)//puntaje x3
        {
            textPuntaje.text = "90 Puntos";
        }
        if (GameManager.Get().tipoTextoPuntaje == 3)//puntaje x4
        {
            textPuntaje.text = "180 Puntos";
        }
        if (GameManager.Get().tipoTextoPuntaje == 4)//puntaje x5
        {
            textPuntaje.text = "250 Puntos";
        }
        
   
	}
}

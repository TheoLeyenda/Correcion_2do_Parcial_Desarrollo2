using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Controller : MonoBehaviour {

    // Use this for initialization
    private static UI_Controller instance = null;

    [HideInInspector]
    public int puntos;
    [HideInInspector]
    public float segundos;
    [HideInInspector]
    public float minutos;
    [HideInInspector]
    public float gasolinaJugador;
    [HideInInspector]
    public float altitudJugador;
    [HideInInspector]
    public float velocidadHorizontalJugador;
    [HideInInspector]
    public float velocidadVerticalJugador;
    [HideInInspector]
    public bool buenAterrizaje;

    public Text TextoTiempo;
    public Text TextoVelocidadVerticalJugador;
    public Text TextoVelocidadHorizontalJugador;
    public Text TextoGasolinaJugador;
    public Text textoAltitudJugador;
    public Text TextoPuntos;
    void Start () {
		
	}
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
        ContarAltitud();
        ContarGasolina();
        ContarVelocidadHorizontal();
        ContarVelocidadVertical();
        ContarPuntaje();
    }
    public void ContarTiempo()
    {
        segundos = segundos + Time.deltaTime;
        if (segundos >= 60)
        {
            segundos = 0;
            minutos++;
        }
        if (segundos >= 10)
        {
            TextoTiempo.text = "Tiempo  " + (int)minutos + " :" + (int)segundos;
        }
        if (segundos < 10)
        {
            TextoTiempo.text = "Tiempo  " + (int)minutos + " :0" + (int)segundos;
        }
    }
    public static UI_Controller Get()
    {
        return instance;
    }
    private void ContarAltitud()
    {
        textoAltitudJugador.text = "Altitud: " + altitudJugador;
    }
    private void ContarGasolina()
    {
        TextoGasolinaJugador.text = "Gasolina: " + (int)gasolinaJugador;
    }
    private void ContarVelocidadHorizontal()
    {
        if (velocidadHorizontalJugador > 0)
        {
            TextoVelocidadHorizontalJugador.text = "Velocidad Horizontal: " + (int)velocidadHorizontalJugador + " -->";
        }
        if (velocidadHorizontalJugador == 0)
        {
            TextoVelocidadHorizontalJugador.text = "Velocidad Horizontal: " + (int)velocidadHorizontalJugador;
        }
        if (velocidadHorizontalJugador < 0)
        {
            TextoVelocidadHorizontalJugador.text = "Velocidad Horizontal: " + (int)velocidadHorizontalJugador * -1 + " <--";
        }
    }
    private void ContarVelocidadVertical()
    {
        if (velocidadVerticalJugador >= 0)
        {
            TextoVelocidadVerticalJugador.text = "Velocidad Vertical: " + (int)velocidadVerticalJugador;
        }
        if (velocidadVerticalJugador < 0)
        {
            TextoVelocidadVerticalJugador.text = "Velocidad Vertical: " + (int)velocidadVerticalJugador;
        }
    }
    private void ContarPuntaje()
    {
        TextoPuntos.text = "Puntos : " + puntos;
    }
}

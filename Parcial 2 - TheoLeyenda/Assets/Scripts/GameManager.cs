using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // Use this for initialization
    private static GameManager instance = null;
    
    [HideInInspector]
    public int tipoTextoPuntaje;
    [HideInInspector]
    public int nivelActual;
    [HideInInspector]
    public bool asignarNivel;
    [HideInInspector]
    public int cantNiveles;
    [HideInInspector]
    public bool HabilitarMovimiento;
    [HideInInspector]
    public bool JuegoTerminado;
    [HideInInspector]
    public bool EnCreditos;
    [HideInInspector]
    public bool JuegoEmpezado;


    public Camera camara;
    public GameObject jugador;
    public GameObject alturaCero;
    public GameObject pantallaCarga;
    public GameObject PantallaAterrizaje;
    public GameObject botonPausa;
    public GameObject UI_Juego;
    public GameObject PantallaGameOver;

    
    public GameObject pantallaAterrizaje;

    [HideInInspector]
    public float Xjugador;
    [HideInInspector]
    public float Yjugador;
    [HideInInspector]
    public float Zjugador;
    void Start () {
        JuegoEmpezado = true;
        EnCreditos = false;
        JuegoTerminado = false;
        alturaCero.SetActive(false);
        pantallaCarga.SetActive(true);
        PantallaGameOver.SetActive(false);
        nivelActual = 0;
        Xjugador = -11.89f;
        Yjugador = 14.91f;
        Zjugador = -1.911f;
        cantNiveles = 3;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Get().EnCreditos)
        {
            alturaCero.SetActive(false);
            pantallaCarga.SetActive(false);
            PantallaAterrizaje.SetActive(false);
            botonPausa.SetActive(false);
            UI_Juego.SetActive(false);
            PantallaGameOver.SetActive(false);
        }
        
        if (CargarNivel.Get().prenderGameObjects)
        {
            UI_Juego.SetActive(true);
            alturaCero.SetActive(true);
            HabilitarMovimiento = true;
            CargarNivel.Get().prenderGameObjects = false;
            PlayerController.Get().puntos = 0;
            PlayerController.Get().gasolina = 3000;
        }
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
    public static GameManager Get()
    {
        return instance;
    }
    public void PasarNivel()
    {
        if (JuegoTerminado == false)
        {
            CargarNivel.Get().cargarNivel = true;
            tipoTextoPuntaje = 0;
            SetearJugador();
            PantallaAterrizaje.SetActive(false);
            botonPausa.SetActive(false);
            UI_Juego.SetActive(false);
            alturaCero.SetActive(false);
            pantallaCarga.SetActive(true);
            PantallaGameOver.SetActive(false);
            if (nivelActual < cantNiveles - 1)
            {
                nivelActual = nivelActual + 1;
            }
            else
            {
                nivelActual = 0;
            }
            camara.orthographicSize = 10;
        }
    }
    public void SetearJugador()
    {
        jugador.transform.position = new Vector3(Xjugador, Yjugador, Zjugador);
    }
}

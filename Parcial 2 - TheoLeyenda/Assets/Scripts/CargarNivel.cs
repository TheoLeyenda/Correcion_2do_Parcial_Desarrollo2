using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargarNivel : MonoBehaviour {
    [HideInInspector]
    public int NumeroNivel;
    public GameObject pantallaCarga;
    public GameObject alturaCero;
    public Text textoNivel;
    public Text textoPorcentaje;
    private static CargarNivel instance;
    public Scrollbar BarraDeCarga;
    [HideInInspector]
    public bool cargarNivel;
    [HideInInspector]
    public float carga;
    [HideInInspector]
    private float velCarga;
    private float total;
    public bool prenderGameObjects;
	// Use this for initialization
	void Start () {
        NumeroNivel = 1;
        cargarNivel = true;
        velCarga = 10;
        total = 100;
	}
    private void Awake()
    {
        instance = this;
    }
    public static CargarNivel Get()
    {
        return instance;
    }
    // Update is called once per frame
    void Update () {
        if (cargarNivel)
        {
            carga = carga + (Time.deltaTime* velCarga);
        }
        //BarraDeCarga.size = carga / 100;
        textoNivel.text = "Nivel " + NumeroNivel;
        textoPorcentaje.text = "" + (int)carga + "%";
        if (carga >= 100 && cargarNivel)
        {
            prenderGameObjects = true;
            GameManager.Get().asignarNivel = true;
            GameManager.Get().UI_Juego.SetActive(true);
            alturaCero.SetActive(true);
            GameManager.Get().botonPausa.SetActive(true);
            NumeroNivel++;//este va a aparecer en el texto luego
            carga = 0;
            cargarNivel = false;
            pantallaCarga.SetActive(false);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBotton : MonoBehaviour {

    // Use this for initialization
    public void Jugar()
    {
        Time.timeScale = 1;
        if (GameManager.Get() != null)
        {
            GameManager.Get().EnCreditos = false;
            GameManager.Get().pantallaCarga.SetActive(true);
            CargarNivel.Get().carga = 0;

        }
        SceneManager.LoadScene("Nivel 1");
        if (GameManager.Get() != null)
        {
            if (GameManager.Get().JuegoEmpezado)
            {
                Debug.Log("Entre");
                CargarNivel.Get().NumeroNivel = 1;
                UI_Controller .Get().segundos = 0;
                UI_Controller.Get().minutos = 0;
                UI_Controller.Get().puntos = 0;
                GameManager.Get().nivelActual = 3;
                PlayerController.Get().velCaida = 0;
                PasarNivel();
            }
        }
        
        
    }
    public void Salir()
    {
        Application.Quit();
        //Time.timeScale = 0;
        UnityEditor.EditorApplication.isPlaying = false;

    }
    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
        if (GameManager.Get() != null)
        {
            GameManager.Get().EnCreditos = true;
        }
        
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        if (GameManager.Get() != null)
        {
            GameManager.Get().EnCreditos = false;
        }
    }
    public void PasarNivel()
    {
        if (GameManager.Get() != null)
        {
            GameManager.Get().EnCreditos = false;
            GameManager.Get().PasarNivel();

            if (GameManager.Get().nivelActual == 0)
            {
                SceneManager.LoadScene("Nivel 1(Clone)");
            }
            if (GameManager.Get().nivelActual == 1)
            {
                SceneManager.LoadScene("Nivel 2");
            }
            if (GameManager.Get().nivelActual == 2)
            {
                SceneManager.LoadScene("Nivel 3");
            }
        }
        
    }
}

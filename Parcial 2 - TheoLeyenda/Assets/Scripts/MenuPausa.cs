using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour {

    // Use this for initialization
    public GameObject menuPausa;
	void Start () {
        menuPausa.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Pausa()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continuar()
    {
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }
    public void MenuInicial()
    {
        //Time.timeScale = 0f;
        menuPausa.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}

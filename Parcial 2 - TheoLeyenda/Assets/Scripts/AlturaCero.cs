using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlturaCero : MonoBehaviour {

    // Use this for initialization
    private static AlturaCero instance = null;
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
		
	}
}

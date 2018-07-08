using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjustarTamanioFondo : MonoBehaviour {

	// Use this for initialization
    public Camera refCamara;
	void Start () {
        transform.localScale = new Vector3(refCamara.scaledPixelWidth, refCamara.scaledPixelHeight,transform.localScale.z);
	}
}

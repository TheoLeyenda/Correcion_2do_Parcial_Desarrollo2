using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Use this for initialization   
    public float fuerzaPropulsores;
    public float velocidadRotacion;
    public float gravedad;
    public float TopeFuerzaPropulsores;
    public ParticleSystem sistemaParticulas;
    private float fuerzaPropulsoresVertical;
    [HideInInspector]
    public float gasolina;
    private float altitud;
    private float rotacion;
    private float contadorH;
    private float contadorV;
    private float maxVelHorizontal;
    private float maxVelVertical;
    [HideInInspector]
    public int puntos;
    [HideInInspector]
    public float velCaida;
    public float alturaAchicarCamara;
    private bool ampliar;
    public Camera refCamara;
    private float auxiliarX;
    private float velAterrizaje;
    private bool mover;
    private bool restaurarMovimiento;

    private float velocidadVertical;

    private static PlayerController instance = null;
    private Rigidbody rig;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        contadorH = 0;
        velocidadVertical = 0;
        rotacion = velocidadRotacion;
        sistemaParticulas.Stop();
        maxVelHorizontal = 180;
        gasolina = 3000;
        maxVelVertical = 200;
        Time.timeScale = 1;
        ampliar = true;
        auxiliarX = 0;
        velAterrizaje = 0f;
        mover = true;
        velCaida = 0;


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
    void Update()
    {
        if (GameManager.Get().HabilitarMovimiento)
        {
            GameManager.Get().HabilitarMovimiento = false;
            mover = true;
            restaurarMovimiento = true;
        }
        if (Time.timeScale != 0)
        {
            altitud = transform.position.y;
            UI_Controller.Get().puntos = puntos;
            if (mover)
            {
                UI_Controller.Get().ContarTiempo();
                UI_Controller.Get().altitudJugador = altitud;
                UI_Controller.Get().gasolinaJugador = gasolina;
                UI_Controller.Get().velocidadHorizontalJugador = contadorH;
                UI_Controller.Get().velocidadVerticalJugador = contadorV;
                if (restaurarMovimiento)
                {
                    RestaurarMovimiento();
                    restaurarMovimiento = false;
                }
                Movimiento();
            }
            CamaraAterrizaje();

        }
    }
    private void Quieto()
    {
        sistemaParticulas.Stop();
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rig.constraints = RigidbodyConstraints.FreezePosition;
        rig.constraints = RigidbodyConstraints.FreezeRotation;

    }
    private void RestaurarMovimiento()
    {
        rig.constraints = RigidbodyConstraints.None;
        rig.constraints = RigidbodyConstraints.FreezeRotationX;
        rig.constraints = RigidbodyConstraints.FreezeRotationY;

    }
    private void Movimiento()
    {
        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.Space))
        {
            velocidadVertical = 0;
            if (contadorV > 0)
            {
                //contadorV = 0;
            }
            if (contadorV > -maxVelVertical)
            {
                contadorV--;
            }
            contadorH = 0;
            fuerzaPropulsoresVertical = 0;
            velCaida = (velocidadVertical - gravedad);
            rig.AddForce(new Vector3(0, velCaida, 0), ForceMode.Acceleration);
            if (sistemaParticulas.isPlaying == true)
            {
                sistemaParticulas.Stop();
            }

        }


        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
        {
            if (gasolina > 0)
            {
                gasolina = gasolina - 1;
                if (contadorV < 0)
                {
                    //contadorV = 0;
                }
                if (contadorV < maxVelVertical)
                {
                    contadorV++;
                }

                if (transform.rotation.z > 0 && contadorH * -1 < maxVelHorizontal)
                {
                    contadorH--;
                }
                if (transform.rotation.z < 0 && contadorH < maxVelHorizontal)
                {
                    contadorH++;
                }

                if (sistemaParticulas.isPlaying == false)
                {
                    sistemaParticulas.Play();
                }
                if (fuerzaPropulsoresVertical < TopeFuerzaPropulsores)
                {
                    fuerzaPropulsoresVertical = fuerzaPropulsoresVertical + fuerzaPropulsores;
                }
                velocidadVertical = fuerzaPropulsoresVertical;
            }
            if (gasolina <= 0)
            {
                contadorH = 0;
                velocidadVertical = 0;
                GameManager.Get().PantallaGameOver.SetActive(true);
                GameManager.Get().JuegoTerminado = true;
            }
            rig.AddRelativeForce(new Vector3(0, velocidadVertical - gravedad, 0), ForceMode.Acceleration);
            if (transform.position.x > refCamara.pixelWidth)
            {
                transform.position = new Vector3(transform.position.x - 10, transform.position.y, transform.position.z);
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) && transform.localRotation.z < 0.7)
        {

            if (gasolina > 0)
            {
                transform.RotateAround(transform.position, new Vector3(0, 0, rotacion), rotacion);
            }

        }

        if (Input.GetKey(KeyCode.RightArrow) && transform.localRotation.z > -0.7)
        {
            if (gasolina > 0)
            {
                transform.RotateAround(transform.position, new Vector3(0, 0, -rotacion), rotacion);
            }
        }

    }
    public static PlayerController Get()
    {
        return instance;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (mover && GameManager.Get().JuegoTerminado == false)
        {
            //var fwdSpeed = Vector3.Dot(rig.velocity, transform.forward);
            float auxVel = collision.relativeVelocity.magnitude;//con esta linea obtengo la velocidad del objeto segun la magnitud de su movimiento
            //(yo me entiendo).

            

            if (collision.gameObject.tag == "x2" && (int)auxVel <= velAterrizaje && this.gameObject.tag == "Jugador")
            {
                Quieto();
                GameManager.Get().tipoTextoPuntaje = 1;
                UI_Controller.Get().buenAterrizaje = true;
                GameManager.Get().pantallaAterrizaje.SetActive(true);
                mover = false;
                puntos = puntos + 60;
            }
            if (collision.gameObject.tag == "x3" && (int)auxVel <= velAterrizaje && this.gameObject.tag == "Jugador")
            {
                Quieto();
                GameManager.Get().tipoTextoPuntaje = 2;
                UI_Controller.Get().buenAterrizaje = true;
                GameManager.Get().pantallaAterrizaje.SetActive(true);

                mover = false;
                puntos = puntos + 90;
            }
            if (collision.gameObject.tag == "x4" && (int)auxVel <= velAterrizaje && this.gameObject.tag == "Jugador")
            {
                Quieto();
                GameManager.Get().tipoTextoPuntaje = 3;
                UI_Controller.Get().buenAterrizaje = true;
                GameManager.Get().pantallaAterrizaje.SetActive(true);
                mover = false;
                puntos = puntos + 180;

            }
            if (collision.gameObject.tag == "x5" && (int)auxVel <= velAterrizaje && this.gameObject.tag == "Jugador")
            {
                Quieto();
                GameManager.Get().tipoTextoPuntaje = 4;
                UI_Controller.Get().buenAterrizaje = true;
                GameManager.Get().pantallaAterrizaje.SetActive(true);
                mover = false;
                puntos = puntos + 250;
            }
            if ((collision.gameObject.tag == "ZonaMuerte") || (collision.gameObject.tag == "x2" && (int)auxVel > velAterrizaje) || (collision.gameObject.tag == "x3" && (int)auxVel > velAterrizaje) || (collision.gameObject.tag == "x4" && (int)auxVel > velAterrizaje) || (collision.gameObject.tag == "x5" && (int)auxVel > velAterrizaje))
            {
                Quieto();
                mover = false;
                UI_Controller.Get().buenAterrizaje = false;
                GameManager.Get().pantallaAterrizaje.SetActive(true);
                GameManager.Get().tipoTextoPuntaje = 0;
            }
        }
    }
    public void CamaraAterrizaje()
    {
        //Codigo de asercamiento de camara.
        //USARLO CON COLLIDERS EN PARTES DEL NIVEL.

        if (transform.position.y < alturaAchicarCamara)
        {
            refCamara.orthographicSize = 5;
            if (ampliar)
            {
                ampliar = false;
                auxiliarX = transform.position.x;
                refCamara.transform.position = new Vector3(auxiliarX, refCamara.transform.position.y - 5, refCamara.transform.position.z + 5);
            }
        }
        else
        {

            refCamara.orthographicSize = 10;
            if (!ampliar)
            {
                refCamara.transform.position = new Vector3(refCamara.transform.position.x - auxiliarX, refCamara.transform.position.y + 5, refCamara.transform.position.z - 5);
                ampliar = true;
            }
        }
        //---------------------------------------------------------------------------
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Jugador : NetworkBehaviour
{
    public GameObject area;
    private float anchoArea, altoArea;
    private float anchoVentana, altoVentana;

    public int invertirMovJugador2 = 1;
    private bool esPrimerUpdate = true;
    private Vector3 posicionInicial;

    void Start()
    {
        anchoArea = area.transform.localScale.x * 1.0f;
        altoArea = area.transform.localScale.z * 1.0f;

        anchoVentana = Screen.width;
        altoVentana = Screen.height;
    }

    void FixedUpdate()
    {
        if (hasAuthority)
        {
            Vector3 posicionActual = Vector3.one;
            Vector3 posicionFinal = Vector3.one;
            Vector3 difPosicion = Vector3.one;
            posicionActual.x = (anchoArea / anchoVentana) * Input.mousePosition.x;
            posicionActual.z = (altoArea / altoVentana) * Input.mousePosition.y;
            posicionActual.x += area.transform.position.x - (anchoArea / 2.0f);
            posicionActual.z += area.transform.position.z - (altoArea / 2.0f);

            posicionActual.y = 0.32f;
            //posicionActual.y = transform.position.y;

            if (esPrimerUpdate)
            {
                posicionInicial = posicionActual;
                esPrimerUpdate = false;
            }
            else
            {
                if (invertirMovJugador2 == -1)
                {
                    posicionActual.x = - posicionActual.x;
                    posicionActual.z = -posicionActual.z;
                    posicionFinal = posicionActual;

                    //difPosicion = posicionActual - posicionInicial;
                    //posicionFinal = posicionInicial - difPosicion;
                    //posicionFinal.x = invertirMovJugador2 * posicionActual.x;
                    //posicionFinal.z = invertirMovJugador2 * posicionActual.z;
                    //posicionFinal.y = 0.32f;
                }
                else
                    posicionFinal = posicionActual;

                transform.position = posicionFinal;
            }

            //transform.position = posicionActual;

            //posicionActual.x = invertirMovJugador2 * posicionActual.x;
            //posicionActual.z = invertirMovJugador2 * posicionActual.z;

            // Chequea si cambió su posición, es decir, si se movió el mouse.
            // Chequeo necesario porque si no, ni bien Respawnea el jugador2, se invierte el valor de z y
            // reaparece en el campo contrario.
            //if ((posicionActual.x - transform.position.x >= 0.01f) | (posicionActual.z - transform.position.z >= 0.01f))  // posicionActual.y == transform.position.y == 0.32f SIEMPRE!
            //    transform.position = posicionActual;
            //Debug.Log("X");
            //Debug.Log(transform.position.x);
            //Debug.Log("Z");
            //Debug.Log(transform.position.z);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
    public Jugador jugador1;
    public Jugador jugador2;
    public Disco disco;

    public GUI managerGUI;

    public int golesJugador1;
    public int golesJugador2;

    public float tiempo;

    public GameObject ojoDerecho;

    public enum Estados
    {
        inicio,
        sacaJugador1,
        sacaJugador2,
		sacandoJugador2,
        golJugador1,
        golJugador2,
        jugando,
        fin,
        esperandoReinicio,
        pinchado
    }

    public Estados estado;

	private int jugadoresConectados = 0;

    void Start()
    {
		// disco.desactivar();
		// Instantiate(disco);
        // Cursor.visible = false;
        estado = Estados.inicio;
    }

    void Update()
    {
        switch (estado)
        {
            case Estados.inicio:
                Debug.Log(jugadoresConectados);
				// if ( jugadoresConectados == 2 )
				// {
				// 	disco.activar();
                //  managerGUI.setMensajeControl("");
				// 	resetearPosiciones(-3.2f);
				// 	golesJugador1 = 0;
				// 	golesJugador2 = 0;
				// 	estado = Estados.sacaJugador1;
				// 	resetearPosiciones(-3.2f);
                //  Cursor.visible = false;
				// }
                break;

            // case Estados.sacaJugador2:
            //     if (Input.GetAxis("Mouse ScrollWheel") != 0.0f)
            //     {
            //         Vector2 posJugador2 = jugador2.getPosicion();
            //         Vector2 posDisco = disco.getPosicion();
            //         dirGolpe = posDisco - posJugador2;
            //         estado = Estados.sacandoJugador2;
            //     }
            //     break;

			// case Estados.sacandoJugador2:
			// 	jugador2.sacar(dirGolpe);
			// 	break;

            case Estados.jugando:
                float tiempo_actual = Time.time;

                if (tiempo_actual - tiempo > 7.0f)
                {
                    disco.setMovimiento(0.0f, 0.0f, 0.0f);
                    estado = Estados.pinchado;
                }
                break;

            case Estados.golJugador2:
            case Estados.golJugador1:
                managerGUI.setGoles(golesJugador1, golesJugador2);

                if (golesJugador1 == 10 || golesJugador2 == 10)
                {
                    // juego terminado
                    estado = Estados.fin;
                    resetearPosiciones(0.0f);
                }
                else if (estado == Estados.golJugador1)
                {
                    // gol del Jugador1
                    estado = Estados.sacaJugador2;
                    resetearPosiciones(3.2f);
                }
                else
                {
                    // gol del Jugador2
                    estado = Estados.sacaJugador1;
                    resetearPosiciones(-3.2f);
                }
                break;

            case Estados.fin:
                if (golesJugador1 == 10)
                    managerGUI.setMensajeControl("Ganó el jugador 1! Apriete algún botón para reiniciar.");
                else
                    managerGUI.setMensajeControl("Ganó el robot! Apriete algún botón para reiniciar.");
                estado = Estados.esperandoReinicio;
                break;

            case Estados.esperandoReinicio:
                // esperar input
				disco.desactivar();
                if (Input.GetAxis("Mouse ScrollWheel") != 0.0f ||
                     Input.GetButton("Fire2") ||
                     Input.GetButton("Fire1"))
                    estado = Estados.inicio;
                break;

            case Estados.pinchado:
                managerGUI.setMensajeControl("Se pinchó!!!");
				disco.desactivar();

                // esperar input
                if (Input.GetAxis("Mouse ScrollWheel") != 0.0f ||
                     Input.GetButton("Fire2") ||
                     Input.GetButton("Fire1"))
                {
					disco.activar();
                    managerGUI.setMensajeControl("");
                    resetearPosiciones(-3.2f);
                    estado = Estados.sacaJugador1;
                }
                break;
        }
    }

    public void cambiarModo(bool VR)
    {
        Vector3 posicion = ojoDerecho.transform.position;
        Vector3 escala = ojoDerecho.transform.localScale;
        if (VR)
        {
            escala.x = 10.0f;
            posicion.x = 50.0f;
            ojoDerecho.transform.localScale = escala;
            ojoDerecho.transform.position = posicion;
        }
        else
        {
            escala.x = 20.0f;
            posicion.x = 0.0f;
            ojoDerecho.transform.localScale = escala;
            ojoDerecho.transform.position = posicion;
        }
    }

	public void jugadorConectado(Jugador jugador)
	{
		Debug.Log(disco);
		jugadoresConectados++;
		switch (jugadoresConectados)
		{
			case 1:
				jugador1 = jugador;
				// jugador1.setPosicion(0.0f, 0.0f, -6.4f);
				break;
			case 2:
				resetearPosiciones(-3.2f);
				jugador2 = jugador;
				// jugador2.setPosicion(0.0f, 0.0f, 6.4f);
				break;
		}
	}

    void resetearPosiciones(float posicionDisco)
    {
        // jugador1.setPosicion(0.0f, 0.0f, -6.4f);
        // jugador2.setPosicion(0.0f, 0.0f, 6.4f);
        disco.setPosicion(0.0f, 0.0f, posicionDisco);
    }

    public void colisionDiscoJugador()
    {
        tiempo = Time.time;
        estado = Estados.jugando;
    }

    public void colisionDiscoFondo()
    {
        tiempo = Time.time;
    }

    public void golJugador2()
    {
        golesJugador2++;
        estado = Estados.golJugador2;
    }

    public void golJugador1()
    {
        golesJugador1++;
        estado = Estados.golJugador1;
    }

    public bool sacaJugador2()
    {
        return estado == Estados.sacaJugador2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
    public Jugador jugador1;
    public Jugador jugador2;
    public Disco disco;

    public UnityEngine.UI.Text[] golesGUI_Jugador1 = { null, null };
    public int golesJugador1;

    public UnityEngine.UI.Text[] golesGUI_Jugador2 = { null, null };
    public int golesJugador2;

    public float tiempo;

    public UnityEngine.UI.Text mensaje_control;

    private Vector2 dirGolpe;

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
		disco.desactivar();
		Instantiate(disco);
        estado = Estados.inicio;
        //Cursor.visible = false;
    }

    void Update()
    {
        switch (estado)
        {
            case Estados.inicio:
				if ( jugadoresConectados == 2 )
				{
					disco.activar();
					mensaje_control.text = "";
					resetearPosiciones(-3.2f);
					golesJugador1 = 0;
					golesJugador2 = 0;
					estado = Estados.sacaJugador1;
					resetearPosiciones(-3.2f);
				}
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
                golesGUI_Jugador1[0].text = golesJugador1.ToString();
                golesGUI_Jugador2[0].text = golesJugador1.ToString();
                golesGUI_Jugador1[1].text = golesJugador2.ToString();
                golesGUI_Jugador2[1].text = golesJugador2.ToString();

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
                    mensaje_control.text = "Ganó el jugador 1! Apriete algún botón para reiniciar.";
                else
                    mensaje_control.text = "Ganó el robot! Apriete algún botón para reiniciar.";
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
                mensaje_control.text = "Se pinchó!!!";
				disco.desactivar();

                // esperar input
                if (Input.GetAxis("Mouse ScrollWheel") != 0.0f ||
                     Input.GetButton("Fire2") ||
                     Input.GetButton("Fire1"))
                {
					disco.activar();
                    mensaje_control.text = " ";
                    resetearPosiciones(-3.2f);
                    estado = Estados.sacaJugador1;
                }
                break;
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
				jugador1.setPosicion(0.0f, 0.0f, -6.4f);
				jugador1.setLugar(-1.0f);
				break;
			case 2:
				resetearPosiciones(-3.2f);
				jugador2 = jugador;
				jugador2.setPosicion(0.0f, 0.0f, 6.4f);
				jugador2.setLugar(1.0f);
				break;
			case 3:
				jugadoresConectados = 2;
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


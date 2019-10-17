using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
    public ControladorJugador1 jugador1;
    public ControladorIA IA;
    public ControladorDisco disco;

    public UnityEngine.UI.Text[] golesGUIJugador1 = { null, null };
    public int golesJugador1;

    public UnityEngine.UI.Text[] golesGUI_IA = { null, null };
    public int golesIA;

    public float tiempo;

    public UnityEngine.UI.Text mensaje_control;

    private Vector2 dirGolpe;

    private Vector3 posicionMouse;

    public enum Estados
    {
        inicio,
        sacaJugador1,
        sacandoJugador1,
        sacaIA,
        sacandoIA,
        golJugador1,
        golIA,
        jugando,
        fin,
        esperandoReinicio,
        pinchado
    }
    
    public Estados estado;

    void Start()
    {
        estado = Estados.inicio;
        Cursor.visible = false;
    }

    void Update()
    {
        Input.mousePosition.Set(0.0f, 0.0f, 0.0f);
        switch (estado)
        {
            case Estados.inicio:
                posicionMouse = Input.mousePosition;
                mensaje_control.text = "";
                resetearPosiciones(-3.0f);
                golesJugador1 = 0;
                golesIA = 0;
                estado = Estados.sacaJugador1;
                resetearPosiciones(-3.0f);
                break;

            case Estados.sacaIA:
                IA.move();
                if (Input.GetAxis("Mouse ScrollWheel") != 0.0f)
                {
                    Vector2 posIA = IA.getPosicion();
                    Vector2 posDisco = disco.getPosicion();
                    dirGolpe = posDisco - posIA;
                    estado = Estados.sacandoIA;
                }
                break;

            case Estados.sacandoIA:
                IA.sacar(dirGolpe);
                break;

            case Estados.sacaJugador1:
                Vector2 mouse = moverMouse();
                jugador1.move(mouse.x, mouse.y);

                if (Input.GetAxis("Mouse ScrollWheel") != 0.0f)
                {
                    Vector2 posJugador1 = jugador1.getPosicion();
                    Vector2 posDisco = disco.getPosicion();
                    dirGolpe = posDisco - posJugador1;
                    estado = Estados.sacandoJugador1;
                }

                break;

            case Estados.sacandoJugador1:
                jugador1.sacar(dirGolpe);
                break;

            case Estados.jugando:
                float tiempo_actual = Time.time;

                if (tiempo_actual - tiempo > 3.0f)
                {
                    disco.setMovimiento(0.0f, 0.0f, 0.0f);
                    estado = Estados.pinchado;
                }
                else
                {
                    mouse = moverMouse();
                    jugador1.move(mouse.x, mouse.y);
                    IA.move();
                }
                break;

            case Estados.golIA:
            case Estados.golJugador1:
                golesGUIJugador1[0].text = golesJugador1.ToString();
                golesGUI_IA[0].text = golesJugador1.ToString();
                golesGUIJugador1[1].text = golesIA.ToString();
                golesGUI_IA[1].text = golesIA.ToString();

                if (golesJugador1 == 10 || golesIA == 10)
                {
                    // juego terminado
                    estado = Estados.fin;
                    resetearPosiciones(0.0f);
                }
                else if (estado == Estados.golJugador1)
                {
                    // gol del Jugador1
                    estado = Estados.sacaIA;
                    resetearPosiciones(3.0f);
                }
                else
                {
                    // gol del IA
                    estado = Estados.sacaJugador1;
                    resetearPosiciones(-3.0f);
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
                if (Input.GetAxis("Mouse ScrollWheel") != 0.0f ||
                     Input.GetButton("Fire2") ||
                     Input.GetButton("Fire1"))
                    estado = Estados.inicio;
                break;

            case Estados.pinchado:
                mensaje_control.text = "Se pinchó!!!";

                // esperar input
                if (Input.GetAxis("Mouse ScrollWheel") != 0.0f ||
                     Input.GetButton("Fire2") ||
                     Input.GetButton("Fire1"))
                {
                    mensaje_control.text = " ";
                    resetearPosiciones(-3.0f);
                    estado = Estados.sacaJugador1;
                }
                break;
        }
    }

    Vector2 moverMouse()
    {
        Vector3 aux = posicionMouse;
        posicionMouse = Input.mousePosition;
        return new Vector2(Input.GetAxis("Mouse X")/2.0f, Input.GetAxis("Mouse Y") / 2.0f);
    }

    void resetearPosiciones(float posicionDisco)
    {
        jugador1.setPosicion(0.0f, 0.0f, -6.0f);
        IA.setPosicion(0.0f, 0.0f, 6.0f);
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

    public void golIA()
    {
        golesIA++;
        estado = Estados.golIA;
    }

    public void golJugador1()
    {
        golesJugador1++;
        estado = Estados.golJugador1;
    }

    public bool sacaIA()
    {
        return estado == Estados.sacaIA;
    }
}


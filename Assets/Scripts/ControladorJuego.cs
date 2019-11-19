using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
    public Jugador jugador1;
    public Jugador jugador2;
    public int jugadorControlado = 1;
    public Disco disco;

    public GUI managerGUI;

    public int golesJugador1;
    public int golesJugador2;

    public float tiempo;

    public Camera camara;

    public EstadoAbstracto estado;

    int cantidadJugadores = 0;

    void Start()
    {
        estado = new EstadoInicio(this);
    }

    void Update()
    {
        estado.Ejecutar();
    }

    public void cambiarModo(bool VR)
    {
        Debug.Log(jugadorControlado);
        Vector3 posicion = camara.transform.position;
        Quaternion rotacion = camara.transform.localRotation;
        if (VR)
        {
            posicion.y = 0.0f;
            posicion.z = 99913.0f;
            rotacion.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

            // (x,y,w,h)
            camara.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
            camara.transform.position = posicion;
            camara.transform.localRotation = rotacion;
        }
        else
        {
            posicion.y = 5.0f;

            if (jugadorControlado == 1)
            {
                posicion.z = -11.0f;
                rotacion.eulerAngles = new Vector3(25.0f, 0.0f, 0.0f);
            }
            else if (jugadorControlado == 2)
            {
                posicion.z = 11.0f;
                rotacion.eulerAngles = new Vector3(25.0f, 180.0f, 0.0f);
            }

            camara.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            camara.transform.position = posicion;
            camara.transform.localRotation = rotacion;
        }
    }

	public void jugadorConectado(Jugador jugador)
	{
        cantidadJugadores = (cantidadJugadores % 2) + 1;
		switch (cantidadJugadores)
		{
			case 1:
				jugador1 = jugador;
                // if (jugador1.tengoAutoridad())
                //     jugadorControlado = 1;
                //managerGUI.habilitacionBotonModo(false);
				break;
			case 2:
				jugador2 = jugador;
                // if (jugador2.tengoAutoridad())
                //     jugadorControlado = 2;
                //managerGUI.habilitacionBotonModo(true);
				break;
		}
	}

    // TODO: cambiar esta lógica por una más robusta (que contemple desconexio-
    // nes de jugadores. no es muy importante, pero estaría bueno.)
    public int jugadoresConectados()
    {
        return cantidadJugadores;
    }

    public void resetearDisco(float posicionDisco)
    {
        disco.setPosicion(0.0f, 0.0f, posicionDisco);
    }

    public void colisionDiscoJugador()
    {
        tiempo = Time.time;
        cambiarEstado(new EstadoJugando(this));
    }

    public void colisionDiscoFondo()
    {
        tiempo = Time.time;
    }

    public void golJugador2()
    {
        golesJugador2++;
        cambiarEstado(new EstadoGolJugador2(this));
    }

    public void golJugador1()
    {
        golesJugador1++;
        cambiarEstado(new EstadoGolJugador1(this));
    }

    public bool sacaJugador2()
    {
        return (typeof(EstadoSacaJugador2).Equals(estado.GetType()));
    }

    public void cambiarEstado(EstadoAbstracto estadoNuevo)
    {
        estado = estadoNuevo;
    }
}

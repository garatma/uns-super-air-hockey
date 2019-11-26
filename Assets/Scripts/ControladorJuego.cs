using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
<<<<<<< Updated upstream
    public Jugador jugador1;
    public Jugador jugador2;
=======
    public int golesJugador1 = 0;
    public int golesJugador2 = 0;
    private int cantidadJugadores = 0;
    private int jugadorControlado = 0;
    private int jugadorQueSaca = 1;
	private bool resetHost = false;

    private bool reiniciaron = false;

>>>>>>> Stashed changes
    public Disco disco;

    public GUI managerGUI;

    public int golesJugador1;
    public int golesJugador2;

    public float tiempo;

    public GameObject ojoDerecho;

    public EstadoAbstracto estado;

    public int jugadoresConectados = 0;

    void Start()
    {
		// disco.desactivar();
		// Instantiate(disco);
        // Cursor.visible = false;
        jugadoresConectados = 0;
        // estado = Estados.inicio;
        estado = new EstadoInicio(this);
    }

    void Update()
    {
        estado.Ejecutar();
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
        if ( jugadoresConectados > 1 ) jugadoresConectados = 0;
		jugadoresConectados++;
		switch (jugadoresConectados)
		{
			case 1:
				jugador1 = jugador;
				break;
			case 2:
				jugador2 = jugador;
				break;
		}
	}

    public int soyJugador()
    {
        return jugadoresConectados;
    }

    public void resetearDisco(float posicionDisco)
    {
        // jugador1.setPosicion(0.0f, 0.0f, -6.4f);
        // jugador2.setPosicion(0.0f, 0.0f, 6.4f);
        disco.setPosicion(0.0f, 0.0f, posicionDisco);
    }

    public void colisionDiscoJugador()
    {
        tiempo = Time.time;
        // estado = Estados.jugando;
        cambiarEstado(new EstadoJugando(this));
    }

    public void colisionDiscoFondo()
    {
        tiempo = Time.time;
    }

    public void golJugador2()
    {
        golesJugador2++;
        // estado = Estados.golJugador2;
        cambiarEstado(new EstadoGolJugador2(this));
    }

    public void golJugador1()
    {
        golesJugador1++;
        // estado = Estados.golJugador1;
        cambiarEstado(new EstadoGolJugador1(this));
    }

    public bool sacaJugador2()
    {
        // return estado == Estados.sacaJugador2;
        //return (typeof(EstadoSacaJugador2) == estado.GetType());
        return (typeof(EstadoSacaJugador2).Equals(estado.GetType()));
    }

    public void cambiarEstado(EstadoAbstracto estadoNuevo)
    {
        estado = estadoNuevo;
    }

    public void reinicar()
    {
        reiniciaron = true;
    }

    public bool reiniciaronPartida()
    {
        return reiniciaron;
    }
}

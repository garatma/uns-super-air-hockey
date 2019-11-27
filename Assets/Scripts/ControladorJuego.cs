using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class ControladorJuego : NetworkBehaviour
{
    public int golesJugador1 = 0;
    public int golesJugador2 = 0;
    private int cantidadJugadores = 0;
    private int jugadorControlado = 0;
    private int jugadorQueSaca = 1;
	private bool resetHost = false;

    public Disco disco;

    public GUI managerGUI;

    public float tiempo;

    public Camera camara;

    public EstadoAbstracto estado;

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

            // (x,y,w,h)
            camara.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            camara.transform.position = posicion;
            camara.transform.localRotation = rotacion;
        }
    }

    [ClientRpc]
    public void RpcReiniciarTodo()
    {
        resetearDisco(0.0f);
        disco.setDireccion(0.0f, 0.0f, 0.0f);
        disco.desactivar();
        cantidadJugadores = 0;
        jugadorQueSaca = 1;
        golesJugador1 = 0;
        golesJugador2 = 0;
        managerGUI.habilitarBoton(false);
		managerGUI.setMensajeControl("");
        resetearDisco(0.0f);
        cambiarEstado(new EstadoInicio(this));
    }

    [ClientRpc]
    public void RpcNuevoJugador(int cantidadJugadores, int golesJugador1, int golesJugador2, int quienSaca)
    {
        this.cantidadJugadores = cantidadJugadores;
        this.golesJugador1 = golesJugador1;
        this.golesJugador2 = golesJugador2;
        managerGUI.setGoles(golesJugador1, golesJugador2);
        switch (cantidadJugadores)
        {
            case 1:
                // el primer cliente viene por acá y controla al jugador1.
                if (jugadorControlado == 0) jugadorControlado = 1;
                managerGUI.habilitarBoton(false);
				disco.desactivar();
                break;
            case 2:
                // el primer cliente que entra acá no debería hacer nada.
                // el segundo cliente viene por acá y controla al jugador2 y se actualiza al estado
                // del servidor.
                if (jugadorControlado == 0)
                {
                    jugadorControlado = 2;
                    if (quienSaca == 1)
                        cambiarEstado(new EstadoSacaJugador1(this));
                    else if (quienSaca == 2)
                        cambiarEstado(new EstadoSacaJugador2(this));
                }
                managerGUI.habilitarBoton(true);
                disco.activar();
                break;
        }
    }

    public void sacaJugador(int jugador)
    {
        jugadorQueSaca = jugador;
    }

    public int getJugadorQueSaca()
    {
        return jugadorQueSaca;
    }

	public bool inputHost()
	{
		return resetHost;
	}

	// el server lo ejecuta
	[Command]
	public void CmdHostReset()
	{
		resetHost = true;
		RpcReset();
	}

	// lo ejecutan los clientes
	[ClientRpc]
	public void RpcReset()
	{
		resetHost = true;
	}
	
	public void resetearControlReinicio()
	{
		resetHost = false;
	}

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

    public void cambiarEstado(EstadoAbstracto estadoNuevo)
    {
        estado = estadoNuevo;
    }

    [ClientRpc]
    public void RpcGolJugador2()
    {
        golesJugador2++;
        cambiarEstado(new EstadoGolJugador2(this));
    }

    [ClientRpc]
    public void RpcGolJugador1()
    {
        golesJugador1++;
        cambiarEstado(new EstadoGolJugador1(this));
    }

    public int getGolesJugador1()
    {
        return golesJugador1;
    }

    public int getGolesJugador2()
    {
        return golesJugador2;
    }
}

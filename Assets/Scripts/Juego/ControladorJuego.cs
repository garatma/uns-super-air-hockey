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

	private bool reinicioPartida = false;
	private bool reinicioPunto = false;

    public Disco disco;

    public GUI managerGUI;
    public Menus managerMenus;

    public float tiempo;

    public Camera camara;

    public Estado estado;

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
		reinicioPuntoListo();
		reinicioPartidaListo();
        resetearDisco(0.0f);
        disco.setearDireccion(0.0f, 0.0f, 0.0f);
        disco.desactivar();
        cantidadJugadores = 0;
        golesJugador1 = 0;
        golesJugador2 = 0;
        managerGUI.habilitarBoton(false);
		managerGUI.setearMensajeControl("");
		managerMenus.mostrarInterfazNetwork(true);
        resetearDisco(0.0f);
        cambiarEstado(new EstadoInicio(this));
		reinicioPartida = false;
		reinicioPunto = false;
    }

    [ClientRpc]
    public void RpcConectarNuevoJugador(int cantidadJugadores)
    {
        this.cantidadJugadores = cantidadJugadores;
        switch (cantidadJugadores)
        {
            case 1:
                // el primer cliente viene por acá y controla al jugador1.
                if (jugadorControlado == 0) 
					jugadorControlado = 1;
                managerGUI.habilitarBoton(false);
				managerMenus.mostrarInterfazNetwork(true);
				disco.desactivar();
                break;
            case 2:
                // el primer cliente que entra acá no debería hacer nada.
                // el segundo cliente viene por acá y controla al jugador2 y se actualiza al estado
                // del servidor.
                if (jugadorControlado == 0)
                    jugadorControlado = 2;
                managerGUI.habilitarBoton(true);
				managerMenus.mostrarInterfazNetwork(false);
                disco.activar();
                break;
        }
    }

    public int obtenerJugadoresConectados()
    {
        return cantidadJugadores;
    }

    public void resetearDisco(float posicionDisco)
    {
        disco.setearPosicion(0.0f, 0.0f, posicionDisco);
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

    public void cambiarEstado(Estado estadoNuevo)
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

	[Command]
	public void CmdReiniciarPartida()
	{
		reinicioPartida = true;
		RpcReiniciarPartida();
	}

	[ClientRpc]
	public void RpcReiniciarPartida()
	{
		reinicioPartida = true;
	}

	[Command]
	public void CmdReiniciarPunto()
	{
		reinicioPunto = true;
		RpcReiniciarPunto();
	}

	[ClientRpc]
	public void RpcReiniciarPunto()
	{
		reinicioPunto = true;
	}

	public bool reiniciaronPartida()
	{
		return reinicioPartida;
	}

	public bool reiniciaronPunto()
	{
		return reinicioPunto;
	}

	public void reinicioPartidaListo()
	{
		reinicioPartida = false;
	}

	public void reinicioPuntoListo()
	{
		reinicioPunto = false;
	}
}

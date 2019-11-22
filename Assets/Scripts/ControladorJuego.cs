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
        jugadorControlado = 0;
        jugadorQueSaca = 1;
        golesJugador1 = 0;
        golesJugador2 = 0;
        managerGUI.habilitarBoton(false);
        resetearDisco(0.0f);
        cambiarEstado(new EstadoInicio(this));
    }

    /*
        TODO: 1) COMMAND para avisar que hubo un gol
              2) RPC para avisar a los jugadores del gol
    */

    [ClientRpc]
    public void RpcNuevoJugador(int cantidadJugadores, int golesJugador1, int golesJugador2, int quienSaca)
    {
        Debug.Log(cantidadJugadores);
        Debug.Log(golesJugador1);
        Debug.Log(golesJugador2);
        Debug.Log(quienSaca);
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

    [ClientRpc]
    public void RpcGolJugador2()
    {
        Debug.Log("juego: gol jugador 2");
        golesJugador2++;
        cambiarEstado(new EstadoGolJugador2(this));
    }

    [ClientRpc]
    public void RpcGolJugador1()
    {
        Debug.Log("juego: gol jugador 1");
        golesJugador1++;
        cambiarEstado(new EstadoGolJugador1(this));
    }

    public void cambiarEstado(EstadoAbstracto estadoNuevo)
    {
        estado = estadoNuevo;
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

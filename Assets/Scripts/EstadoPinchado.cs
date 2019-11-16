using System;
using UnityEngine;

public class EstadoPinchado : EstadoAbstracto
{
    public ControladorJuego juego;

    public EstadoPinchado(ControladorJuego ctrlJuego)
    {
        // Constructor
        this.juego = ctrlJuego;
    }

    public override void Ejecutar()
    {
        // Realiza la acción correspondiente
        juego.managerGUI.setMensajeControl("Se pinchó!!!");
        juego.disco.desactivar();

        // Esperar input
        if (Input.GetAxis("Mouse ScrollWheel") != 0.0f ||
             Input.GetButton("Fire2") ||
             Input.GetButton("Fire1"))
        {
            juego.disco.activar();
            juego.managerGUI.setMensajeControl("");
            juego.resetearDisco(-3.2f);
            // estado = Estados.sacaJugador1;
            juego.cambiarEstado(new EstadoSacaJugador1(juego));
        }
    }
}
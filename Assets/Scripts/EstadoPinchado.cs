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

        // TODO: mejorar para que avise al otro jugador de que apretó para reiniciar.
        // Esperar input
        if (Input.GetAxis("Mouse ScrollWheel") != 0.0f ||
             Input.GetButton("Fire2") ||
             Input.GetButton("Fire1"))
        {
            if (juego.disco != null) juego.disco.activar();
            juego.managerGUI.setMensajeControl("");
            if (juego.disco != null) juego.resetearDisco(-3.2f);
            juego.cambiarEstado(new EstadoSacaJugador1(juego));
        }
    }
}

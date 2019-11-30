using System;
using UnityEngine;

public class EstadoJugando : EstadoAbstracto
{
    public ControladorJuego juego;

    public EstadoJugando(ControladorJuego ctrlJuego)
    {
        // Constructor
        this.juego = ctrlJuego;
    }

    public override void Ejecutar()
    {
        // Realiza la acción correspondiente
        float tiempo_actual = Time.time;

        if (tiempo_actual - juego.tiempo > 7.0f)
        {
            juego.disco.setearDireccion(0.0f, 0.0f, 0.0f);
            juego.managerGUI.setearMensajeControl("Se pinchó!!!");
            juego.disco.desactivar();
            juego.cambiarEstado(new EstadoPinchado(juego));
        }
    }
}

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

        // TODO: mejorar para que avise al otro jugador de que apretó para reiniciar.
        if (tiempo_actual - juego.tiempo > 7.0f)
        {
            if (juego.disco != null) juego.disco.setDireccion(0.0f, 0.0f, 0.0f);
            juego.managerGUI.setMensajeControl("Se pinchó!!!");
            if (juego.disco != null) juego.disco.desactivar();
            juego.cambiarEstado(new EstadoPinchado(juego));
        }
    }
}

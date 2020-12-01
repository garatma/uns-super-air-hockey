using System;
using UnityEngine;

public class EstadoGolJugador2 : EstadoGol
{
    public ControladorJuego juego;

    public EstadoGolJugador2(ControladorJuego ctrlJuego)
    {
        // Constructor
        this.juego = ctrlJuego;
    }

    public override void Ejecutar()
    {
        // Realiza la acción correspondiente
        base.ejecutarGol(juego);

        if (!typeof(EstadoFin).Equals(juego.estado.GetType()))
        {
            // Gol del Jugador2
            juego.resetearDisco(-3.2f);
            juego.cambiarEstado(new EstadoSacaJugador1(juego));
        }
    }
}

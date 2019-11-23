using System;
using UnityEngine;

public class EstadoGolJugador2 : EstadoGolAbstracto
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
        base.EjecutarGol(juego);

        if (!typeof(EstadoFin).Equals(juego.estado.GetType()))
        {
            // Gol del Jugador2
            juego.sacaJugador(1);
            juego.resetearDisco(-3.2f);
            juego.cambiarEstado(new EstadoSacaJugador1(juego));
        }
    }
}

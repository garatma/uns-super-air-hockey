using System;
using UnityEngine;

public class EstadoGolJugador1 : EstadoGolAbstracto
{
    public ControladorJuego juego;

    public EstadoGolJugador1(ControladorJuego ctrlJuego)
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
            // Gol del Jugador1
            juego.resetearDisco(3.2f);
            juego.cambiarEstado(new EstadoSacaJugador2(juego));
        }
    }
}

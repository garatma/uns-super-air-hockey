using System;

public class EstadoSacaJugador1 : EstadoAbstracto
{
    public ControladorJuego juego;

    public EstadoSacaJugador1(ControladorJuego ctrlJuego)
    {
        // Constructor
        this.juego = ctrlJuego;
    }

    public override void Ejecutar()
    {
        // No hace nada, solo esclarece el proceso de cambio de estados.
    }
}
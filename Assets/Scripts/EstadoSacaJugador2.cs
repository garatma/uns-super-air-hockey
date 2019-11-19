using System;

public class EstadoSacaJugador2 : EstadoAbstracto
{
    public ControladorJuego juego;

    public EstadoSacaJugador2(ControladorJuego ctrlJuego)
    {
        // Constructor
        this.juego = ctrlJuego;
    }

    public override void Ejecutar()
    {
        // No hace nada, solo esclarece el proceso de cambio de estados.
    }
}

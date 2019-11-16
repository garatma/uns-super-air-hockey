using System;

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
            // gol del Jugador2
            // estado = Estados.sacaJugador1;
            juego.cambiarEstado(new EstadoSacaJugador1(juego));
            juego.resetearDisco(-3.2f);
        }
    }
}
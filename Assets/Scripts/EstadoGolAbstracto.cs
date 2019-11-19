using System;

public abstract class EstadoGolAbstracto : EstadoAbstracto
{
    public void EjecutarGol(ControladorJuego juego)
    {
        // Realiza la acción correspondiente
        juego.managerGUI.setGoles(juego.golesJugador1, juego.golesJugador2);

        if (juego.golesJugador1 == 10 || juego.golesJugador2 == 10)
        {
            // Juego terminado
            juego.cambiarEstado(new EstadoFin(juego));
            juego.resetearDisco(0.0f);
        }
    }
}

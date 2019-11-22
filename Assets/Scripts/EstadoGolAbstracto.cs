using System;
using UnityEngine;

public abstract class EstadoGolAbstracto : EstadoAbstracto
{
    public void EjecutarGol(ControladorJuego juego)
    {
        Debug.Log("gol de algún jugador. cambiando labels");
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

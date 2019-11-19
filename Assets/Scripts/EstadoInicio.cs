using System;
using UnityEngine;

public class EstadoInicio : EstadoAbstracto
{
    public ControladorJuego juego;

    public EstadoInicio(ControladorJuego ctrlJuego)
	{
        // Constructor
        this.juego = ctrlJuego;
	}

    public override void Ejecutar()
    {
        // Realiza la acción correspondiente
        juego.managerGUI.setMensajeControl("");
        juego.resetearDisco(-3.2f);
        juego.golesJugador1 = 0;
        juego.golesJugador2 = 0;
        juego.managerGUI.setGoles(juego.golesJugador1, juego.golesJugador2);

        if (juego.jugadoresConectados() == 2)
        {
            juego.disco.activar();
            juego.cambiarEstado(new EstadoSacaJugador1(juego));
        }
    }
}

using System;
using UnityEngine;

public class EstadoInicio : Estado
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
        juego.managerGUI.setearMensajeControl("");
        juego.golesJugador1 = 0;
        juego.golesJugador2 = 0;
        juego.managerGUI.setearGoles(juego.golesJugador1, juego.golesJugador2);
        juego.resetearControlReinicio();

        if (juego.obtenerJugadoresConectados() == 2)
        {
            juego.resetearDisco(-3.2f);
            juego.disco.activar();
            juego.cambiarEstado(new EstadoSacaJugador1(juego));
        }
    }
}

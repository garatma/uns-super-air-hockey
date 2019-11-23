using System;
using UnityEngine;

public class EstadoPinchado : EstadoAbstracto
{
    public ControladorJuego juego;
    private bool inputLocal = false;

    public EstadoPinchado(ControladorJuego ctrlJuego)
    {
        // Constructor
        this.juego = ctrlJuego;
    }

    public override void Ejecutar()
    {
        // Realiza la acción correspondiente

		if (!inputLocal && base.inputReinicio())
		{
			juego.inputLocal();
			inputLocal = true;
			juego.managerGUI.setMensajeControl("Esperando reinicio del otro jugador");
		}
		if (juego.inputAmbosJugadores())
		{
			inputLocal = false;
			juego.resetInputs();
            juego.disco.activar();
            juego.managerGUI.setMensajeControl("");
            juego.resetearDisco(-3.2f);
            juego.cambiarEstado(new EstadoSacaJugador1(juego));
		}
    }
}

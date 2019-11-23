using System;
using UnityEngine;

public class EstadoEsperandoReinicio : EstadoAbstracto
{
    public ControladorJuego juego;
    private bool inputLocal = false;

    public EstadoEsperandoReinicio(ControladorJuego ctrlJuego)
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
			juego.cambiarEstado(new EstadoInicio(juego));
		}
    }
}

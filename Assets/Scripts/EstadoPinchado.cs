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
			juego.CmdHostReset();
			inputLocal = true;
		}
		if (juego.inputHost())
		{
			inputLocal = false;
			juego.resetearControlReinicio();
            juego.disco.activar();
            juego.managerGUI.setMensajeControl("");
            juego.resetearDisco(-3.2f);
            juego.cambiarEstado(new EstadoSacaJugador1(juego));
		}
    }
}

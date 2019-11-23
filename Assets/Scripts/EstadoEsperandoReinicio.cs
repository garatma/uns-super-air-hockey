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
			juego.CmdHostReset();
			inputLocal = true;
		}
		if (juego.inputHost())
		{
			inputLocal = false;
			juego.resetearControlReinicio();
			juego.cambiarEstado(new EstadoInicio(juego));
		}
    }
}

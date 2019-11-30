using System;
using UnityEngine;

public class EstadoEsperandoReinicio : Estado
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
		if (!inputLocal && base.chequearInputJugador())
		{
			juego.CmdHostReset();
			inputLocal = true;
		}
		if (juego.chequearInputHost())
		{
			inputLocal = false;
			juego.resetearControlReinicio();
			juego.cambiarEstado(new EstadoInicio(juego));
		}
    }
}

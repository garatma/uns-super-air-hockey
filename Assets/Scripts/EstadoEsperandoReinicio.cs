using System;
using UnityEngine;

public class EstadoEsperandoReinicio : EstadoAbstracto
{
    public ControladorJuego juego;

    public EstadoEsperandoReinicio(ControladorJuego ctrlJuego)
    {
        // Constructor
        this.juego = ctrlJuego;
    }

    public override void Ejecutar()
    {
        // Realiza la acción correspondiente
<<<<<<< Updated upstream
        juego.disco.desactivar();
        if (Input.GetAxis("Mouse ScrollWheel") != 0.0f ||
             Input.GetButton("Fire2") ||
             Input.GetButton("Fire1"))
            // estado = Estados.inicio;
            juego.cambiarEstado(new EstadoInicio(juego));
=======
		if (!inputLocal && juego.reiniciaronPartida())
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
>>>>>>> Stashed changes
    }
}
using System;
using UnityEngine;

public class EstadoEsperandoReinicio : Estado
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
		
		juego.disco.desactivar();

		if (juego.reiniciaronPartida())
		{
			juego.reinicioPartidaListo();
			juego.managerMenus.cambiarAPartida();
			juego.cambiarEstado(new EstadoInicio(juego));
		}
    }
}

using System;
using UnityEngine.SceneManagement;

public class EstadoFin : EstadoAbstracto
{
    public ControladorJuego juego;

    public EstadoFin(ControladorJuego ctrlJuego)
    {
        // Constructor
        this.juego = ctrlJuego;
    }

    public override void Ejecutar()
    {
		juego.disco.desactivar();
        // juego.cambiarEstado(new EstadoEsperandoReinicio(juego));
		
        // Realiza la acción correspondiente
        if (juego.golesJugador1 == 10)
			// juego.managerGUI.setMensajeControl("Ganó el jugador 1! Esperando que el host apriete algún botón para reiniciar.");
			SceneManager.LoadScene(2);
        else
            // juego.managerGUI.setMensajeControl("Ganó el jugador 2! Esperando que el host apriete algún botón para reiniciar.");
			SceneManager.LoadScene(3);
    }
}

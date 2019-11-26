using System;

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
<<<<<<< Updated upstream
        // Realiza la acción correspondiente
        if (juego.golesJugador1 == 10)
            juego.managerGUI.setMensajeControl("Ganó el jugador 1! Apriete algún botón para reiniciar.");
        else
            juego.managerGUI.setMensajeControl("Ganó el robot! Apriete algún botón para reiniciar.");
        // estado = Estados.esperandoReinicio;
        juego.cambiarEstado(new EstadoEsperandoReinicio(juego));
    }
}
=======
		juego.disco.desactivar();
        juego.cambiarVista(1);
		juego.cambiarEstado(new EstadoEsperandoReinicio(juego));

        // Realiza la acción correspondiente
        if (juego.golesJugador1 == 10)
			juego.managerGUI.setMensajeControl("Ganó el jugador 1! Esperando que el host apriete algún botón para reiniciar.");
		else
            juego.managerGUI.setMensajeControl("Ganó el jugador 2! Esperando que el host apriete algún botón para reiniciar.");
	}
}
>>>>>>> Stashed changes

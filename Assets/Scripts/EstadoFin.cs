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
        // Realiza la acción correspondiente
        if (juego.golesJugador1 == 10)
            juego.managerGUI.setMensajeControl("Ganó el jugador 1! Apriete algún botón para reiniciar.");
        else
            juego.managerGUI.setMensajeControl("Ganó el robot! Apriete algún botón para reiniciar.");
        // estado = Estados.esperandoReinicio;
        juego.cambiarEstado(new EstadoEsperandoReinicio(juego));
    }
}
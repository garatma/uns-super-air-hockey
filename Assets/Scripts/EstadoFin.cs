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
        // Realiza la acción correspondiente
        
        juego.disco.desactivar();
        // juego.cambiarVista(1);

        if (juego.golesJugador1 == 10)
            juego.managerGUI.setMensajeControl("Ganó el jugador 1! Esperando que el host apriete algún botón para reiniciar.");
        else
            juego.managerGUI.setMensajeControl("Ganó el jugador 2! Esperando que el host apriete algún botón para reiniciar.");

        juego.cambiarEstado(new EstadoEsperandoReinicio(juego));
    }
}

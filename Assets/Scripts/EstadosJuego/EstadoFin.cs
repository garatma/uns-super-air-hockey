using System;
using UnityEngine.SceneManagement;

public class EstadoFin : Estado
{
    public ControladorJuego juego;

    public EstadoFin(ControladorJuego ctrlJuego)
    {
        // Constructor
        this.juego = ctrlJuego;
    }

    public override void Ejecutar()
    {
        // Realiza la acciÃ³n correspondiente
        
        juego.disco.desactivar();

        if (juego.golesJugador1 == 10)
			juego.managerMenus.setearTextoFin("¡Ganó el jugador 1! Para reiniciar, el host debe apretar el botón");
        else if (juego.golesJugador2 == 10)
			juego.managerMenus.setearTextoFin("¡Ganó el jugador 2! Para reiniciar, el host debe apretar el botón");

		juego.reinicioPartidaListo();
		juego.managerMenus.cambiarAMenuReinicio();
        juego.cambiarEstado(new EstadoEsperandoReinicio(juego));
    }
}

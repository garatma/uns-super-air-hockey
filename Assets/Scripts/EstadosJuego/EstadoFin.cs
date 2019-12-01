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
        // Realiza la accion correspondiente
        
        juego.disco.desactivar();

        if (juego.golesJugador1 == 10)
        {
            juego.managerMenus.cambiarAMenuReinicioGJ1();
        }
        else if (juego.golesJugador2 == 10)
        {
            juego.managerMenus.cambiarAMenuReinicioGJ2();
        }

		juego.reinicioPartidaListo();
		juego.cambiarEstado(new EstadoEsperandoReinicio(juego));
    }
}

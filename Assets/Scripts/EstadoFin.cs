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
       
        // Realiza la acción correspondiente
        if (juego.golesJugador1 == 10)
			SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(3);
    }
}

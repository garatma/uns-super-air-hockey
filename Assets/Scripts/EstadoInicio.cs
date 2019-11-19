using System;

public class EstadoInicio : EstadoAbstracto
{
    public ControladorJuego juego;

    public EstadoInicio(ControladorJuego ctrlJuego)
	{
        // Constructor
        this.juego = ctrlJuego;
	}

    public override void Ejecutar()
    {
        // Realiza la acción correspondiente
        if (juego.jugadoresConectados() == 2)
        {
            juego.disco.activar();
            juego.managerGUI.setMensajeControl("");
            juego.resetearDisco(-3.2f);
            juego.golesJugador1 = 0;
            juego.golesJugador2 = 0;
            juego.managerGUI.setGoles(juego.golesJugador1, juego.golesJugador2);
            //estado = Estados.sacaJugador1;
            juego.cambiarEstado(new EstadoSacaJugador1(juego));
        }
    }
}

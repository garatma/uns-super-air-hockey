using System;
using UnityEngine;

public class EstadoEsperandoReinicio : EstadoAbstracto
{
    public ControladorJuego juego;
    private ingresoInput = false;

    public EstadoEsperandoReinicio(ControladorJuego ctrlJuego)
    {
        // Constructor
        this.juego = ctrlJuego;
    }

    // TODO: mejorar para que avise al otro jugador de que apretó para reiniciar.
    public override void Ejecutar()
    {
        // Realiza la acción correspondiente
        juego.disco.desactivar();
        if (!ingresoInput && Input.GetAxis("Mouse ScrollWheel") != 0.0f || Input.GetButton("Fire2") || Input.GetButton("Fire1"))
        {
            ingresoInput = true;
            juego.cambiarEstado(new EstadoInicio(juego));
        }
    }
}

using System;
using UnityEngine;

public class EstadoEsperandoReinicio : EstadoAbstracto
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
        if (Input.GetAxis("Mouse ScrollWheel") != 0.0f ||
             Input.GetButton("Fire2") ||
             Input.GetButton("Fire1"))
            // estado = Estados.inicio;
            juego.cambiarEstado(new EstadoInicio(juego));
    }
}
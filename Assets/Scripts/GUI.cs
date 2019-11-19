using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI : MonoBehaviour
{
    public UnityEngine.UI.Text[] golesGUI_Jugador1 = {null, null};
    public UnityEngine.UI.Text[] golesGUI_Jugador2 = {null, null};
    public UnityEngine.UI.Text mensaje_control;
    public UnityEngine.UI.Button botonModo;
    public UnityEngine.UI.Text textoBotonModo;

    public ControladorJuego juego;

    private bool VR = true;

    void Start()
    {
        botonModo.onClick.AddListener(oyente);
        //botonModo.interactable = false;
    }

    void oyente()
    {
        VR = !VR;
        textoBotonModo.text = VR ? "VR" : "Normal";
        juego.cambiarModo(VR);
    }

    public void setMensajeControl(string mensaje)
    {
        mensaje_control.text = mensaje;
    }

    public void setGoles(int golesJugador1, int golesJugador2)
    {
        golesGUI_Jugador1[0].text = golesJugador1.ToString();
        golesGUI_Jugador2[0].text = golesJugador1.ToString();
        golesGUI_Jugador1[1].text = golesJugador2.ToString();
        golesGUI_Jugador2[1].text = golesJugador2.ToString();
    }

    public void habilitacionBotonModo(bool habilitar)
    {
        botonModo.interactable = habilitar;
    }
}

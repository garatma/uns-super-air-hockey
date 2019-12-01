using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Menus : MonoBehaviour
{
	public GameObject MenuInicio;
	public GameObject MenuReinicioGJ1;
    public GameObject MenuReinicioGJ2;
    public GameObject MenuPinchado;
	public GameObject MenuControl;
	public GameObject Vista;
	public NetworkManager MenuNetwork;

    public UnityEngine.UI.Text textoFin;

    public ControladorJuego juego;

    public UnityEngine.UI.Button botonInicio;
    public UnityEngine.UI.Button botonReinicio1;
    public UnityEngine.UI.Button botonReinicio2;
    public UnityEngine.UI.Button botonPinchado;

	public void setearMenus(bool inicio, 
							bool reinicioGJ1,
                            bool reinicioGJ2,
                            bool pinchado,
							bool control,
							bool vista,
							bool network)
	{
        MenuInicio.SetActive(inicio);
        MenuReinicioGJ1.SetActive(reinicioGJ1);
        MenuReinicioGJ2.SetActive(reinicioGJ2);
        MenuPinchado.SetActive(pinchado);
        MenuControl.SetActive(control);
        Vista.SetActive(vista);
		MenuNetwork.GetComponent<NetworkManagerHUD>().enabled = network;
	}

    void Start()
    {
		setearMenus(true, false, false, false, false, false, false);

        botonInicio.onClick.AddListener(cambiarMenuInicio);
        botonReinicio1.onClick.AddListener(cambiarMenuReinicio);
        botonReinicio2.onClick.AddListener(cambiarMenuReinicio);
        botonPinchado.onClick.AddListener(cambiarMenuPinchado);

        // botonPinchado.interactable = false;
        // botonReinicio.interactable = false;

		// CmdHabilitarBotonesServidor();
    }

    void Update() { }

	// en el servidor estos botones tienen que estar activos
	// en los clientes no
	// [Command]
	public void CmdHabilitarBotonesServidor()
	{
        botonPinchado.interactable = true;
        botonReinicio1.interactable = true;
        botonReinicio2.interactable = true;

    }

    // las siguientes tres funciones se ejecutan al clickear los botones
    // botonInicio, botonReinicio y botonPinchado, respectivamente
    public void cambiarMenuInicio()
	{
		setearMenus(false, false, false, false, true, true, true);
	}

	public void cambiarMenuReinicio()
	{
		setearMenus(false, false, false, false, true, true, false);
		juego.CmdReiniciarPartida();
	}

	public void cambiarMenuPinchado()
	{
		setearMenus(false, false, false, false, true, true, false);
		juego.CmdReiniciarPunto();
	}

	// ------------------------------------------------------------------
	
	public void cambiarAMenuReinicioGJ1()
	{
		setearMenus(false, true, false, false, false, false, false);
	}
    public void cambiarAMenuReinicioGJ2()
    {
        setearMenus(false, false, true, false, false, false, false);
    }

    public void cambiarAPartida()
	{
		setearMenus(false, false, false, false, true, true, false);
	}

	public void cambiarAMenuPinchado()
	{
		setearMenus(false, false, false, true, true, true, false);
	}

	public void mostrarInterfazNetwork(bool visible)
	{
		MenuNetwork.GetComponent<NetworkManagerHUD>().enabled = visible;
	}

    public void setearTextoFin(string mensaje)
    {
        textoFin.text = mensaje;
    }
}

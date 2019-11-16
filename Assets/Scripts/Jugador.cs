using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Jugador : NetworkBehaviour
{
	public ControladorJuego juego;
    private Vector3 direccion;
    private float velocidad = 0.2f;

	public GameObject zonaJugador1;
	public GameObject zonaJugador2;
	private GameObject zona;
	private float anchoZona, altoZona;
	private float anchoVentana, altoVentana;

	public Material materialJugador2;

	public int sentidoMovimiento = 1;

	void Start()
	{
		anchoVentana = Screen.width;
		altoVentana = Screen.height;

		juego.jugadorConectado(this);

		int jugador = juego.soyJugador();

		if ( jugador == 2 )
		{
			GetComponent<Renderer>().material = materialJugador2;
			zona = zonaJugador2;
		}
		else
		{
			zona = zonaJugador1;
		}

		anchoZona = zona.transform.localScale.x * 1.0f;
		altoZona = zona.transform.localScale.z * 1.0f;
	}

	void Update()
	{
		if (hasAuthority)
		{
			Vector3 posicionActual = Vector3.one;
			posicionActual.x = (anchoZona/anchoVentana) * Input.mousePosition.x;
			posicionActual.z = (altoZona/altoVentana) * Input.mousePosition.y;

			posicionActual.x += zona.transform.position.x - (anchoZona/2.0f);
			posicionActual.z += zona.transform.position.z - (altoZona/2.0f);

			posicionActual.y = 0.32f;
			transform.position = posicionActual;
		}
	}

	// public void move(float movx, float movz)
	// {
	// 	direccion = new Vector3(movx, 0.0f, movz);
	// 	GetComponent<Rigidbody>().position += direccion * velocidad;
	// 	GetComponent<Rigidbody>().position = new Vector3(
	// 			Mathf.Clamp(GetComponent<Rigidbody>().position.x,-3.5f,3.5f),
	// 			0.0f,
    //             Mathf.Clamp(GetComponent<Rigidbody>().position.z, -6.4f, 6.4f));
	// }
	//
    // public void sacar(Vector2 dir)
    // {
    //     direccion = new Vector3(dir.x, 0.0f, dir.y);
    //     GetComponent<Rigidbody>().position += direccion * velocidad;
    // }
	//
    // public Vector2 getPosicion()
    // {
    //     return new Vector2(GetComponent<Rigidbody>().position.x, GetComponent<Rigidbody>().position.z);
    // }
	//
    // public void setPosicion(float x, float y, float z)
    // {
    //     GetComponent<Rigidbody>().position = new Vector3(x, y, z);
    // }
}

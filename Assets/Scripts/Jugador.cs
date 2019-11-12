using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Jugador : NetworkBehaviour
{
	public ControladorJuego juego;
    private Vector3 movimiento;
    private float reaccion = 0.2f;

	public GameObject zonaJugador1;
	public GameObject zonaJugador2;
	private GameObject zona;
	private float targetWidth, targetHeight;
	private float width, height;

	public Material materialJugador2;

	public int sentidoMovimiento = 1;

	void Start()
	{
		width = Screen.width;
		height = Screen.height;

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

		targetWidth = zona.transform.localScale.x * 1.0f;
		targetHeight = zona.transform.localScale.z * 1.0f;
	}

	void Update()
	{
		if (hasAuthority)
		{
			Vector3 targetPos = Vector3.one;
			targetPos.x = (targetWidth/width) * Input.mousePosition.x;
			targetPos.z = (targetHeight/height) * Input.mousePosition.y;

			targetPos.x += zona.transform.position.x - (targetWidth/2.0f);
			targetPos.z += zona.transform.position.z - (targetHeight/2.0f);

			targetPos.y = 0.32f;
			transform.position = targetPos;
		}
	}

	// public void move(float movx, float movz)
	// {
	// 	movimiento = new Vector3(movx, 0.0f, movz);
	// 	GetComponent<Rigidbody>().position += movimiento * reaccion;
	// 	GetComponent<Rigidbody>().position = new Vector3(
	// 			Mathf.Clamp(GetComponent<Rigidbody>().position.x,-3.5f,3.5f),
	// 			0.0f,
    //             Mathf.Clamp(GetComponent<Rigidbody>().position.z, -6.4f, 6.4f));
	// }
	//
    // public void sacar(Vector2 dir)
    // {
    //     movimiento = new Vector3(dir.x, 0.0f, dir.y);
    //     GetComponent<Rigidbody>().position += movimiento * reaccion;
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

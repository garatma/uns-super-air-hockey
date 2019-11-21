using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Jugador : NetworkBehaviour
{
	public ControladorJuego juego;

	private GameObject area;
	private float anchoArea, altoArea;
	private float anchoVentana, altoVentana;

	private bool areaAsignada = false;

	private float sentidoMovimiento = 1.0f;

	void Start()
	{
		anchoVentana = Screen.width;
		altoVentana = Screen.height;
	}

	void FixedUpdate()
	{
		if (hasAuthority && areaAsignada)
		{
			Vector3 posicionActual = Vector3.one;
			posicionActual.x = (anchoArea/anchoVentana) * Input.mousePosition.x;
			posicionActual.z = (altoArea/altoVentana) * Input.mousePosition.y;

			posicionActual.x += area.transform.position.x - (anchoArea/2.0f);
			posicionActual.z += area.transform.position.z - (altoArea/2.0f);

			posicionActual.y = 0.32f;
			transform.position = posicionActual;
		}
	}

	public bool tengoAutoridad()
	{
		return hasAuthority;
	}

	public void asignarJuego(ControladorJuego nuevoJuego)
	{
		juego = nuevoJuego;
	}

	public void asignarArea(GameObject nuevaArea)
	{
		area = nuevaArea;
		anchoArea = area.transform.localScale.x * 1.0f;
		altoArea = area.transform.localScale.z * 1.0f;
		areaAsignada = true;
	}

	public void asignarMaterial(Material nuevoMaterial)
	{
		GetComponent<Renderer>().material = nuevoMaterial;
	}

	public void asignarSentidoMovimiento(float nuevoSentidoMovimiento)
	{
		sentidoMovimiento = nuevoSentidoMovimiento;
	}
}

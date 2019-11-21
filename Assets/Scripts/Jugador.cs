using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Jugador : NetworkBehaviour
{
	public GameObject area;
	private float anchoArea, altoArea;
	private float anchoVentana, altoVentana;

	void Start()
	{
		anchoArea = area.transform.localScale.x * 1.0f;
		altoArea = area.transform.localScale.z * 1.0f;

		anchoVentana = Screen.width;
		altoVentana = Screen.height;
	}

	void FixedUpdate()
	{
		if (hasAuthority)
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
}

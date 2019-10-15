using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckController : MonoBehaviour
{
	public GameController juego;

	private Vector3 movimiento;

	void OnTriggerEnter(Collider colision)
	{
		GameObject obj = colision.gameObject;

		if ( colision.gameObject.tag == "Player" )
		{
			juego.estado = GameController.Estados.jugando;

			Vector2 posDisco = new Vector2(GetComponent<Rigidbody>().position.x, GetComponent<Rigidbody>().position.z);
			Vector2 posPlayer = new Vector2(obj.GetComponent<Rigidbody>().position.x, obj.GetComponent<Rigidbody>().position.z);
			Vector2 dirGolpe = posDisco - posPlayer;

			movimiento.z = -movimiento.z;
			movimiento += new Vector3(dirGolpe.x, 0.0f, dirGolpe.y);
			movimiento = new Vector3(
					Mathf.Clamp(movimiento.x, -1.25f, 1.25f),
					0.0f,
					Mathf.Clamp(movimiento.z, -1.25f, 1.25f)
			);
		}
		else if ( colision.gameObject.tag == "Costado" )
			movimiento.x = -movimiento.x;
		else if ( colision.gameObject.tag == "Fondo" )
			movimiento.z = -movimiento.z;
		else if ( colision.gameObject.tag == "ArcoPlayer1" )
		{
			movimiento = new Vector3(0.0f, 0.0f, 0.0f);
			juego.golesPlayer2++;
			juego.estado = GameController.Estados.golPlayer2;
		}
		else if ( colision.gameObject.tag == "ArcoPlayer2" )
		{
			movimiento = new Vector3(0.0f, 0.0f, 0.0f);
			juego.golesPlayer1++;
			juego.estado = GameController.Estados.golPlayer1;
		}
	}

	void Update()
	{
		GetComponent<Rigidbody>().position += movimiento * juego.reaccionDisco;
		GetComponent<Rigidbody>().position = new Vector3(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, -50f, 50f),
			0.4f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, -100f, 100f)
		);
	}
}

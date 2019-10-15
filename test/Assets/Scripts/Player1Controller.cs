using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
	public GameController juego;

	private Vector3 movimiento;

    void FixedUpdate()
	{

        float movH = 0;
        
        if ( Input.GetButton("Fire1") )
			movH = -1;

        else if (Input.GetButton("Fire2"))
            movH = 1;

        switch (juego.estado)
		{
			case GameController.Estados.sacaPlayer1:
				move(movH);
				if (Input.GetAxis("Mouse ScrollWheel") != 0.0f )
					juego.estado = GameController.Estados.sacandoPlayer1;
				break;

			case GameController.Estados.sacandoPlayer1:
				Vector2 posDisco = new Vector2(GetComponent<Rigidbody>().position.x,
											  GetComponent<Rigidbody>().position.z);

				Vector2 posPlayer1 = new Vector2(juego.disco.GetComponent<Rigidbody>().position.x,
												 juego.disco.GetComponent<Rigidbody>().position.z);

				Vector2 dirGolpe= posPlayer1 - posDisco;

				movimiento = new Vector3(dirGolpe.x, 0.0f, dirGolpe.y);
				GetComponent<Rigidbody>().position += movimiento * juego.reaccionPlayer1;
				break;

			case GameController.Estados.jugando:
				move(movH);
				break;
			
			case GameController.Estados.esperandoReinicio:
				if ( juego.golesPlayer1 == 1 && (Input.GetAxis("Mouse ScrollWheel") != 0.0f || Input.GetButton("Fire2") || Input.GetButton("Fire1")) )
					juego.estado = GameController.Estados.inicio;
				break;
		}
	}

	void move(float movH)
	{
		movimiento = new Vector3(movH, 0.0f, 0.0f);
		GetComponent<Rigidbody>().position += movimiento * juego.reaccionPlayer1;
		GetComponent<Rigidbody>().position = new Vector3(
				Mathf.Clamp(GetComponent<Rigidbody>().position.x,-3.3f,3.3f),
				0.0f,
				-6f);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public GameController juego;

    private Vector3 movimiento;

    private float movIzquierda_Derecha = 1;

    void FixedUpdate()
    {
        float movH = movIzquierda_Derecha; 
        
        switch(juego.estado)
        {
            case GameController.Estados.sacaPlayer2:
                move(movH);
				if (Input.GetAxis("Mouse ScrollWheel") != 0.0f )
                    juego.estado = GameController.Estados.sacandoPlayer2;
                break;

            case GameController.Estados.sacandoPlayer2:
                Vector2 posDisco = new Vector2(GetComponent<Rigidbody>().position.x,
                                              GetComponent<Rigidbody>().position.z);

                Vector2 posPlayer2 = new Vector2(juego.disco.GetComponent<Rigidbody>().position.x,
                                                 juego.disco.GetComponent<Rigidbody>().position.z);

                Vector2 dirGolpe= posPlayer2 - posDisco;

                movimiento = new Vector3(dirGolpe.x, 0.0f, dirGolpe.y);
                GetComponent<Rigidbody>().position += movimiento * juego.reaccionPlayer2;
                break;

            case GameController.Estados.jugando:
                move(movH);
                break;

			case GameController.Estados.esperandoReinicio:
				if ( juego.golesPlayer2 == 1 && ( Input.GetAxis("Mouse ScrollWheel") != 0.0f || Input.GetButton("Fire2") || Input.GetButton("Fire1")) )
                	juego.estado = GameController.Estados.inicio;
                break;
        }
    }

    void move(float movH)
    {
        movimiento = new Vector3(movH, 0.0f, 0.0f);
        GetComponent<Rigidbody>().position += movimiento * juego.reaccionPlayer2;
        GetComponent<Rigidbody>().position = new Vector3(
                Mathf.Clamp(GetComponent<Rigidbody>().position.x,-3.3f,3.3f),
                0.0f,
                6f);
    }

    void OnTriggerEnter(Collider colision)
    {
        GameObject obj = colision.gameObject;

        if (colision.gameObject.tag == "Costado")
            movIzquierda_Derecha *= -1;
    }

}

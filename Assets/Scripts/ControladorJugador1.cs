using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorJugador1 : MonoBehaviour
{
	public ControladorJuego juego;

    private Vector3 movimiento;

    private float reaccion = 0.2f;

	public void move(float movx, float movz)
	{
		movimiento = new Vector3(movx, 0.0f, movz);
		GetComponent<Rigidbody>().position += movimiento * reaccion;
		GetComponent<Rigidbody>().position = new Vector3(
				Mathf.Clamp(GetComponent<Rigidbody>().position.x,-3.3f,3.3f),
				0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, -6f, -3f));
	}

    public void sacar(Vector2 dir)
    {
        movimiento = new Vector3(dir.x, 0.0f, dir.y);
        GetComponent<Rigidbody>().position += movimiento * reaccion;
    }

    public Vector2 getPosicion()
    {
        return new Vector2(GetComponent<Rigidbody>().position.x, GetComponent<Rigidbody>().position.z);
    }

    public void setPosicion(float x, float y, float z)
    {
        GetComponent<Rigidbody>().position = new Vector3(x, y, z);
    }
}

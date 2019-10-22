using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
	public ControladorJuego juego;

    private Vector3 movimiento;

    private float reaccion = 0.2f;

	private float lugar = 1.0f;

	void Start()
	{
		juego.jugadorConectado(this);
	}

	void Update()
	{
        Vector2 mouse = new Vector2(Input.GetAxis("Mouse X")/2.0f, Input.GetAxis("Mouse Y")/2.0f);
		move(mouse.x, mouse.y);
	}

	public void setLugar(float lug)
	{
		lugar = lug;
	}

	public void move(float movx, float movz)
	{
		movimiento = new Vector3(movx, 0.0f, movz);
		GetComponent<Rigidbody>().position += movimiento * reaccion;
		GetComponent<Rigidbody>().position = new Vector3(
				Mathf.Clamp(GetComponent<Rigidbody>().position.x,-3.5f,3.5f),
				0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, -6.4f, 6.4f));
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

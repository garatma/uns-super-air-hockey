using System;
using UnityEngine;

public abstract class EstadoAbstracto : MonoBehaviour
{
    public abstract void Ejecutar();

	public bool inputReinicio()
	{
		return Input.GetAxis("Mouse ScrollWheel") != 0.0f || 
			   Input.GetButton("Fire2") || 
			   Input.GetButton("Fire1");
	}
}

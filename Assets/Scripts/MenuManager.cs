using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
	public Transform atras;
	public Transform adelante;
	public GameObject CanvasMenuPrincipal;
	public GameObject CanvasMenuFin;
	public GameObject CanvasVista;
    public ControladorJuego juego;


    // Start is called before the first frame update
    void Start()
    {
        CanvasMenuFin.SetActive(false);
        CanvasMenuPrincipal.SetActive(true);
        CanvasVista.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void jugar()
    {
    	CanvasVista.SetActive(true);
    	CanvasMenuPrincipal.SetActive(false);
    	CanvasMenuFin.SetActive(false);
    }

    public void irMenuPrincipal()
    {
       
    }

    public void reiniciar()
    {
    	// juego.reiniciar();
    	jugar();
    }
}

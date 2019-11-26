using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuFinJvJ : MonoBehaviour
{
    public void jugar()
    {
        SceneManager.LoadScene(1);

    }

    public void irMenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
}

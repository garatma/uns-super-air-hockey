using UnityEngine;
using Mirror;

// Custom NetworkManager that simply assigns the correct racket positions when
// spawning players. The built in RoundRobin spawn method wouldn't work after
// someone reconnects (both players would be on the same side).
public class Server : NetworkManager
{
    public Transform spawnJugador1;
    public Transform spawnJugador2;
    public ControladorJuego juego;
    public GameObject discoSrc;
    private GameObject disco = null;
    private GameObject jugador1 = null;
    private GameObject jugador2 = null;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // add player at correct spawn position


        // NOTA: NO CAMBIAR EL NOMBRE DE numPlayers, ES UN ATRIBUTO DE NetworkManager!!!


        Transform start = numPlayers == 0 ? spawnJugador1 : spawnJugador2;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        // TODO: castear disco y jugador de GameObject a Disco y Jugador, respectivamente
        if (numPlayers == 1)
            jugador1 = player;
        else if (numPlayers == 2)
            jugador2 = player;
        Jugador jugadorAux = player.GetComponent<Jugador>();
        Debug.Log(jugadorAux);
        juego.nuevoJugador(jugadorAux, numPlayers);

        // spawn ball if two players
        if (numPlayers == 2)
        {
            disco = Instantiate(discoSrc);
            Disco discoAUX = disco.GetComponent<Disco>();
            Debug.Log(discoAUX);
            NetworkServer.Spawn(disco);
            discoAUX.asignarJuego(juego);
            juego.asignarDisco(discoAUX);
        }

    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        // destroy ball
        if (disco != null)
            NetworkServer.Destroy(disco);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);

        juego.eliminarDisco();
    }

    // TODO: agregar OnPlayerDisconnect o algo así
    /*
    public override void OnServerRemovePlayer(NetworkConnection conn)
    {
        Jugador jugadorAux;
        int numeroJugador;
        if (jugador1 == null)
        {
            jugadorAux = jugador1;
            numeroJugador = 1;
        }
        else if (jugador2 == null)
        {
            jugadorAux = jugador2;
            numeroJugador = 2;
        }

        NetworkServer.Destroy(jugadorAux);

        // call base functionality (actually destroys the player)
        base.OnServerDisconnect(conn);

        juego.eliminarJugador(numeroJugador);
    }
    */
}

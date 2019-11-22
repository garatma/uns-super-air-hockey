using UnityEngine;
using Mirror;

// Custom NetworkManager that simply assigns the correct racket positions when
// spawning players. The built in RoundRobin spawn method wouldn't work after
// someone reconnects (both players would be on the same side).
public class Server : NetworkManager
{
    public ControladorJuego juego;
    public GameObject jugador1;
    public GameObject jugador2;
    public Transform spawnJugador1;
    public Transform spawnJugador2;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        // NOTA: NO CAMBIAR EL NOMBRE DE numPlayers, ES UN ATRIBUTO DE NetworkManager!!!


        Transform start = numPlayers == 0 ? spawnJugador1 : spawnJugador2;
        GameObject playerASpawnear;
        playerASpawnear = numPlayers == 0 ? jugador1 : jugador2;
        GameObject player = Instantiate(playerASpawnear, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        int golesJugador1 = juego.getGolesJugador1(),
            golesJugador2 = juego.getGolesJugador2(),
            quienSaca = juego.getJugadorQueSaca();
        juego.RpcNuevoJugador(numPlayers, golesJugador1, golesJugador2, quienSaca);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        juego.RpcReiniciarTodo();
        base.OnServerDisconnect(conn);
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

        base.OnServerDisconnect(conn);

        juego.eliminarJugador(numeroJugador);
    }
    */
}

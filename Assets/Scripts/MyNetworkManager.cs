using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class MyNetworkManager : NetworkManager
{

    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;
    public Transform enemySpawnPoint;


    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {

        Transform startPoint;

        if(numPlayers == 0)
        {
            startPoint = player1SpawnPoint;
            Invoke("SpawnEnemy",5);
        }
        else
        {
            startPoint = player2SpawnPoint;
            InvokeRepeating("SpawnEnemy" , 5.0f, 5.0f);
        }

        GameObject player = 
            Instantiate(playerPrefab, startPoint.position, startPoint.rotation); 

        NetworkServer.AddPlayerForConnection(conn, player);

    }


    public override void OnServerSceneChanged(string sceneName)
    {
        player1SpawnPoint = GameObject.FindGameObjectWithTag("P1_Spawn").transform;
        player2SpawnPoint = GameObject.FindGameObjectWithTag("P2_Spawn").transform;
    }
    void SpawnEnemy()
    {
        if(enemySpawnPoint != null)
        {
            GameObject new_enemy = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "EnemyFollower"), enemySpawnPoint.position, enemySpawnPoint.rotation);

            NetworkServer.Spawn(new_enemy);
        }
    }
    
}

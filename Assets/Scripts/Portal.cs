using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Portal : NetworkBehaviour
{
    MyNetworkManager manager;
    public string sceneName;
    
   private void Start()
    {
        manager = GameObject.Find("NetworkController").GetComponent<MyNetworkManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            manager.ServerChangeScene(sceneName);
        }
    }
}

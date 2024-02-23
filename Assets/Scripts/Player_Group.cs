using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player_Group : NetworkBehaviour
{
    public GroupHUD hud;
    [SyncVar] public Player_Group friendPlayer;
    Player_Group closePlayer;

    public GroupEvent OnPlayerJoin;
    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<GroupHUD>();
        OnPlayerJoin.AddListener(JoinGroup);
    }

    private void Update()
    {
        if(isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.G) && hud.message.activeSelf)
            {
                InviteToGroup();
            }
        }
    }

    [Command]
    void InviteToGroup()
    {
        SendGroupInvite(closePlayer.GetComponent<NetworkIdentity>().connectionToClient);
    }

    [TargetRpc]
    void SendGroupInvite(NetworkConnection target)
    {
        hud.ReceiveInvite(this, closePlayer);
    }

    [Command(requiresAuthority = false)]
    public void JoinGroup(Player_Group invitingPlayer)
    {
        UpdateGroup(invitingPlayer);
    }

    [ClientRpc]
    public void UpdateGroup(Player_Group invitingPlayer)
    {
        friendPlayer = invitingPlayer;
        if(isLocalPlayer)
        {
            friendPlayer.GetComponent<Player>().OnHPChanged.AddListener(hud.UpdateHPBar);
            hud.groupPanel.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player && collision.collider != player.weaponCollider)
        {
            if(friendPlayer == null)
            {
                closePlayer = player.GetComponent<Player_Group>();
                hud.message.SetActive(true);
            }
            else
            {
                hud.message.SetActive(false);
            }
        }
    }
    
     private void OnCollisionExit2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player && collision.collider != player.weaponCollider)
        {
            hud.message.SetActive(false);
        }
    }
}

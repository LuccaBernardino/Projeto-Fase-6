using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class GroupHUD : NetworkBehaviour
{
    public GameObject message;
    public GameObject groupInvitingPanel;

    Player_Group myPlayer;
    Player_Group friendPlayer;
    public Image groupPlayerHPBar;
    public GameObject groupPanel;

    public void ReceiveInvite(Player_Group new_myPlayer, Player_Group new_friendPlayer)
    {
        groupInvitingPanel.SetActive(true);
        myPlayer = new_myPlayer;
        friendPlayer = new_friendPlayer;
    }

    public void AcceptInvite()
    {
        groupInvitingPanel.SetActive(false);
        myPlayer.OnPlayerJoin.Invoke(friendPlayer);
        friendPlayer.OnPlayerJoin.Invoke(myPlayer);
    }

    public void RefuseInvite()
    {
        myPlayer = null;
        friendPlayer = null;
        groupInvitingPanel.SetActive(false);
    }

    public void UpdateHPBar(float value)
    {
        groupPlayerHPBar.fillAmount = value;
    }
}


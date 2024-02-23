using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsHUD : MonoBehaviour
{
   public Image playerHPBar;

    public void UpdateHPBar(float value)
    {
        playerHPBar.fillAmount = value;
    }
}

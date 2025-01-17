using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;
    public GameObject Player;
    private GameObject lastTeleportPoint;
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }
        else{
            Instance = this;
        }
    }
    
    public void DisableTeleportPoint(GameObject teleportPoint)
    {
        if(lastTeleportPoint !=null){
            lastTeleportPoint.SetActive(true);
        }

        teleportPoint.SetActive(false);
        lastTeleportPoint = teleportPoint;
    }
}

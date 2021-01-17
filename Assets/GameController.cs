using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance = null;

    public static GameController Instance { get { 
            if(instance == null)
            {
                instance = new GameController();
            }
            return instance;
        } }

    private void Awake()
    {
        Debug.Log(SystemInfo.deviceUniqueIdentifier);
    }
 

}

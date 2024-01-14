using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager not instantiated");
            return _instance;
        }
    }

    public void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        
    }
    
}

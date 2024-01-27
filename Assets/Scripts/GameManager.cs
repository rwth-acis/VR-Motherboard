using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private GameObject cpu;
    [SerializeField] private GameObject ram1;
    [SerializeField] private GameObject ram2;
    [SerializeField] private GameObject cpuFan;
    [SerializeField] private GameObject gpu;
    

    Vector3 cpuPos;
    Vector3 ram1Pos;
    Vector3 ram2Pos;
    Vector3 cpuFanPos;
    Vector3 gpuPos;
    
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
    private void Start() {
        cpuPos = cpu.transform.position;
        ram1Pos = ram1.transform.position;
        ram2Pos = ram2.transform.position;
        cpuFanPos = cpuFan.transform.position;
        gpuPos = gpu.transform.position;
        
    }

    public void ResetMotherboard()
    {
        cpu.transform.position = cpuPos;
        ram1.transform.position = ram1Pos;
        ram2.transform.position  =  ram2Pos;
        cpuFan.transform.position = cpuFanPos;
        gpu.transform.position = gpuPos;
    }

}

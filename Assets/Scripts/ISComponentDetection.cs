using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Scripting;
using UnityEngine.XR.Content.Interaction;

public class ISComponentDetection : MonoBehaviour
{

    [SerializeField] private Key gpuKey;
    [SerializeField] private Key ramKey;
    [SerializeField] private Key cpuKey;
    [SerializeField] private Key cpuFanKey;
    private Component currentComponent = Component.NONE;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI confirmText;

    [SerializeField] private Transform gpuTutorial;
    [SerializeField] private Transform cpuTutorial;
    [SerializeField] private Transform ramTutorial;

    public void TPToComponentTutorial() {
        if (currentComponent == Component.GPU) {
            player.transform.position = gpuTutorial.position;
        }
        else if (currentComponent == Component.CPU) {
            player.transform.position = cpuTutorial.position;
        }
        else if (currentComponent == Component.RAM) {
            player.transform.position = ramTutorial.position;
        }
        else if (currentComponent == Component.FAN) {
            player.transform.position = cpuTutorial.position;
        }
    }

    private void OnTriggerEnter(Collider other) {
        bool hasKey = other.gameObject.GetComponent<Keychain>();

        if (!hasKey) {
            Debug.Log("No Keychain");
            return;
        }


        if (currentComponent != Component.NONE) // One Component is already displaying
            return;

        bool isKey(Key key) => other.gameObject.GetComponent<Keychain>().Contains(key);

        if (isKey(cpuKey))
            currentComponent = Component.CPU;
        else if (isKey(gpuKey))
            currentComponent = Component.GPU;
        else if (isKey(cpuFanKey))
            currentComponent = Component.FAN;
        else if (isKey(ramKey))
            currentComponent = Component.RAM;

        UpdateSelectMenuText();
        UpdateQuiz();
    }



    private void OnTriggerExit(Collider other) {
        if (!other.gameObject.GetComponent<Keychain>())
            return;

        currentComponent = Component.NONE;
        UpdateSelectMenuText();

        UpdateQuiz();
    }

    private void UpdateQuiz() => GameObject.Find("Monitor").GetComponent<Monitor>().OnChangeComponent(currentComponent);

    private void UpdateSelectMenuText() {
        if (currentComponent == Component.NONE) {
            confirmText.text = "Drag a component\n into the beam";
            return;
        }

        confirmText.text = "Learn more about\n the " + currentComponent.Name + "!";
    }
}

public class Component
{
    public string Name { get; set; }
    public Quiz Quiz { get; set; }

    public Component(string name, Quiz quiz) { 
        Name = name; 
        Quiz = quiz;
    }

    public static readonly Component GPU = new Component("GPU", Quiz.GPU);
    public static readonly Component CPU = new Component("CPU", Quiz.CPU);
    public static readonly Component RAM = new Component("RAM", Quiz.RAM);
    public static readonly Component FAN = new Component("FAN", Quiz.FAN);
    public static readonly Component NONE = new Component("NONE", Quiz.DEFAULT);

    public static readonly List<Component> VALUES = new List<Component> { CPU, GPU, RAM, FAN };
}


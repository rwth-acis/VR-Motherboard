using System.Collections;
using System.Collections.Generic;
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
    private string currentComponent = "one";

    [SerializeField] private TextMeshProUGUI confirmText;

    private void OnTriggerEnter(Collider other) {
        if (!other.gameObject.GetComponent<Keychain>()) {
            Debug.Log("No Keychain");
            return;
        }
        if (!currentComponent.Equals("none")) // One Component is already displaying
            return;
        if (other.gameObject.GetComponent<Keychain>().Contains(cpuKey))
            currentComponent = "cpu";
        else if (other.gameObject.GetComponent<Keychain>().Contains(gpuKey))
            currentComponent = "gpu";
        else if (other.gameObject.GetComponent<Keychain>().Contains(cpuFanKey))
            currentComponent = "cpuFan";
        else if (other.gameObject.GetComponent<Keychain>().Contains(ramKey))
            currentComponent = "ram";
        UpdateSelectMenuText();
    }
    private void OnTriggerExit(Collider other) {
        if (!other.gameObject.GetComponent<Keychain>())
            return;
        currentComponent = "none";
        UpdateSelectMenuText();
    }

    private void UpdateSelectMenuText() {
        if (currentComponent.Equals("none")) {
            confirmText.text = "Drag a component\n into the beam";
            return;
        }
        confirmText.text = "Learn more about\n the " + CurCompInText() + "!";
    }

    private string CurCompInText() {
        switch (currentComponent) {
            case "gpu":
                return "GPU";
            case "cpu":
                return "CPU";
            case "cpuFan":
                return "CPU fan";
            case "ram":
                return "RAM";
            default:
                return "Error";
        }
    }
}

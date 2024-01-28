using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textspeed;
    private int index;
    ISPresentItemScript script;
    public i5.VirtualAgents.Examples.ControllerScript myScript;
    public Button button;
    // Start is called before the first frame update
    [SerializeField] private bool repeat = false;
    void Start()
    {
        textComponent.text = string.Empty;
        startDialogue();
        Button btn = button.GetComponent<Button>();
         btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick() // display next line if exists, else deactivate component
     {
        if (textComponent.text == lines[index])
        {
            nextline();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    void startDialogue()
    {
        index = 0;
        StartCoroutine(Typeline());
    }
    IEnumerator Typeline()
    {
        foreach(char c in lines[index])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textspeed);
        }
    }
    void nextline()
    {
        if(index< lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(Typeline());

        }
        else
        {
            if (repeat) { // repeat courses
                index = 0;
                textComponent.text = string.Empty;
                StartCoroutine(Typeline());
            }
            else {
                gameObject.SetActive(false);
                myScript.startItemPickup = true;
            }
        }
    }
}

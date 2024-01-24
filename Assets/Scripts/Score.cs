using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Color correctColor = Color.green;
    public Color incorrectColor = Color.red;
    public Color pendingColor = Color.white;

    private int score = 0;
    private int maxScore = 100;

    private const float width = 180f;
    private const float height = 100f;

    public Color backgroundColor = new Color(0f, 0f, 0f, 0.5f);

    private Dictionary<Question, GameObject> questions = new Dictionary<Question, GameObject>();


    void Start()
    {
        CreateScoreCard();
    }

    void CreateScoreCard()
    {
        float i = 0f;
        float spacing = width / Component.VALUES.Count;

        foreach (var component in Component.VALUES)
        {
            float x = (i * spacing) + (spacing / 2f);
            float y = 30f;

            CreateText(component.Name, x, 0f);

            foreach (var question in component.Quiz.Questions)
            {
                var cube = CreateCube(x, y, pendingColor);
                questions.Add(question, cube);

                y += 15f;
            }

            i++;
        }
    }

    void CreateText(string text, float x, float y)
    {
        GameObject textObject = new GameObject(text);
        TextMeshProUGUI textMeshPro = textObject.AddComponent<TextMeshProUGUI>();
        textObject.transform.SetParent(transform);

        textMeshPro.text = text;
        textMeshPro.fontSize = 10;
        textMeshPro.color = Color.white;

        Vector3 vec = transform.TransformPoint(Vec(x, y + 20f, 0f));

        textObject.transform.position = vec;
        textObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }


    GameObject CreateCube(float x, float y, Color color)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Vector3 vec = transform.TransformPoint(Vec(x, y, 0f));
        // cube.transform.SetParent(transform);
        cube.transform.SetParent(transform);
        cube.transform.position = transform.TransformPoint(Vec(x - 90f, 50f - y, 0f));
        cube.transform.localScale = new Vector3(7f, 7f, 7f);

        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        if (cubeRenderer != null)
            cubeRenderer.material.color = color;

        return cube;
    }

    public void OnAnswer(Question question, bool correct)
    {
        GameObject cube = questions[question];
        if (cube == null) return;

        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        if (cubeRenderer != null)
            cubeRenderer.material.color = correct ? correctColor : incorrectColor;
    }


    void OnDrawGizmos()
    {
        float i = 0f;
        float spacing = width / Component.VALUES.Count;

        foreach (var component in Component.VALUES)
        {
            float x = (i * spacing) + (spacing / 2f);
            float y = 30f;


            foreach (var question in component.Quiz.Questions)
            {
                DrawCube(x, y, pendingColor);
                y += 15f;
            }

            i++;
        }
    }

    private Vector3 Vec(float x, float y, float z) => new Vector3(x, y, z);    

    private void DrawCube(float x, float y, Color color)
    {
        Gizmos.color = color;
        Vector3 vec = transform.TransformPoint(Vec(x - 90f, 50f - y, 0f));
        Gizmos.DrawCube(vec, new Vector3(0.1f, 0.1f, 0.1f));   
    }
}

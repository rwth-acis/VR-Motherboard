using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISPresentItemScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0f;

    void Update()
    {

        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
    }
}
using System;
using UnityEngine;
using UnityEngineInternal;

public class MouseTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject handObject;
    private Vector3 offset;

    private void Start()
    {
        //hide cursror
        Cursor.visible = false;
    }

    private void Update()
    {
        // Track mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Update the position of the attached object
        handObject.transform.position = mousePosition + offset;
    }
}


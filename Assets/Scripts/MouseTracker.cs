using System;
using UnityEngine;
using UnityEngineInternal;

public class MouseTracker : MonoBehaviour
{
    private GameObject attachedObject;
    private Vector3 offset;

    private void Update()
    {
        // Track mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        if (attachedObject != null)
        {
            // Update the position of the attached object
            attachedObject.transform.position = mousePosition + offset;

            // Check if the attached object should be released
            if (Input.GetMouseButtonDown(0))
            {
                ReleaseObject();
            }

            if(Input.GetKeyDown(KeyCode.R))
            {
                RotateObject(45);
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                RotateObject(90);
            }
        }
        else
        {
            // Check for object selection
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
                if (hit.collider != null && hit.collider.CompareTag("Hand"))
                {
                    // Attach the object
                    attachedObject = hit.collider.gameObject;
                    offset = attachedObject.transform.position - mousePosition;
                }
            }
        }
    }

    private void RotateObject(int rotation = 90)
    {
        attachedObject.transform.Rotate(0, 0, rotation);
    }

    private void ReleaseObject()
    {
        // Reset variables and detach the object
        attachedObject = null;
        offset = Vector3.zero;
    }
}


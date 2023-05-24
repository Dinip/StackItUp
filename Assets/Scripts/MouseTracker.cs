using System;
using UnityEngine;
using UnityEngineInternal;

public class MouseTracker : MonoBehaviour
{
    Rigidbody2D _rb;

    private void Start()
    {
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Track mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Update the position of the attached object
        _rb.MovePosition(Vector2.Lerp(transform.position, mousePosition, 1));
        
        //without rigidbody
        //transform.position = mousePosition;
    }
}


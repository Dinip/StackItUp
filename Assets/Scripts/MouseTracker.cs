using System;
using UnityEngine;
using UnityEngineInternal;

public class MouseTracker : MonoBehaviour
{
    //[SerializeField]
    //private GameManagerObject gameManager;

    Rigidbody2D _rb;
    Vector2 _position = new(0f, 0f);

    private void Start()
    {
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //if (gameManager.isPaused) return;

        // Track mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = 0f;

        // Update the position of the attached object
        _rb.MovePosition(Vector2.Lerp(transform.position, mousePosition, 1));
        //transform.position = mousePosition;
    }

    //private void FixedUpdate()
    //{
    //    _rb.MovePosition(_position);
    //}
}


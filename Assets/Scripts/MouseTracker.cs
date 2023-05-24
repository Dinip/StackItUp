using System;
using UnityEngine;
using UnityEngineInternal;

public class MouseTracker : MonoBehaviour
{
    [SerializeField]
    private GameManagerObject gameManager;

    private Rigidbody2D _rb;

    private SpriteRenderer _spriteRenderer;

    private CircleCollider2D _circleCollider;

    private bool _selected = true;

    private Camera _camera;

    private delegate void MouseModeDelegate();
    private MouseModeDelegate _mouseModeDelegate;

    private void Start()
    {
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
        _camera = Camera.main;

        if (gameManager.mouseMode == MouseMode.Toggle)
        {
            _mouseModeDelegate = ToggleMode;
        }
        else if (gameManager.mouseMode == MouseMode.Hold)
        {
            _mouseModeDelegate = HoldMode;
        }
        else if (gameManager.mouseMode == MouseMode.Hold_Inv)
        {
            _mouseModeDelegate = HoldInvMode;
        }
    }

    private void Update()
    {
        _mouseModeDelegate?.Invoke();
        SetColliderAndColor();
        TrackMouse();
    }

    private void ToggleMode()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selected = !_selected;
        }
    }

    private void HoldMode()
    {
        _selected = Input.GetMouseButton(0);
    }

    private void HoldInvMode()
    {
        _selected = !Input.GetMouseButton(0);
    }

    private void SetColliderAndColor()
    {
        _circleCollider.enabled = _selected;
        _spriteRenderer.color = _selected ? Color.white : Color.gray;
    }

    private void TrackMouse()
    {
        // Track mouse position in world space
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        // Update the position of the attached object
        _rb.MovePosition(mousePosition);
    }
}


using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField]
    private GameManagerObject gameManager;

    private Rigidbody2D _rb;

    private bool _hasCollided = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            SendEvent();
            Debug.Log("Boundary");
            // Handle object falling off the platform
            // End the game or deduct points
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform") || collision.collider.CompareTag("FallingObject"))
        {
            SendEvent();
            // Attach the object to the platform
            //_rb.isKinematic = true;
            //transform.SetParent(collision.transform);
            // Adjust any necessary variables or triggers
            return;
        }
    }

    private void SendEvent()
    {
        if (!_hasCollided)
        {
            gameManager.collisionEvent.Invoke(true);
            _hasCollided = true;
        }
    }
}
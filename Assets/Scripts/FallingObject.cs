using UnityEngine;

public class FallingObject : MonoBehaviour {
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            // Attach the object to the platform
            rb.isKinematic = true;
            transform.SetParent(collision.transform);
            // Adjust any necessary variables or triggers
        }
        //else if (collision.CompareTag("Boundary"))
        //{
        //    // Handle object falling off the platform
        //    // End the game or deduct points
        //}
    }
}
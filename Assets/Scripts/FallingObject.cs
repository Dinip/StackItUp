using System.Collections;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField]
    private GameManagerObject gameManager;

    public bool hasCollided = false;
    public bool isCollidingWithHand = false;
    public bool isCollidingWithWinBound = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            SendEvent();
            gameManager.TakeDamage();
            Destroy(gameObject);
        }


        if (collision.CompareTag("WinBoundary"))
        {
            isCollidingWithWinBound = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform") || collision.collider.CompareTag("FallingObject"))
        {
            PlaySound();
            SendEvent();
            return;
        }

        if (collision.collider.CompareTag("Hand"))
        {
            isCollidingWithHand = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WinBoundary"))
        {
            isCollidingWithWinBound = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Hand"))
        {
            StartCoroutine(WaitCollisionEnd());
        }
    }

    private IEnumerator WaitCollisionEnd()
    {
        yield return new WaitForSeconds(3);
        isCollidingWithHand = false;
    }

    private void SendEvent()
    {
        if (!hasCollided)
        {
            gameManager.collisionEvent.Invoke(true);
            hasCollided = true;
        }
    }

    private void PlaySound()
    {
        if (!hasCollided)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blockPrefabs;

    [SerializeField]
    private GameManagerObject gameManager;

    [SerializeField]
    private GameObject nextObjectDisplayParent;

    [SerializeField]
    private GameObject winBoundary;

    private GameObject _nextObjectDisplay;

    private GameObject _nextObject;

    private void OnEnable()
    {
        gameManager.collisionEvent.AddListener(OnCollisionEvent);
    }

    private void OnDisable()
    {
        gameManager.collisionEvent.RemoveListener(OnCollisionEvent);
    }

    private void OnCollisionEvent(bool spawn)
    {
        SpawnItem();
    }

    private void Start()
    {
        //set winboundary y position
        //easy 5
        //medium 10
        //hard 15
        var winBoundaryPosition = winBoundary.transform.position;
        switch (gameManager.difficulty)
        {
            case Difficulty.Easy:
                winBoundaryPosition.y = 5.625f;
                break;
            case Difficulty.Medium:
                winBoundaryPosition.y = 10.625f;
                break;
            case Difficulty.Hard:
                winBoundaryPosition.y = 14.375f;
                break;
        }
        winBoundary.transform.position = winBoundaryPosition;

        SetNextObject();
        SpawnItem();
    }

    private void SpawnItem()
    {
        Instantiate(_nextObject, new Vector3(Random.Range(-2.5f, 2.5f), 18f, 0f), Quaternion.identity);
        SetNextObject();
    }

    private void SetNextObject()
    {
        _nextObject = blockPrefabs[Random.Range(0, blockPrefabs.Length)];
        DisplayItem();
    }

    private void DisplayItem()
    {
        if (_nextObjectDisplay != null) Destroy(_nextObjectDisplay);
        _nextObjectDisplay = Instantiate(_nextObject, new Vector3(85, 85, 85), Quaternion.identity);
        _nextObjectDisplay.GetComponent<Rigidbody2D>().isKinematic = true;
        _nextObjectDisplay.transform.SetParent(nextObjectDisplayParent.transform);
        _nextObjectDisplay.transform.localPosition = new Vector3(-125f, -180f, 0f);
        _nextObjectDisplay.transform.localScale = new Vector3(
            _nextObjectDisplay.transform.localScale.x * 1.25f,
            _nextObjectDisplay.transform.localScale.y * 1.25f,
            _nextObjectDisplay.transform.localScale.z * 1.25f);
    }

    private void FixedUpdate()
    {
        if (CheckWin())
        {
            gameManager.gameOverEvent.Invoke(true);
        }
    }

    private bool CheckWin()
    {
        //check hand isnt colliding with any falling objects
        //check any falling objects are colliding with win boundary
        var fallingObjects = FindObjectsOfType<FallingObject>();
        if (fallingObjects.Any(f => f.isCollidingWithHand)) return false;
        return fallingObjects.Any(f => f.hasCollided && f.isCollidingWithWinBound);
    }
}
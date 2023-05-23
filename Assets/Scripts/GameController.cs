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
                winBoundaryPosition.y = 5f;
                break;
            case Difficulty.Medium:
                winBoundaryPosition.y = 10f;
                break;
            case Difficulty.Hard:
                winBoundaryPosition.y = 15f;
                break;
        }
        winBoundary.transform.position = winBoundaryPosition;

        SetNextObject();
        SpawnItem();
    }

    private void SpawnItem()
    {
        Instantiate(_nextObject, new Vector3(Random.Range(0, 0), 18f, 0f), Quaternion.identity);
        SetNextObject();
    }

    private void SetNextObject()
    {
        _nextObject = blockPrefabs[0];
        DisplayItem();
    }

    private void DisplayItem()
    {
        if (_nextObjectDisplay != null) Destroy(_nextObjectDisplay);
        _nextObjectDisplay = Instantiate(_nextObject, new Vector3(100, 100, 100), Quaternion.identity);
        _nextObjectDisplay.GetComponent<Rigidbody2D>().isKinematic = true;
        _nextObjectDisplay.transform.SetParent(nextObjectDisplayParent.transform);
        _nextObjectDisplay.transform.localPosition = new Vector3(-125f, -170f, 0f);
        _nextObjectDisplay.transform.localScale = new Vector3(
            _nextObjectDisplay.transform.localScale.x * 1.25f,
            _nextObjectDisplay.transform.localScale.y * 1.25f,
            _nextObjectDisplay.transform.localScale.z * 1.25f);
    }

    private void FixedUpdate()
    {
        var win = CheckWin();
        if (win)
        {
            Debug.Log("Win");
        }
    }

    private bool CheckWin()
    {
        //cicle all objects of type FallingObject and for those
        //check if none of them is colliding with hand and
        //at the same time, if any is colliding with win boundary

        var fallingObjects = FindObjectsOfType<FallingObject>();
        if (fallingObjects.All(f => !f.isCollidingWithHand))
        {
            return fallingObjects.Any(f => f.hasCollided && f.isCollidingWithWinBound);
        }
        return false;
    }
}
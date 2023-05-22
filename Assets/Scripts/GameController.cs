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
        SetNextObject();
        SpawnItem();
    }

    private void SpawnItem()
    {
        Instantiate(_nextObject, new Vector3(Random.Range(-1.5f, 1.5f), 15f, 0f), Quaternion.identity);
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
        _nextObjectDisplay = Instantiate(_nextObject, new Vector3(100, 100, 100), Quaternion.identity);
        _nextObjectDisplay.GetComponent<Rigidbody2D>().isKinematic = true;
        _nextObjectDisplay.transform.SetParent(nextObjectDisplayParent.transform);
        _nextObjectDisplay.transform.localPosition = new Vector3(-100f, -135f, 0f);
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
            return fallingObjects.Any(f => f.isCollidingWithWinBound);
        }
        return false;
    }
}

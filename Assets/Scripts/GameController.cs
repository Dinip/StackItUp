using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public GameObject blockPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        Coroutine coroutine = StartCoroutine(SpawnBlocks());
    }

    private IEnumerator SpawnBlocks()
    {
        while (true)
        {
            Instantiate(blockPrefabs, new Vector3(Random.Range(-2.5f, 2.5f), 6f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWobble : MonoBehaviour {
    public float wobbleSpeed = 5f;
    public float wobbleAmount = 1f;

    private float startingPosition;

    private void Start()
    {
        startingPosition = transform.position.x;
    }

    private void Update()
    {
        float wobble = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        Vector3 newPosition = new Vector3(startingPosition + wobble, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;

    private Vector3 playerTransform;
    private Vector3 offset = new Vector3(0f, 3f, -3.5f);
    private Vector3 rotationOffset = new Vector3(20f, 0f, 0f);

    void Start()
    {
        StartCoroutine(FollowPlayer());
    }

    // Update is called once per frame

    IEnumerator FollowPlayer()
    {
        while (true)
        {
            playerTransform = player.GetComponent<Transform>().position;

            transform.position = playerTransform + offset;
            transform.rotation = Quaternion.Euler(rotationOffset);
            yield return null;
        }
    }
}

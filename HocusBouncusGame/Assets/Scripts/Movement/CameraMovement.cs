using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Player as game object that can be referenced.
    [SerializeField] GameObject player;
    // Saves the players current position.
    [SerializeField] Vector3 playerPosition;

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        transform.position = playerPosition;
    }
}

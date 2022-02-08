using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private float minSpeedToTeleport = 0.5f;

    [SerializeField]
    private float distanceToBall = 0.3f;

    private bool canTeleport = false;
    private Vector3[] teleportOffsets;

    // Start is called before the first frame update
    void Start()
    {
        teleportOffsets = new [] {
            new Vector3(distanceToBall, 0.0f, 0.0f),
            new Vector3(-distanceToBall, 0.0f, 0.0f),
            new Vector3(0.0f, 0.0f, distanceToBall),
            new Vector3(0.0f, 0.0f, -distanceToBall),
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.GetComponent<Rigidbody>().velocity.magnitude > minSpeedToTeleport)
        {
            canTeleport = true;
            ball.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (canTeleport)
        {
            TeleportNearBall();
            canTeleport = false;
            ball.GetComponent<Renderer>().material.color = Color.green;
        }

    }

    private Vector3 GetFreePositionOffset()
    {
        foreach (Vector3 offset in teleportOffsets)
        {
            // TODO: this doesn't work
            if (!Physics.CheckBox(ball.transform.position + offset, new Vector3(0.1f, 0.1f, 0.1f), transform.rotation))
            {
                return offset;
            }
        }

        return teleportOffsets[0];
    }

    private void TeleportNearBall()
    {
        Vector3 offset = GetFreePositionOffset();
        var player = GetComponentInChildren<OVRPlayerController>();

        player.enabled = false;
        transform.position = new Vector3(
            ball.transform.position.x + offset.x,
            transform.position.y,
            ball.transform.position.z + offset.z);
        player.enabled = true;
    }
}

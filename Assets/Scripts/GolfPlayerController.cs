using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private float minSpeedToTeleport = 0.5f;

    private bool canTeleport = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.GetComponent<Rigidbody>().velocity.magnitude < minSpeedToTeleport)
        {
            TeleportNearBall();
            canTeleport = false;
            ball.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            canTeleport = true;
            ball.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void TeleportNearBall()
    {

    }
}

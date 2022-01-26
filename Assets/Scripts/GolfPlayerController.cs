using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPlayerController : MonoBehaviour
{

    private GameObject golfStick = null;

    // Start is called before the first frame update
    void Start()
    {
        golfStick = GameObject.Find("Golf Stick");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

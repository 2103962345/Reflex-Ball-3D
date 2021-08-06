using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    // follow ball definition
    public Transform targetBall;

    //  Cam position
    public float camY;
    public float distance;

    
    private void Update()
    {
            transform.position = new Vector3(transform.position.x, camY, targetBall.position.z - distance);
    }
}

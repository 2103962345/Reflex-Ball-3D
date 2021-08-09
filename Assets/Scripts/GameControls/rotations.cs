using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotations : MonoBehaviour
{
    Vector3 rotation = new Vector3(0,2,0);

    private void FixedUpdate()
    {
        gameObject.transform.Rotate(rotation);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotations : MonoBehaviour
{
  
    public Vector3 rotation;
    float start = 0;

    private void Update() {
        if (Time.time >= start)
        {
            gameObject.transform.Rotate(rotation);
            start = Time.time + .1f;
        }
        
    }
   
}

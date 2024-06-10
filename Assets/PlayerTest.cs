using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10; // Set this to be the distance between the camera and your object
            Vector3 objectPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objectPosition;
        }
    }
}

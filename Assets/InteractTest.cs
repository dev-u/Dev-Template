using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTest : MonoBehaviour
{

    

    public void TestContactInteract(GameObject gameObject)
    {
        Debug.Log("Interacted on Contact!");
        Debug.Log("Object: " + gameObject.name);
        Destroy(gameObject);
    }

    public void TestKeyInteract(GameObject gameObject)
    {
        Debug.Log("Interacted!");
        Destroy(gameObject);
    }

    private void TestInteract(GameObject gameObject)
    {
        Debug.Log("Interacted!");
        Destroy(gameObject);
    }
}

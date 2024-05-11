using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTest : MonoBehaviour
{

    

    public void TestContactInteract()
    {
        Debug.Log("Interacted on Contact!");
        Destroy(gameObject);
    }

    public void TestKeyInteract()
    {
        Debug.Log("Interacted!");
        Destroy(gameObject);
    }
}

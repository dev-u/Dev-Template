using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
       
    [SerializeField] float parallaxEffect;
    [SerializeField] private GameObject[] layers;

    private Camera cam;
    private float[] lengths;
    private float[] startPos;

    void Start()
    {
        cam = Camera.main;
        lengths = new float[layers.Length];
        startPos = new float[layers.Length];

        for (int i = 0; i < layers.Length; i++)
        {
            startPos[i] = layers[i].gameObject.transform.position.x;
            lengths[i] = layers[i].gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

        
    // Usar o lateUpdate() 
    void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            float temp = cam.transform.position.x * (1 - parallaxEffect);
            float dist = cam.transform.position.x * parallaxEffect;

            layers[i].gameObject.transform.position = new Vector3(startPos[i] + dist, 
                                                                  layers[i].gameObject.transform.position.y,
                                                                  layers[i].gameObject.transform.position.z);

            if (temp > startPos[i] + lengths[i]) startPos[i] += lengths[i];
            else if (temp < startPos[i] - lengths[i]) startPos[i] -= lengths[i];
        }
    }
}

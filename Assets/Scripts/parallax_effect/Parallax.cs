using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header ("Os objetos devem ser maiores que a camera para melhor uso do parallax \n para criar mais de um objeto colocar pelo menos um background como entidade pai")]
    [Header ("GameObjects dos backgrounds")] 
    [SerializeField] private GameObject[] layers;
    [Header ("ParallaxEffect, segue a ordem das layers \n 1 = mesma velocidade da camera \n quanto menor o valor mais devagar a velocidade do objeto \n quanto mais maior o valor mais rapido fica o objeto")]
    [SerializeField] private float[] parallaxEffect;
    private Camera cam;
    private float [] length, startpos;       
    private float temp, dist;
    void Start()
    {
        if (layers.Length != parallaxEffect.Length)
        {
            Debug.Log ("Não há velocidade para todas as layers, ou não há layers para todas as velocidades");
        }
        length = new float[layers.Length];
        startpos = new float[layers.Length];
        
        cam = Camera.main;
        for (int i = 0; i < layers.Length; i++){
            startpos[i] = layers[i].transform.position.x;
            length[i] = layers[i].GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }


    void LateUpdate()
    {
        for (int i = 0; i < layers.Length; i++){
            
            temp = cam.transform.position.x * (1f - parallaxEffect[i]);
            dist = cam.transform.position.x * parallaxEffect[i];
            
            layers[i].transform.position = new Vector3(startpos[i] + dist, layers[i].transform.position.y, layers[i].transform.position.z); 
        
            if (temp > startpos[i] + length[i]) startpos[i] += length[i];
            else if (temp < startpos[i] - length[i]) startpos[i] -= length[i];
        }
    }
}

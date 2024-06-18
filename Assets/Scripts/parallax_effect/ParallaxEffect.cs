using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    #region variables
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Transform _playerReference;

    [SerializeField]
    private List<ParallaxObject> _parallaxObjects;

    private List<Vector2> _travelDistances => ComputeTravelDistances();
    #endregion


    
    // Start is called before the first frame update
    void Start()
    {
        //// Fills the list with the Parallax Objects
        //ParallaxObject[] parallaxObjects = FindObjectsOfType(typeof(ParallaxObject)) as ParallaxObject[];
        //if (parallaxObjects != null)
        //{
        //    foreach (ParallaxObject parallaxObject in parallaxObjects)
        //    {
        //        _parallaxObjects.Add(parallaxObject);
        //    }
        //} 
    }

    // Update is called once per frame
    void Update()
    {
        if (_parallaxObjects != null)
        {
            foreach (ParallaxObject parallaxObject in _parallaxObjects)
            {
                parallaxObject.gameObject.transform.position = parallaxObject.StartPosition + _travelDistances[parallaxObject.GetComponentIndex()] * parallaxObject.ParallaxEffect;
            }
        }
    }

    List<Vector2> ComputeTravelDistances()
    {
        List<Vector2> travelDistances = new List<Vector2>();

        foreach (ParallaxObject parallaxObject in _parallaxObjects)
        {
            Vector2 travelDistance = (Vector2)_camera.transform.position - parallaxObject.StartPosition;
            travelDistances.Add(travelDistance);
        }

        return travelDistances;
    }
}

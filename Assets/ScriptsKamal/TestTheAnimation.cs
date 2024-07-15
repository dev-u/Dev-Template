using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatorHelper))]
public class TestTheAnimation : MonoBehaviour //only to see the AnimatorHelper working
{
    [SerializeField] string nameAnimation;
    [SerializeField] bool rotate;
    [SerializeField] bool changeAnimation;

    private void Update()
    {
        if (changeAnimation)
        {
            if(rotate)
            {
                GetComponent<AnimatorHelper>().SetReverseAnimation(nameAnimation);
            }
            else
            {
                GetComponent<AnimatorHelper>().SetAnimation(nameAnimation);
            }
            changeAnimation = false;
        }
    }
}

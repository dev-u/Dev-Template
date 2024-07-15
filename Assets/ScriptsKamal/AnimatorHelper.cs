using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.SceneManagement;
using UnityEngine;

//[RequireComponent(typeof(Animator))]
public class AnimatorHelper : MonoBehaviour
{
    [SerializeField] private AnimatorController controller;
    private Animator animator;
    void Start()
    {
        if (GetComponent<Animator>() == null) //set the animator if it is not setted
        {
            animator = gameObject.AddComponent<Animator>();
        }
        animator.runtimeAnimatorController = controller; //set the controller
    }
    public void SetAnimation(string name)
    {
        transform.eulerAngles = new Vector3(0f, 0f, 0f); //set the normal rotation
        animator.Play(name);
    }
    public void SetReverseAnimation(string name)
    {
        transform.eulerAngles = new Vector3(0f, 180f, 0f); //set the oposite rotation
        animator.Play(name);
    }
}

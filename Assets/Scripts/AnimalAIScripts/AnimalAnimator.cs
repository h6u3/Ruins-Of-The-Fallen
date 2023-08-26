using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimator : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private AnimalController animal;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool(IS_WALKING, animal.IsWalking());
    }
}

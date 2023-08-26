using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private EnemyController enemy;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool(IS_WALKING, enemy.IsWalking());
    }
}

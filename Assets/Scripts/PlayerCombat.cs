using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class PlayerCombat : MonoBehaviour {

    public static PlayerCombat Instance { get; set; }

    //[SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    //private float rotateSpeed = 10f;
    private Transform currentTarget;
    private PlayerStats playerStats;
    private TargetUI targetUI;
    private float coneAngle = 90f;
    private float coneDistance = 5f;
    private bool playerDead = false;

    private void Start () {
        playerStats = PlayerStats.instance;
        targetUI = TargetUI.instance;
    }

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        //HandleMovement();
        if (!playerDead)
        {
            CheckInteractCone();
            if (gameInput.checkLeftClick())
            {
                if (currentTarget != null)
                {
                    Attack();
                }
            }

            if (gameInput.checkRightClick())
            {
            }
            currentTarget = null;
        }
    }

    //private void HandleMovement()
    //{
    //    float moveDistance = moveSpeed * Time.deltaTime;

    //    Vector2 inputVector = gameInput.GetMovementVectorNormalised();
    //    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

    //    transform.position += moveDir * moveDistance;

    //    transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    //}

    public void setPlayerDead(bool isDead)
    {
        playerDead = isDead;
    }

    private void CheckInteractCone() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, coneDistance);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Attackable"))
            {
                //Debug.Log("enemyinrange");
                Vector3 direction = collider.transform.position - transform.position;
                float angle = Vector3.Angle(transform.forward, direction);

                if (angle <= coneAngle * 0.5f)
                {
                    currentTarget = collider.transform;
                    targetUI.setCurrentTarget(currentTarget);
                    targetUI.updateHealthBar();
                    break;
                }
            }
        }

        if (currentTarget == null)
        {
            currentTarget = null;
            targetUI.setCurrentTarget(null);
        }
    }

    private void Attack() {
            
        if (currentTarget.name == "Enemy") {
            EnemyController enemyController = currentTarget.GetComponent<EnemyController>();

            if (enemyController != null) {
                float updatedHealth = enemyController.getEnemyHealth() - playerStats.getAttack();
                enemyController.setEnemyHealth(updatedHealth);
                targetUI.updateHealthBar();
                enemyController.checkHealth();
            }
        }

        if (currentTarget.name == "Animal") {
            AnimalController animalController = currentTarget.GetComponent<AnimalController>();
                
            if (animalController != null) {
                float updatedHealth = animalController.getAnimalHealth() - playerStats.getAttack();
                animalController.setAnimalHealth(updatedHealth);
                targetUI.updateHealthBar();
                animalController.checkHealth();
            }
        }
    }
}

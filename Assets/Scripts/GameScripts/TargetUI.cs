using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetUI : MonoBehaviour
{
    public static TargetUI instance;
    [SerializeField] private TextMeshProUGUI NPCName;
    [SerializeField] private TextMeshProUGUI ThreatLevel;
    [SerializeField] private Image HealthBarBackground;
    [SerializeField] private Image HealthBar;
    private Transform currentTarget;
    private float targetHealth;
    private float maxTargetHealth;
    private float HealthBarDefault;
    private string targetThreatLevel;

    private RectTransform rectTransform;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        rectTransform = HealthBar.rectTransform;
        HealthBarDefault = rectTransform.rect.width;
        ShowUI(false);
    }

    private void ShowUI(bool isShow) {
        gameObject.SetActive(isShow);
        setHealthBarDefault();
    }

    public void setCurrentTarget(Transform target) {
        if (target != null) {
        currentTarget = target;

         if (currentTarget.name == "Enemy") {
            EnemyController enemyController = currentTarget.GetComponent<EnemyController>();

            if (enemyController != null) {
                targetHealth = enemyController.getEnemyHealth();
                maxTargetHealth = enemyController.getMaxEnemyHealth();
                targetThreatLevel = enemyController.GetThreatLevel();
            }
        }

        if (currentTarget.name == "Animal") {
            AnimalController animalController = currentTarget.GetComponent<AnimalController>();
                
            if (animalController != null) {
                targetHealth = animalController.getAnimalHealth();
                maxTargetHealth = animalController.getMaxAnimalHealth();
                targetThreatLevel = animalController.GetThreatLevel();
            }
        }

        setTargetText();
        ShowUI(true);
        }
        else {
            NPCName.text = "";
            ShowUI(false);
            currentTarget = null;
        }
        

    }

    private void setTargetText() {
        NPCName.text = currentTarget.name;
        ThreatLevel.text = "Threat: "+targetThreatLevel;
    }

    public void updateHealthBar() {
        RectTransform rectTransform = HealthBar.rectTransform;
    
        float HealthPercent = targetHealth / maxTargetHealth;
        float newWidth = rectTransform.rect.width * HealthPercent;

        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
    }

    public void setHealthBarDefault() {
        rectTransform.sizeDelta = new Vector2(HealthBarDefault, rectTransform.sizeDelta.y);
    }
}

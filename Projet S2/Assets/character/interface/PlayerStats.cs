using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int currentHealth;
    private int maxHealth;
    public GameObject mortv;

    [SerializeField] GameObject ragdoll;

    private int currentMana;
    private int maximumMana;

    private float healthRegenerationRate;
    private float manaRegenerationRate;

    public delegate void HealthChangedEvent(float currentValue);
    public delegate void ManaChangedEvent(float currentValue);

    public event HealthChangedEvent healthChanged;
    public event ManaChangedEvent manaChanged;

    public bool getDamage;
    public int damageAmount;

    private float lastDamageTime; // Time of the last received damage
    private float regenDelay = 5f; // Delay before health starts regenerating
    private float regenMultiplier = 1f;

    private void Awake() {
        StartCoroutine(RecoverResources());
        currentHealth = 100;
        maxHealth = 100;
        healthRegenerationRate = 5;
        currentMana = 100;
        maximumMana = 100;
        manaRegenerationRate = 5;
    }

    public void AddDamage(int amount) { // use negative value to heal
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
        float percent = (float)currentHealth / (float)maxHealth;
        healthChanged?.Invoke(percent);

        if (amount > 0) {
            lastDamageTime = Time.time; // Reset the regen delay timer
        }
    }

    public void ConsumeMana(int amount) {
        currentMana = Mathf.Clamp(currentMana - amount, 0, maximumMana);
        float percent = (float)currentMana / (float)maximumMana;
        manaChanged?.Invoke(percent);
    }

    public bool HasEnoughMana(int requiredValue) {
        return currentMana > requiredValue;
    }

    private IEnumerator RecoverResources() {
        float healthAmount = 0;
        float manaAmount = 0;
        while (true) {
            if (Time.time - lastDamageTime >= regenDelay) {
                healthAmount += Time.deltaTime * healthRegenerationRate * regenMultiplier;
                manaAmount += Time.deltaTime * manaRegenerationRate;
                if (healthAmount > 1) {
                    AddDamage(-Mathf.FloorToInt(healthAmount));
                    healthAmount = healthAmount % 1;
                }
                if (manaAmount > 1) {
                    ConsumeMana(-Mathf.FloorToInt(manaAmount));
                    manaAmount = manaAmount % 1;
                }
            }
            yield return null;
    }}

    private void Update() {
        if (getDamage) {
            AddDamage(damageAmount);
            getDamage = false;
        }
        if (currentHealth <= 2)
        {
            Instantiate(ragdoll, transform.position, transform.rotation);
            Destroy(this.gameObject);
            mortv.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
        }
    }

    
}
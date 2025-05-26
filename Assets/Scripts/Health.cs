using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Maximum health value
    [SerializeField] private int currentHealth; // Current health value
    [SerializeField] private bool isDead = false; // Flag to check if the entity is dead
    [SerializeField] private GameObject deathEffect; // Effect to play on death
    [SerializeField] private AudioClip deathSound; // Sound to play on death
    [SerializeField] private AudioSource audioSource; // Audio source to play the death sound
    [SerializeField] private bool destroyOnDeath = false; // Whether to destroy the GameObject on death
    [SerializeField] private bool disableOnDeath = false; // Whether to disable the GameObject on death
    [SerializeField] private bool isPlayer = false; // Flag to check if this is the player

    // Event to notify when health changes
    public delegate void HealthChanged(int newHealth);
    public event HealthChanged OnHealthChanged;

    // Properties to access current health and dead status
    public int CurrentHealth
    {
        get { return currentHealth; }
        private set { currentHealth = value; }
    }
    public bool IsDead
    {
        get { return isDead; }
        private set { isDead = value; }
    }
    public int MaxHealth
    {
        get { return maxHealth; }
        private set { maxHealth = value; }
    }

    private static Health _playerHealth; // Static instance for player health


    private void Awake()
    {
        if (isPlayer)
        {
            if (_playerHealth == null) _playerHealth = this;
            else Debug.LogWarning("Multiple player health instances found. Only one should exist at a time.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // If already dead, do nothing
        currentHealth -= damage; // Reduce current health by damage amount
        if (currentHealth <= 0)
        {
            Die(); // Call the Die method if health is zero or less
            currentHealth = 0;
        }
        OnHealthChanged?.Invoke(currentHealth); // Notify subscribers about the health change
    }

    void Die()
    {
        if (isDead) return;
        isDead = true; // Set the dead flag to true
    }

    public static Health GetPlayerHealth()
    {
        return _playerHealth; // Return the static instance of player health
    }
}

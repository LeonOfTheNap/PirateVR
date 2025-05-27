using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    [SerializeField] private TextMeshPro healthText; // Reference to the TextMeshProUGUI component
    [SerializeField] private Health playerHealth; // Reference to the player's Health component

    void Start()
    {
        if (playerHealth == null) playerHealth = Health.GetPlayerHealth();
        if (healthText == null) healthText = GetComponent<TMPro.TextMeshPro>();

        if (playerHealth != null) UpdateHealthText(playerHealth.CurrentHealth); // Initialize the health text with the current health
        else Debug.LogError("Player Health instance is not set. Please assign it in the inspector or ensure it is initialized.");

        playerHealth.OnHealthChanged -= UpdateHealthText; // Subscribe to the health change event
        playerHealth.OnHealthChanged += UpdateHealthText; // Subscribe to the health change event
    }

    void OnEnable()
    {
        if (playerHealth != null) playerHealth.OnHealthChanged += UpdateHealthText; // Subscribe to the health change event
    }

    void OnDisable()
    {
        if (playerHealth != null) playerHealth.OnHealthChanged -= UpdateHealthText; // Unsubscribe from the health change event
    }

    void UpdateHealthText(int newHealth)
    {
        if (healthText == null) return;
        healthText.text = $"{newHealth}/{playerHealth.MaxHealth}"; // Update the text with the current health value
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRadius : MonoBehaviour
{
    [SerializeField] private float damageRadius = 5f; // Radius within which damage is applied
    [SerializeField] private int damageAmount = 10; // Amount of damage to apply
    [SerializeField] private Health playerHealth;
    [SerializeField] private float damageInterval = 5f; // Time interval between damage applications
    private float nextDamageTime; // Time when the next damage will be applied

    // Update is called once per frame
    void Update()
    {
        if (playerHealth == null) return;
        if (Vector3.Distance(transform.position, playerHealth.transform.position) > damageRadius) return; // Check if player is within damage radius

        if (Time.time >= nextDamageTime)
        {
            playerHealth.TakeDamage(damageAmount); // Apply damage to the player
            Debug.Log($"Player took {damageAmount} damage from {gameObject.name}. Current Health: {playerHealth.CurrentHealth}");
            nextDamageTime = Time.time + damageInterval; // Set the next damage time
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = Health.GetPlayerHealth(); // Get the player health instance if not assigned
            if (playerHealth == null)
            {
                Debug.LogError("Player Health instance is not set. Please assign it in the inspector or ensure it is initialized.");
            }
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius); // Draw a wire sphere to visualize the damage radius
    }
}

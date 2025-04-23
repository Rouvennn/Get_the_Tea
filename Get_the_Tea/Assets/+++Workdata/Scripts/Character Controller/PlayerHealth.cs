using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHearts = 3;
    public int currentHearts;

    private bool isInvincible = false;
    public float invincibilityDuration = 1f;

    void Start()
    {
        currentHearts = maxHearts;
    }

    public void TakeDamage(int damage = 1)
    {
        if (isInvincible) return;

        currentHearts -= damage;
        Debug.Log($"Player took damage! Hearts left: {currentHearts}");

        if (currentHearts <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvincibilityCooldown());
    }

    IEnumerator InvincibilityCooldown()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    void Die()
    {
        Debug.Log("Player Died!");

        // show highscorescreen and let player enter name if new highscore
    }

    public void Heal(int heal = 1)
    {
        if (currentHearts >= maxHearts) return;

        currentHearts = Mathf.Min(currentHearts + heal, maxHearts);
        Debug.Log($"Player healed! Hearts now: {currentHearts}");
    }
}

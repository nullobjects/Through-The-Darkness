using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrapScript : MonoBehaviour {
    private bool Used = false;
    public Animator TrapAnimator;

    void OnTriggerEnter2D(Collider2D other) {
        if (Used) return;

        if (other.CompareTag("Player")) {
            Used = true;
            TrapAnimator.SetTrigger("Activate");

            other.GetComponent<PlayerMovement>().Kill();
        } else if (other.CompareTag("Enemy")) {
            Used = true;
            TrapAnimator.SetTrigger("Activate");

            Transform childTransform = other.transform.Find("GFX");

            if (childTransform != null) {
                EnemyGFX enemyGFX = childTransform.GetComponent<EnemyGFX>();

                if (enemyGFX != null) {
                    enemyGFX.Kill();
                }
            }
        }
    }
}

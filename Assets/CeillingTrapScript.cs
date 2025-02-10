using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CeillingTrapScript : MonoBehaviour {
    private bool Used = false;
    public Animator TrapAnimator;
    public Light2D CandleLight;

    void OnTriggerEnter2D(Collider2D other) {
        if (Used) return;

        if (other.CompareTag("Player") && CandleLight != null && CandleLight.enabled) {
            Used = true;
            TrapAnimator.SetTrigger("Activate");

            other.GetComponent<PlayerMovement>().Kill();
        }
    }
}

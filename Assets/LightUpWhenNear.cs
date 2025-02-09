using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightUpWhenNear : MonoBehaviour {
    public Transform Player;
    void Update() {
        if (Player == null) return;

        float distance = Vector2.Distance(Player.position, transform.position);
        if (distance >= 10f) {
            transform.GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = false;
        } else {
            transform.GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = true;
        }
    }
}

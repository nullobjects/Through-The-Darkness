using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

public class EnemyGFX : MonoBehaviour {
    public AIPath aipath;
    public Animator animator;
    public Transform Player;
    private bool Alive;
    public float DistanceCanMove = 10f;

    void Update() {
        float distance = Vector2.Distance(Player.position, transform.position);
        
        if (distance < DistanceCanMove) {
            Transform candle = Player.transform.Find("Candle");

            if (candle == null || candle.GetComponent<CandleMovement>().IsCandleLit()) {
                aipath.canMove = true;
            } else {
                if (aipath.canMove) {
                    aipath.canMove = false;
                    animator.SetFloat("Speed", 0f);
                    return;
                }
            }
        }

        if (!aipath.canMove) return;

        float speed = aipath.desiredVelocity.magnitude;
        animator.SetFloat("Speed", speed);

        if (Math.Abs(aipath.desiredVelocity.x) > Math.Abs(aipath.desiredVelocity.y)) {
            if (aipath.desiredVelocity.x >= 0.01f) { // Right
                animator.SetInteger("Direction", 3);
            } else if (aipath.desiredVelocity.x <= -0.01f) { // Left
                animator.SetInteger("Direction", 2);
            }
        } else {
            if (aipath.desiredVelocity.y >= 0.01f) { // Up
                animator.SetInteger("Direction", 0);
            } else if (aipath.desiredVelocity.y <= -0.01f) { // Down
                animator.SetInteger("Direction", 1);
            }
        }
    }

    public void Kill() {
        /*Alive = false;

        animator.SetFloat("Speed", 0f);

        if (Direction == 0) {
            animator.Play("enemy_death_back");
        } else if (Direction == 1) {
            animator.Play("enemy_death_front");
        } else if (Direction == 2) {
            animator.Play("enemy_death_left");
        } else if (Direction == 3) {
            animator.Play("enemy_death_right");
        }*/
        Destroy(transform.parent.gameObject);
    }
}

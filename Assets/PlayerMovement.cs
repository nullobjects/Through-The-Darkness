using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public float speed = 3f;
    public float BonusSpeed = 0f;
    private float MoveX;
    private float MoveY;
    private int Direction = 1;
    public Animator animator;
    private bool Alive = true;
    public bool CanMove = true;
    private Rigidbody2D rb;
    private LevelLoader LevelLoader;
    private bool CanDie = true;

    public coin_manager cm;
    public key_collect kc;

    public int GetDirection() {
        return Direction;
    }

    public void SetCanDie(bool canDie) {
        CanDie = canDie;
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        LevelLoader = FindFirstObjectByType<LevelLoader>();
    }

    void Update() {
        if (!CanMove || !Alive) return;

        MoveX = Input.GetAxisRaw("Horizontal");
        MoveY = Input.GetAxisRaw("Vertical");

        float TotalSpeed = speed + BonusSpeed;
        float speedX = TotalSpeed * MoveX;
        float speedY = TotalSpeed * MoveY;

        float absx = Math.Abs(speedX);
        float absy = Math.Abs(speedY);

        if (absx > 0 || absy > 0) {
            if (absx >= absy) {
                Direction = (speedX > 0) ? 3 : 2;
            } else {
                Direction = (speedY > 0) ? 0 : 1;
            }
        }

        float newSpeed = Mathf.Max(absx, absy);
        animator.SetFloat("Speed", newSpeed);

        if (animator.GetInteger("Direction") != Direction) {
            animator.SetInteger("Direction", Direction);
        }

        Vector2 movement = new Vector2(speedX, speedY);

        if (movement.magnitude > 0) {
            movement.Normalize();
        }

        rb.linearVelocity = movement * TotalSpeed;
    }

    public void Kill() {
        if (!CanDie) return;

        Alive = false;
        
        rb.linearVelocity = new Vector2(0, 0);
        animator.SetFloat("Speed", 0f);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (Direction == 0) {
            animator.Play("player_death_back");
        } else if (Direction == 1) {
            animator.Play("player_death_front");
        } else if (Direction == 2) {
            animator.Play("player_death_left");
        } else if (Direction == 3) {
            animator.Play("player_death_right");
        }

        StartCoroutine(DelayedRestart());
    }

    private IEnumerator DelayedRestart() {
        yield return new WaitForSeconds(2f);

        if (LevelLoader != null) {
            LevelLoader.RestartLevel();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Kill();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            cm.flag = true;
            PlayerScript.AddCoins(1);
        }
        if (other.gameObject.CompareTag("key"))
        {
            Destroy(other.gameObject);
            if (kc != null) {
                kc.hasKey = true;
            }
        }
    }
    // Jumping version for blocks but it's bugged bruh //
    /*
     * private bool Jumping = false;
     * void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Block") && !Jumping) {
            Jumping = true;
            animator.SetFloat("Speed", 0);

            float JumpY = 0f;
            if (Direction == 0) {
                JumpY = 2f;
            } else if (Direction == 1) {
                JumpY = -2f;
            }

            float JumpX = 0f;
            if (Direction == 3) {
                JumpX = 2f;
            } else if (Direction == 2) {
                JumpX = -2f;
            }

            rb.position = new Vector2(rb.position.x + JumpX, rb.position.y + JumpY);
            Jumping = false;
        }
    }*/
}

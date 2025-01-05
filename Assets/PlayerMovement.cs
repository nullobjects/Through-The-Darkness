using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {
    public float speed = 3f;
    private float MoveX;
    private float MoveY;
    public int Direction = 1;
    public Animator animator;
    private bool Alive = true;
    private bool CanMove = true;
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
        LevelLoader = FindObjectOfType<LevelLoader>();
    }

    void Update() {
        if (!CanMove || !Alive) return;

        MoveX = Input.GetAxis("Horizontal");
        MoveY = Input.GetAxis("Vertical");

        float speedX = speed * MoveX;
        float speedY = speed * MoveY;

        if (Math.Abs(speedX) > 0 && Math.Abs(speedX) > Math.Abs(speedY)) {
            animator.SetFloat("Speed", Math.Abs(speedX));
            animator.SetFloat("Speed", Math.Abs(speedX));

            if (speedX > 0) {
                Direction = 3;
            } else {
                Direction = 2;
            }
        } else if (Math.Abs(speedY) > 0 && Math.Abs(speedY) > Math.Abs(speedX)) {
            animator.SetFloat("Speed", Math.Abs(speedY));

            if (speedY > 0) {
                Direction = 0;
            } else {
                Direction = 1;
            }
        } else if (animator.GetFloat("Speed") != 0) {
            animator.SetFloat("Speed", 0);
        }

        if (animator.GetInteger("Direction") != Direction) {
            animator.SetInteger("Direction", Direction);
        }

        rb.velocity = new Vector2(speedX, speedY);
    }

    public void Kill() {
        if (!CanDie) return;

        Alive = false;
        
        rb.velocity = new Vector2(0, 0);
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

        LevelLoader.RestartLevel();
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
        }
        if (other.gameObject.CompareTag("key"))
        {
            Destroy(other.gameObject);
            kc.hasKey = true;
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

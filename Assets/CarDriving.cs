using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriving : MonoBehaviour {
    private float MoveX;
    private float MoveY;
    private Rigidbody2D rb;
    private float speedX = 15f;
    private float speedY = 10f;
    private LevelLoader LevelLoader;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        LevelLoader = FindAnyObjectByType<LevelLoader>();
    }

    void Update() {
        MoveY = Input.GetAxis("Vertical");

        float speedYf = speedY * MoveY;

        rb.linearVelocity = new Vector2(speedX, speedYf);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Obstacle")) {
            rb.linearVelocity = new Vector2(0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            LevelLoader.RestartLevel();
        }
    }
}

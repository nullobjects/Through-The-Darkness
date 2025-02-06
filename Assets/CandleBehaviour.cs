using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandleMovement : MonoBehaviour {
    public Transform target;
    public UnityEngine.Rendering.Universal.Light2D CandleLight;
    private float FollowSpeed = 5.5f;
    private PlayerMovement playerMovement;
    public Animator animator;

    private void Awake() {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        animator.SetBool("IsCandleLit", true);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (IsCandleLit()) {
                DisableLight();
            } else {
                EnableLight();
            }
        }

        int Direction = playerMovement.GetDirection();

        Renderer renderer = GetComponent<Renderer>();

        Vector3 newPos = new Vector3(target.position.x + 0.3f, target.position.y - 0.1f, 0f);
        if (Direction == 0) {
            newPos = new Vector3(target.position.x, target.position.y + 0.1f, 0f);
            renderer.sortingOrder = 5;
        } else if (Direction == 1) {
            newPos = new Vector3(target.position.x, target.position.y - 0.3f, 0f);
            renderer.sortingOrder = 6;
        } else if (Direction == 2) {
            newPos = new Vector3(target.position.x - 0.5f, target.position.y - 0.1f, 0f);
            renderer.sortingOrder = 6;
        } else if (Direction == 3) {
            newPos = new Vector3(target.position.x + 0.5f, target.position.y - 0.1f, 0f);
            renderer.sortingOrder = 6;
        }

        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }

    public void EnableLight() {
        CandleLight.enabled = true;
        animator.SetBool("IsCandleLit", true);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Transform Light = enemy.transform.GetChild(1);
            Light.gameObject.SetActive(true);
        }
    }

    private void DisableLight() {
        CandleLight.enabled = false;
        animator.SetBool("IsCandleLit", false);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Transform Light = enemy.transform.GetChild(1);
            Light.gameObject.SetActive(false);
        }
    }

    public bool IsCandleLit() {
        return CandleLight.enabled;
    }
}

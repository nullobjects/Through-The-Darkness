using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CandleLightBehaviour : MonoBehaviour {
    public Transform target;
    private float FollowSpeed = 5.5f;
    private PlayerMovement playerMovement;

    private void Awake() {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    void Update() {
        int Direction = playerMovement.Direction;

        Vector3 newPos = new Vector3(target.position.x + 0.3f, target.position.y - 0.1f, 0f);
        if (Direction == 0) {
            newPos = new Vector3(target.position.x, target.position.y + 0.1f, 0f);
        } else if (Direction == 1) {
            newPos = new Vector3(target.position.x, target.position.y - 0.3f, 0f);
        } else if (Direction == 2) {
            newPos = new Vector3(target.position.x - 0.3f, target.position.y - 0.1f, 0f);
        } else if (Direction == 3) {
            newPos = new Vector3(target.position.x + 0.3f, target.position.y - 0.1f, 0f);
        }

        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);

        float targetAngle = 0f;

        switch (Direction) {
            case 0: // Up
                targetAngle = 0f;
                break;
            case 1: // Down
                targetAngle = 180f;
                break;
            case 2: // Left
                targetAngle = 90f;
                break;
            case 3: // Right
                targetAngle = 270f;
                break;
        }

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.5f * Time.deltaTime);
    }
}

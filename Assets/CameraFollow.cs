using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    public float followSpeed = 2.0f;

    private LayerMask collisionLayerMask;

    void Start() {
        collisionLayerMask = LayerMask.GetMask("Wall");
    }

    void LateUpdate() {
        // If the direction is needed //
        //int Direction = target.GetDirection();
        /*float RayHitDistanceU = 1f;
        float RayHitDistanceD = 3.2f;
        float RayHitDistanceL = 9.5f;
        float RayHitDistanceR = 9.5f;

        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);

        Vector3 RayShootPosU = new Vector3(target.position.x, target.position.y - 0.7f, 0);
        Vector3 RayShootPosD = new Vector3(target.position.x, target.position.y - 0.7f, 0);
        Vector3 RayShootPosL = new Vector3(target.position.x, target.position.y - 0.7f, 0);
        Vector3 RayShootPosR = new Vector3(target.position.x, target.position.y - 0.7f, 0);

        RaycastHit2D hitU = Physics2D.Raycast(RayShootPosU, Vector2.up, RayHitDistanceU, collisionLayerMask);
        RaycastHit2D hitD = Physics2D.Raycast(RayShootPosD, Vector2.down, RayHitDistanceD, collisionLayerMask);
        RaycastHit2D hitL = Physics2D.Raycast(RayShootPosL, Vector2.left, RayHitDistanceL, collisionLayerMask);
        RaycastHit2D hitR = Physics2D.Raycast(RayShootPosR, Vector2.right, RayHitDistanceR, collisionLayerMask);

        if (hitU.collider != null && hitU.collider.CompareTag("Wall")) {
            //Debug.Log("U " + (RayHitDistanceU - hitU.distance));
            newPos.y -= (RayHitDistanceU - hitU.distance);
        }
        if (hitD.collider != null && hitD.collider.CompareTag("Wall")) {
            //Debug.Log("D " + (RayHitDistanceD - hitD.distance));
            newPos.y += (RayHitDistanceD - hitD.distance);
        }
        if (hitL.collider != null && hitL.collider.CompareTag("Wall")) {
            //Debug.Log("L " + (RayHitDistanceL - hitL.distance));
            newPos.x += (RayHitDistanceL - hitL.distance);
        }
        if (hitR.collider != null && hitR.collider.CompareTag("Wall")) {
            //Debug.Log("R " + (RayHitDistanceR - hitR.distance));
            newPos.x -= (RayHitDistanceR - hitR.distance);
        }*/

        Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        
        /*// Draw raycasts in the Scene view
        Debug.DrawRay(RayShootPosU, Vector2.up * RayHitDistanceU, Color.red, 3);
        Debug.DrawRay(RayShootPosD, Vector2.down * RayHitDistanceD, Color.green, 3);
        Debug.DrawRay(RayShootPosL, Vector2.left * RayHitDistanceL, Color.blue, 3);
        Debug.DrawRay(RayShootPosR, Vector2.right * RayHitDistanceR, Color.yellow, 3);*/
    }
}

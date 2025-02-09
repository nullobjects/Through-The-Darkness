using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CandleMovement : MonoBehaviour {
    public Transform target;
    public Light2D CandleLight;
    private float FollowSpeed = 5.5f;
    private PlayerMovement playerMovement;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    public Sprite defaultSprite;
    public Sprite FlashlightSprite;
    public MerchantShop merchantShop;
    public Light2D flashlight_light;

    private void Awake() {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        animator.SetBool("IsCandleLit", true);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = defaultSprite;
        flashlight_light.gameObject.SetActive(false);
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart() {
        yield return new WaitForSeconds(0.1f);
    
        print(merchantShop.GetItem("Flashlight").purchased);
        if (merchantShop.GetItem("Flashlight").purchased) {
            spriteRenderer.sprite = FlashlightSprite;
            flashlight_light.gameObject.SetActive(true);    
        }
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
            //GetComponent<SpriteRenderer>().flipY = true;
        } else if (Direction == 1) {
            newPos = new Vector3(target.position.x, target.position.y - 0.3f, 0f);
            renderer.sortingOrder = 6;
            //GetComponent<SpriteRenderer>().flipY = false;
        } else if (Direction == 2) {
            newPos = new Vector3(target.position.x - 0.5f, target.position.y - 0.1f, 0f);
            renderer.sortingOrder = 6;
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (Direction == 3) {
            newPos = new Vector3(target.position.x + 0.5f, target.position.y - 0.1f, 0f);
            renderer.sortingOrder = 6;
            GetComponent<SpriteRenderer>().flipX = true;
        }

        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }

    public void EnableLight() {
        CandleLight.enabled = true;
        flashlight_light.enabled = true;
        animator.SetBool("IsCandleLit", true);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            Transform Light = enemy.transform.GetChild(1);
            Light.gameObject.SetActive(true);
        }
    }

    private void DisableLight() {
        CandleLight.enabled = false;
        flashlight_light.enabled = false;
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

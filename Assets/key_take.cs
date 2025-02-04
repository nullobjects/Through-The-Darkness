using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_take : MonoBehaviour
{
    public Transform key;
    public Transform door;
    private bool hasKey = false;

    void Update() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == key && !hasKey)
        {
            hasKey = true;
            Destroy(key.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform == door && hasKey)
        {
            door.gameObject.SetActive(false);
        }
    }
}
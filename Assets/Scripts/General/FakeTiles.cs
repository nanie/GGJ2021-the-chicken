using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTiles : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 90));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInParent<FallingPuzzle>().PlayerFall();
    }
}

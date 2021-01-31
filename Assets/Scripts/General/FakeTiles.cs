using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTiles : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInParent<FallingPuzzle>().PlayerFall();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPuzzle : MonoBehaviour
{

    [SerializeField] private Transform playerRespawnPoint;
    [SerializeField] private float animationTime = 2;

    private SingleAnimationManager playerAnimation;
    private PlayerController controller;
    private DamageManager damage;
    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerAnimation = player.GetComponent<SingleAnimationManager>();
        controller = player.GetComponent<PlayerController>();
        damage = player.GetComponent<DamageManager>();
    }

    public void PlayerFall()
    {
        controller.PlayerCanWalk(false);
        damage.SetDamage(1);
        playerAnimation.Fall();
        StartCoroutine(RespawnPlayer());
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(animationTime);
        controller.transform.position = playerRespawnPoint.position;
        controller.PlayerCanWalk(true);
    }
}

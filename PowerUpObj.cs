using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpObj : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.player.power = PowerUps.SPEED;
            Destroy(gameObject);
            //play sound
            GameManager.instance.player.pickUpSFX.Play();
            //play effect
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacals : MonoBehaviour
{
    private BoxCollider2D collider2D;
    private void Start()
    {
        collider2D = gameObject.GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //play soundfx
            StartCoroutine(PlaySound());
        }
    }

    IEnumerator PlaySound()
    {
        GameManager.instance.player.HitSFX.Play();
        Debug.Log("Sound played");
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.ResetDay();

    }

    public void EnableCollider()
    {
        collider2D.enabled = true;
    }

    public void DisableCollider()
    {
        collider2D.enabled = false;
    }
}

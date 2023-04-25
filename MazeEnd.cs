using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
        }
    }
}

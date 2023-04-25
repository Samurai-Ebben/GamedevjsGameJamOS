using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    public GameObject closedChest = null;
    public GameObject openChest = null;

    // Start is called before the first frame update
    void Start()
    {
        closedChest.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            closedChest.SetActive(false);
            openChest.SetActive(true);
        }
    }
}

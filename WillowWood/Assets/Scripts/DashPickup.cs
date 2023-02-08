using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPickup : MonoBehaviour
{
    [SerializeField] Abilities dashAbility;
    [SerializeField] gameManager gameManager;

    public void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //make function in player script for pickup
            gameManager.playerScript.hasDash = true;

            Destroy(gameObject);
        }
    }
}

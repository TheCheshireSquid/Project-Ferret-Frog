using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TRAP : MonoBehaviour
{
    public PlayerScript playerScript;

    [SerializeField] int DMG;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            gameManager.instance.playerScript.TakeDamage(1);
        }
    }
}
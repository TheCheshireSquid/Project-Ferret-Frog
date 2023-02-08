using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu]
[System.Serializable]
public class DashAbility : Abilities
{
    public float dashVelocity;
    [SerializeField] public gameManager gameManager;
    public override void Activate(GameObject parent)
    {
        //if(playerScript.hasDash == true) 
        if (gameManager.playerScript.hasDash)
        {
            PlayerScript movement = parent.GetComponent<PlayerScript>();
            Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();

            var xx = movement.input.normalized;
            xx.y = 0;

            rb.velocity = xx * dashVelocity;
        }

    }
}

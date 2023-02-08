using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    Controls controls;
    public Abilities ability;
    float activeTime;
    float cooldownTime = 0;
   
    [SerializeField] gameManager gameManager;
 
    enum abilityState
    {
        ready,
        active,
        cooldown
    }

    abilityState state = abilityState.ready;


    private void Start()
    {
        controls = new Controls();
        controls.Player.Enable();
        if(ability is DashAbility)
        {
            ((DashAbility)ability).gameManager = gameManager;
        }
    }


    private void Update()
    {
        switch (state)
        {
            case abilityState.ready:
                if (controls.Player.Dash.WasPressedThisFrame())
                {
                    ability.Activate(gameManager.player);
                    state = abilityState.active;
                    activeTime = ability.activeTime;
                }  
                break;
            case abilityState.active:
                if (activeTime > 0f)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = abilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }

                break;  
            case abilityState.cooldown:
                if (cooldownTime > 0f)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = abilityState.ready;
                }
                break;
        }
    }
}

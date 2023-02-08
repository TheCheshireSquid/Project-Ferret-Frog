using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : ScriptableObject
{
   public float activeTime;
   public float cooldownTime;
   public GameObject effect;

    public virtual void Activate(GameObject parent)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitableBehaviour : MonoBehaviour, IHittable
{
    [SerializeField]
    UnityEvent a;

    public void TakeHit(float damage)
    {
        a?.Invoke();

    }

}


public interface IHittable
{
    void TakeHit(float damage);
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CachedColliderBall 
{
    private static Dictionary<int, ICollider> CacheDynamicUnits = new();
    public static ICollider GetColliderUnit(this Collision2D collision)
    {
        int id = collision.gameObject.GetInstanceID();
        if (CacheDynamicUnits.TryGetValue(id, out ICollider dynamicUnit))
        {
            return dynamicUnit;
        }

        ICollider newUnit = collision.gameObject.GetComponent<ICollider>();
        CacheDynamicUnits.Add(id, newUnit);
        return newUnit;
    }
}

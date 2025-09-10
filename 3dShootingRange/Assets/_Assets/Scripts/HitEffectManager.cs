using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitSurface
{
    Default,
    Flesh,
    Metal,
    Concrete,
    Wood,
    Dirt,
    Crate
}

[CreateAssetMenu(menuName = "HitEffects/HitEffectDatabase")]
public class HitEffectDatabase : ScriptableObject
{
    public GameObject defaultEffect;
    public GameObject fleshEffect;
    public GameObject metalEffect;
    public GameObject concreteEffect;
    public GameObject woodEffect;
    public GameObject dirtEffect;
    public GameObject crateEffect;

    public GameObject GetEffect(HitSurface s)
    {
        switch (s)
        {
            case HitSurface.Flesh: return fleshEffect ? fleshEffect : defaultEffect;
            case HitSurface.Metal: return metalEffect ? metalEffect : defaultEffect;
            case HitSurface.Concrete: return concreteEffect ? concreteEffect : defaultEffect;
            case HitSurface.Wood: return woodEffect ? woodEffect : defaultEffect;
            case HitSurface.Dirt: return dirtEffect ? dirtEffect : defaultEffect;
            case HitSurface.Crate: return crateEffect ? crateEffect : defaultEffect;
            default: return defaultEffect;
        }
    }
}

public class HitEffectManager : MonoBehaviour
{
    public HitEffectDatabase database;
    public float effectLifetime = 5f; // auto destroy

    public void SpawnHitEffect(Vector3 position, Vector3 normal, HitSurface surface)
    {
        if (database == null) return;
        GameObject prefab = database.GetEffect(surface);
        if (prefab == null) return;

        Quaternion rot = Quaternion.LookRotation(normal);
        GameObject go = Instantiate(prefab, position + normal * 0.01f, rot);
        // optionally parent to hit object? we leave world-space
        Destroy(go, effectLifetime);
    }
}

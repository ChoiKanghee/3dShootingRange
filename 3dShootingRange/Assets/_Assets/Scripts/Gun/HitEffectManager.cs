using UnityEngine;

public class HitEffectManager : MonoBehaviour
{
    public static HitEffectManager Instance { get; private set; }

    [System.Serializable]
    public class EffectEntry { public HitSurface surface; public GameObject prefab; }
    public EffectEntry[] effects;
    public float lifeTime = 5f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SpawnHitEffect(Vector3 pos, Vector3 normal, HitSurface surface)
    {
        GameObject prefab = null;
        foreach (var e in effects) if (e.surface == surface) { prefab = e.prefab; break; }
        if (!prefab) return;
        var go = Instantiate(prefab, pos, Quaternion.LookRotation(normal));
        Destroy(go, lifeTime);
    }
}

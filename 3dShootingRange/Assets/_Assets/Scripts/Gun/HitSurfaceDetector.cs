using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitSurfaceDetector : MonoBehaviour
{
    public HitSurface surface = HitSurface.Default;
}

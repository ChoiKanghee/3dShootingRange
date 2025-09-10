using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitSurfaceDetector : MonoBehaviour
{
    public HitSurface surface = HitSurface.Default;

    // simple helper: you can set surface in inspector per collider
}
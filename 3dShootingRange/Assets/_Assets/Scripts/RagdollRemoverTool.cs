using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollRemoverTool : MonoBehaviour
{
    // Press a key (or call method) to find ragdolled zombies and reset them.
    public KeyCode resetKey = KeyCode.R;

    void Update()
    {
        if (Input.GetKeyDown(resetKey))
        {
            ResetAllZombies();
        }
    }

    public void ResetAllZombies()
    {
        ZombieRagdoll[] all = FindObjectsOfType<ZombieRagdoll>();
        foreach (var z in all)
        {
            z.ForceDisableRagdoll();
            // optionally reset health
            // z.ResetHealth(); // implement if needed
        }
    }
}
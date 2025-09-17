using UnityEngine;

public class RagdollRemoverTool : MonoBehaviour
{
    public KeyCode resetKey = KeyCode.R;

    void Update()
    {
        if (Input.GetKeyDown(resetKey))
            foreach (var z in FindObjectsOfType<ZombieRagdoll>())
                z.ForceDisableRagdoll();
    }
}

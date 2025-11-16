using UnityEngine;
using System.Collections;

public class SpawnOnTouch : MonoBehaviour
{
    public SculptManager sculptManager;
    public bool spawnCube = true; 

    public float cooldownTime = 0.5f;

    bool onCooldown = false;

    void OnTriggerEnter(Collider other)
    {
        if (onCooldown) return;
        if (sculptManager == null) return;

        if (spawnCube)
        {
            sculptManager.SpawnCube();
        }
        else
        {
            sculptManager.SpawnSphere();
        }

        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        onCooldown = false;
    }
}

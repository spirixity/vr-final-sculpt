using UnityEngine;

public class TableTrigger : MonoBehaviour
{
    [SerializeField] private string objectNameContains; // Set in Inspector: "Cube" or "Circle"
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if object name matches expected word
        if (other.name.Contains(objectNameContains))
        {
            // Play the sound once
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
                Debug.Log($"{objectNameContains} placed correctly!");
            }
        }
    }
}

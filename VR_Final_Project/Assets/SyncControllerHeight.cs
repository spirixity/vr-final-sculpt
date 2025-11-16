using UnityEngine;
public class SyncControllerHeight : MonoBehaviour
{
    public CharacterController controller;
    public Transform xrCamera;
    public float minHeight = 1.2f, maxHeight = 2.2f;
    void Reset(){ controller = GetComponent<CharacterController>(); if (Camera.main) xrCamera = Camera.main.transform; }
    void LateUpdate()
    {
        if (!controller || !xrCamera) return;
        float h = Mathf.Clamp(xrCamera.localPosition.y, minHeight, maxHeight);
        controller.height = h;
        controller.center = new Vector3(0, h * 0.5f, 0);
    }
}

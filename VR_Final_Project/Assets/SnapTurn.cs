using UnityEngine;
using UnityEngine.InputSystem;

public class SnapTurn : MonoBehaviour
{
    public Transform rigRoot;                       
    public InputActionProperty turnAction;         
    InputAction _fallback;                         

    public float snapAngle = 45f;                  
    public float cooldown = 0.35f;                 
    float nextAllowed;

    void OnEnable()
    {
        if (turnAction.reference == null)
        {
            _fallback = new InputAction(type: InputActionType.Value, binding: "<XRController>{RightHand}/thumbstick");
            _fallback.Enable();
        }
        else turnAction.action.Enable();
    }

    void OnDisable()
    {
        if (_fallback != null) _fallback.Disable();
        else if (turnAction.reference != null) turnAction.action.Disable();
    }

    void Update()
    {
        if (!rigRoot) return;

        Vector2 v = turnAction.reference != null ? turnAction.action.ReadValue<Vector2>()
                                                 : _fallback.ReadValue<Vector2>();

        if (Time.time < nextAllowed) return;

        if (v.x > 0.6f) { Rotate(snapAngle);  nextAllowed = Time.time + cooldown; }
        else if (v.x < -0.6f) { Rotate(-snapAngle); nextAllowed = Time.time + cooldown; }
    }

    void Rotate(float angle)
    {
        Transform cam = Camera.main ? Camera.main.transform : null;
        Vector3 pivot = cam ? new Vector3(cam.position.x, rigRoot.position.y, cam.position.z)
                            : rigRoot.position;
        transform.RotateAround(pivot, Vector3.up, angle);
    }
}

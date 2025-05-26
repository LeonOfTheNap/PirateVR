using UnityEngine;
using UnityEngine.InputSystem;

public class SwordController : MonoBehaviour
{
    public GameObject sword;
    public InputActionReference activateSwordAction;

    private bool swordActive = false;

    void OnEnable()
    {
        activateSwordAction.action.performed += ToggleSword;
    }

    void OnDisable()
    {
        activateSwordAction.action.performed -= ToggleSword;
    }

    private void ToggleSword(InputAction.CallbackContext ctx)
    {
        swordActive = !swordActive;
        sword.SetActive(swordActive);
    }
}
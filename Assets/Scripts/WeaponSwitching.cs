using UnityEngine.InputSystem;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    InputAction switching;
    public int selectedWeapon = 0;

    void Start()
    {
        switching = new InputAction("Scroll", binding: "<Mouse>/scroll");
        switching.AddBinding("<Gamepad>/Dpad");
        switching.performed += _ => SwitchWeapon();
        switching.Enable();

        SelectWeapon();
    }

    void SwitchWeapon()
    {
        int previousSelected = selectedWeapon;
        selectedWeapon += (int)switching.ReadValue<Vector2>().y;

        if (selectedWeapon < 0)
            selectedWeapon = transform.childCount - 1;
        else if (selectedWeapon >= transform.childCount)
            selectedWeapon = 0;

        if (previousSelected != selectedWeapon)
            SelectWeapon();
    }

    private void SelectWeapon()
    {
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
        transform.GetChild(selectedWeapon).gameObject.SetActive(true);
    }
}

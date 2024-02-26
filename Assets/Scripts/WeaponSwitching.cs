using UnityEngine.InputSystem;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    InputAction switching;
    public int selectWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        switching = new InputAction("Scroll", binding: "<Mouse>/scroll");
        switching.Enable();
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        float scrollValue = switching.ReadValue<Vector2>().y;
        int previousSelected = selectWeapon;
        if (scrollValue > 0)
        {
            selectWeapon++;
            if (selectWeapon == 2)
            {
                selectWeapon = 0;
            }
        }
        else if (scrollValue < 0)
        {
            selectWeapon--;
            if (selectWeapon == -1)
            {
                selectWeapon = 2;
            }
        }
        if (previousSelected != selectWeapon)
            SelectWeapon();
    }

    private void SelectWeapon()
    {
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(false);
        }
        transform.GetChild(selectWeapon).gameObject.SetActive(true);
    }
}

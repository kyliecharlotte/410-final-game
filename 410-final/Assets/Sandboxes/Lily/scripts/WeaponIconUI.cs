using UnityEngine;
using UnityEngine.UI;

public class WeaponIconUI : MonoBehaviour
{
    [SerializeField] private Image weaponIconImage;
    [SerializeField] private Sprite maceIcon;
    [SerializeField] private Sprite crossbowIcon;

    public void UpdateWeaponIcon(GameObject weapon)
    {
        Debug.Log("weapon change");
        if (weapon == null)
        {
            weaponIconImage.enabled = false;
            return;
        }

        if (weapon.CompareTag("Mace_Weapon"))
        {
            weaponIconImage.sprite = maceIcon;
            weaponIconImage.enabled = true;
        }
        else if (weapon.CompareTag("Crossbow_Weapon"))
        {
            weaponIconImage.sprite = crossbowIcon;
            weaponIconImage.enabled = true;
        }
        else
        {
            weaponIconImage.enabled = false;
        }
    }
}

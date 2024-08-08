using UnityEngine;
using UnityEngine.UI;

public class Sarjor : MonoBehaviour
{
    public int MaxRedAmmo = 60;
    public int MaxGreenAmmo = 60;
    public int MaxBlueAmmo = 60;

    public int RedAmmo;
    public int GreenAmmo;
    public int BlueAmmo;

    public int RedClipSize = 30;
    public int GreenClipSize = 30;
    public int BlueClipSize = 30;

    public int RedClipAmmo;
    public int GreenClipAmmo;
    public int BlueClipAmmo;

    void Start()
    {
        RedAmmo = MaxRedAmmo;
        GreenAmmo = MaxGreenAmmo;
        BlueAmmo = MaxBlueAmmo;

        RedClipAmmo = RedClipSize;
        GreenClipAmmo = GreenClipSize;
        BlueClipAmmo = BlueClipSize;
    }
    public void Reload()
    {
        int redAmmoNeeded = RedClipSize - RedClipAmmo;
        int greenAmmoNeeded = GreenClipSize - GreenClipAmmo;
        int blueAmmoNeeded = BlueClipSize - BlueClipAmmo;
        if (RedAmmo >= redAmmoNeeded)
        {
            RedAmmo -= redAmmoNeeded;
            RedClipAmmo = RedClipSize;
        }
        else
        {
            RedClipAmmo += RedAmmo;
            RedAmmo = 0;
        }

        if (GreenAmmo >= greenAmmoNeeded)
        {
            GreenAmmo -= greenAmmoNeeded;
            GreenClipAmmo = GreenClipSize;
        }
        else
        {
            GreenClipAmmo += GreenAmmo;
            GreenAmmo = 0;
        }

        if (BlueAmmo >= blueAmmoNeeded)
        {
            BlueAmmo -= blueAmmoNeeded;
            BlueClipAmmo = BlueClipSize;
        }
        else
        {
            BlueClipAmmo += BlueAmmo;
            BlueAmmo = 0;
        }
        Debug.Log("Yeniden dolduruldu!");
    }

    public bool HasAmmo(string ammoColor)
    {
        if (ammoColor == "Red" && RedClipAmmo > 0) return true;
        if (ammoColor == "Green" && GreenClipAmmo > 0) return true;
        if (ammoColor == "Blue" && BlueClipAmmo > 0) return true;
        return false;
    }

    public void Fire(string ammoColor)
    {
        if (ammoColor == "Red" && RedClipAmmo > 0)
        {
            RedClipAmmo--;
            Debug.Log("Red Ammo fired. Remaining in clip: " + RedClipAmmo);
        }
        else if (ammoColor == "Green" && GreenClipAmmo > 0)
        {
            GreenClipAmmo--;
            Debug.Log("Green Ammo fired. Remaining in clip: " + GreenClipAmmo);
        }
        else if (ammoColor == "Blue" && BlueClipAmmo > 0)
        {
            BlueClipAmmo--;
            Debug.Log("Blue Ammo fired. Remaining in clip: " + BlueClipAmmo);
        }
        else
        {
            Debug.Log("No ammo in clip, reload needed!");
        }
    }
}

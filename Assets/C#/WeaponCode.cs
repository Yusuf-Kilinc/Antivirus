using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class WeaponCode : MonoBehaviour
{
    public AudioSource ses;
    public AudioClip sound;
    public Sarjor Sarjr;
    RaycastHit hit;
    public float mesafe, sýkma_araligi;
    bool fire = true;
    public ParticleSystem muzzleFlash;
    public float AimAssist;
    public GameObject kamera, raycast;
    GameObject Hedef;
    public GameObject DeathObj;
    // Renk butonlarý
    public Button fireButton;
    public Button redButton;
    public Button greenButton;
    public Button blueButton;
    public Image Cursor;

    private string currentAmmoColor = "Red"; // Varsayýlan renk

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

        ses.clip = sound;
        muzzleFlash.Stop();

        // Fire button'a týklama eventini ekle
        fireButton.onClick.AddListener(FireButtonClicked);

        // Renk butonlarýna týklama eventleri ekle
        redButton.onClick.AddListener(() => ChangeAmmoColor("Red"));
        greenButton.onClick.AddListener(() => ChangeAmmoColor("Green"));
        blueButton.onClick.AddListener(() => ChangeAmmoColor("Blue"));
    }
    public void FireButtonClicked()
    {
        if (fire && HasAmmo(currentAmmoColor))
        {
            fire = false;
            AtesEt();
        }
    }
    void Update()
    {
        // Raycast ýþýðýný sahnede çiz
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * mesafe, Color.yellow);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, mesafe))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                GameObject Chedef = hit.transform.gameObject;
                Target CtargetScript = Chedef.GetComponent<Target>();
                if (CtargetScript != null)
                {
                    if (CtargetScript.enemyColor == "Red")
                    {
                        Cursor.color = Color.red;
                    }
                    else if (CtargetScript.enemyColor == "Green")
                    {
                        Cursor.color = Color.green;
                    }
                    else if (CtargetScript.enemyColor == "Blue")
                    {
                        Cursor.color = Color.blue;
                    }
                    else
                    {
                        Cursor.color = Color.white;
                    }
                }
            }
            else
            {
                Cursor.color = Color.white;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }
        else
        {
            Cursor.color = Color.white;
        }
    }
    public void AtesEt()
    {
        muzzleFlash.Play();
        Fire(currentAmmoColor);  // Mevcut mermi rengi ile ateþ et
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, mesafe))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Hedef = hit.transform.gameObject;
                Target targetScript = Hedef.GetComponent<Target>();

                if (targetScript != null)
                {
                    if (currentAmmoColor == "Red" && targetScript.enemyColor == "Red")
                    {
                        targetScript.TakeDamage(100);
                    }
                    else if (currentAmmoColor == "Green" && targetScript.enemyColor == "Green")
                    {
                        targetScript.TakeDamage(100);
                    }
                    else if (currentAmmoColor == "Blue" && targetScript.enemyColor == "Blue")
                    {
                        targetScript.TakeDamage(100);
                    }
                    else
                    {
                        targetScript.GainHealth(10);
                    }
                }
                Instantiate(DeathObj, hit.point, Quaternion.identity);
                StartCoroutine(YOKET());
                Debug.Log("Raycast isabet etti: " + hit.transform.name);
            }
            ses.Play();
            StartCoroutine(FireTime());
        }
    }
    public void ChangeAmmoColor(string color)
    {
        currentAmmoColor = color;
    }
    IEnumerator FireTime()
    {
        yield return new WaitForSeconds(sýkma_araligi);
        fire = true;
        muzzleFlash.Stop();
    }

    IEnumerator YOKET()
    {
        yield return new WaitForSeconds(5);
        Destroy(Hedef);
    }

    public void Reload()
    {
        int redAmmoNeeded = RedClipSize - RedClipAmmo;
        int greenAmmoNeeded = GreenClipSize - GreenClipAmmo;
        int blueAmmoNeeded = BlueClipSize - BlueClipAmmo;

        if (currentAmmoColor == "Red")
        {
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
        }
        else if (currentAmmoColor == "Green")
        {
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
        }
        else if (currentAmmoColor == "Blue")
        {
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

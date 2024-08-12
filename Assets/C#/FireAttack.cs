using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FireAttack : MonoBehaviour
{

    public float Mermi, Sarjor, FullBullet, Menzil, Hasar1, Hasar2, NewBullet, FireTime, sayi, Zmn, MaxZmn;
    public float RedSayi, RedMer, RedFull, newRed, redSarjor;
    public bool Fire, Reload;
    public bool RedTrue, RedReload;

    RaycastHit hit;
    public TextMeshProUGUI Bullettext;
    public ParticleSystem muzzleFlash;
    public AudioSource ses;
    public AudioClip sound;
    public Image Cursor;
    private string currentAmmoColor = "Red"; // Varsayýlan renk
    public GameObject DeathObj;
    float Mesafe = 50;
    GameObject Hedef;

    float asdasdasdas;
    private void Start()
    {
        Zmn = MaxZmn;
    }
    void Update()
    {
        #region CursorSystem
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * Mesafe, Color.yellow);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mesafe))
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
        }
        else
        {
            Cursor.color = Color.white;
        }


        #endregion

        if (!RedTrue)
        {
            Bullettext.text = "" + Mermi + "/" + FullBullet;
        }

        if (Input.GetMouseButton(0) && Mermi > 0 && Time.time > NewBullet && !Reload)
        {
            Fire = true;
            NewBullet = Time.time + FireTime;
            Mermi--;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Reload = true;
        }
        if (Reload == true && Mermi != 30)
        {
            Zmn -= Time.deltaTime;
            sayi = Sarjor - Mermi;

            if (Zmn <= 0)
            {
                Reload = false;
                Zmn = MaxZmn;
                if (sayi > FullBullet)
                {
                    Mermi += FullBullet;
                    FullBullet = 0;

                }
                if (sayi < FullBullet)
                {
                    Mermi += sayi;
                    FullBullet -= sayi;
                }
            }
        }

        if (RedTrue)
        {
            Bullettext.text = "" + RedMer + "/" + RedFull;
            if (Input.GetMouseButton(0) && RedMer > 0 && Time.time > newRed && !Reload)
            {
                Fire = true;
                newRed = Time.time + FireTime;
                RedMer--;
                muzzleFlash.Play();
                ses.clip = sound;
                ses.Play();
            }
            else
            {
                muzzleFlash.Stop();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                RedReload = true;
            }
            if (RedReload == true && RedMer != 30)
            {
                Zmn -= Time.deltaTime;
                RedSayi = redSarjor - RedMer;

                if (Zmn <= 0)
                {
                    RedReload = false;
                    Zmn = MaxZmn;
                    if (RedSayi > RedFull)
                    {
                        RedMer += RedFull;
                        RedFull = 0;

                    }
                    if (RedSayi < RedFull)
                    {
                        RedMer += RedSayi;
                        RedFull -= RedSayi;
                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (Fire)
        {
            Fire = false;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Menzil))
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
            }
        }
    }
    IEnumerator YOKET()
    {
        yield return new WaitForSeconds(5);
        Destroy(Hedef);
    }
    public void RedBullet()
    {
        RedTrue = true;
    }
}
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FireAttack : MonoBehaviour
{
    #region Floats
    public float /*Mermi, Sarjor, FullBullet,*/ Menzil, Hasar1, Hasar2,/* NewBullet,*/ FireTime,/* sayi,*/ Zmn, MaxZmn;
    public float RedSayi, RedMer, RedFull, newRed, redSarjor;
    public float BlueSayi, BlueMer, BlueFull, NewBlue, BlueSarjor;
    public float GreenSayi, GreenMer, GreenFull, NewGreen, GreenSarjor;
    float Mesafe = 50;
    #endregion

    #region Bools
    public bool Fire, Reload;
    public bool RedTrue, RedReload;
    public bool BlueTrue, BlueReload;
    public bool GreenTrue, GreenReload;
    #endregion

    #region Digerleri
    RaycastHit hit;
    public TextMeshProUGUI Bullettext;
    public ParticleSystem muzzleFlash;
    public AudioSource ses;
    public AudioClip sound;
    public Image Cursor;
    private string currentAmmoColor = "Red"; // Varsayżlan renk
    public GameObject DeathObj;
    GameObject Hedef,SpawnerHedef;
    public GunRecoil Recoil;
    #endregion

    private void Start()
    {
        Zmn = MaxZmn;
        RedTrue = true;
        GreenTrue = false;
        BlueTrue = false;
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

        #region Red || Blue || Green

        #region RedTrue
        if (RedTrue)
        {
            Bullettext.text = "" + RedMer + "/" + RedFull;
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
        #endregion

        #region BlueTrue
        if (BlueTrue)
        {
            Bullettext.text = "" + BlueMer + "/" + BlueFull;
            if (BlueReload == true && BlueMer != 30)
            {
                Zmn -= Time.deltaTime;
                BlueSayi = BlueSarjor - BlueMer;

                if (Zmn <= 0)
                {
                    BlueReload = false;
                    Zmn = MaxZmn;
                    if (BlueSayi > BlueFull)
                    {
                        BlueMer += BlueFull;
                        BlueFull = 0;

                    }
                    if (BlueSayi < BlueFull)
                    {
                        BlueMer += BlueSayi;
                        BlueFull -= BlueSayi;
                    }
                }
            }
        }
        #endregion

        #region GreenTrue
        if (GreenTrue)
        {
            Bullettext.text = "" + GreenMer + "/" + GreenFull;
            if (GreenReload == true && GreenMer != 30)
            {
                Zmn -= Time.deltaTime;
                GreenSayi = GreenSarjor - GreenMer;

                if (Zmn <= 0)
                {
                    GreenReload = false;
                    Zmn = MaxZmn;
                    if (GreenSayi > GreenFull)
                    {
                        GreenMer += GreenFull;
                        GreenFull = 0;

                    }
                    if (GreenSayi < GreenFull)
                    {
                        GreenMer += GreenSayi;
                        GreenFull -= GreenSayi;
                    }
                }
            }
        }
        #endregion

        #endregion
    }
    private void FixedUpdate()
    {
        #region Fire
        if (Fire)
        {
            Recoil.ApplyRecoil();
            Fire = false;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Menzil))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    Hedef = hit.transform.gameObject;
                  //  Target targetScript = Hedef.GetComponent<Target>();
                    RedVirus RVirus = Hedef.GetComponent<RedVirus>();
                    if (RVirus != null)
                    {
                        if (currentAmmoColor == "Red" && RVirus.enemyColor == "Red")
                        {
                            RVirus.TakeDamage(100);
                        }
                        else if (currentAmmoColor == "Green" && RVirus.enemyColor == "Green")
                        {
                            RVirus.TakeDamage(100);
                        }
                        //else if (currentAmmoColor == "Blue" && RVirus.enemyColor == "Blue")
                        //{
                        //    RVirus.TakeDamage(100);
                        //}
                        //else
                        //{
                        //    RVirus.GainHealth(10);
                        //}
                    }
                    Instantiate(DeathObj, hit.point, Quaternion.identity);
                    StartCoroutine(YOKET());
                    Debug.Log("Raycast isabet etti: " + hit.transform.name);
                }
                if (hit.transform.tag == "Spawner")
                {
                    Hedef = hit.transform.gameObject;
                    Spawner SpawnerScript = Hedef.GetComponent<Spawner>();
                    SpawnerScript.Health -= 5;
                }
            }
        }
        #endregion
    }
    IEnumerator YOKET()
    {
        yield return new WaitForSeconds(5);
        Destroy(Hedef);
    }
    public void RedBullet()
    {
        GreenTrue = false;
        BlueTrue = false;
        RedTrue = true;
    }
    public void GreenBullet()
    {
        GreenTrue = true;
        BlueTrue = false;
        RedTrue = false;
    }
    public void BlueBullet()
    {
        GreenTrue = false;
        BlueTrue = true;
        RedTrue = false;
    }
    public void WeaponFire()
    {
        if (RedTrue)
        {
            if (/*Input.GetMouseButton(0) &&*/ RedMer > 0 && Time.time > newRed && !Reload)
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
        }
        if (BlueTrue) 
        {
            if (/*Input.GetMouseButton(0) && */BlueMer > 0 && Time.time > NewBlue && !Reload)
            {
                Fire = true;
                NewBlue = Time.time + FireTime;
                BlueMer--;
                muzzleFlash.Play();
                ses.clip = sound;
                ses.Play();
            }
            else
            {
                muzzleFlash.Stop();
            }
        }
        if (GreenTrue)
        {
            if (/*Input.GetMouseButton(0) &&*/ GreenMer > 0 && Time.time > NewGreen && !Reload)
            {
                Fire = true;
                NewGreen = Time.time + FireTime;
                GreenMer--;
                muzzleFlash.Play();
                ses.clip = sound;
                ses.Play();
            }
            else
            {
                muzzleFlash.Stop();
            }
        }
    }
    public void WeaponReload()
    {
        if (RedTrue)
        {
            RedReload = true;
        }
        if (BlueTrue)
        {
            BlueReload = true;
        }
        if (GreenTrue) 
        {
            GreenReload = true;
        }
    }
}
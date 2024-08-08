using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ates : MonoBehaviour
{
    public float Mesafe;

    public float sıkma_aralıgı;
    float zamanlayici;

    public float Hasar;

    public float AimAssist;

    public int Mermi_Sayisi;
    public int Sarjor;
    public int Sarjor_mermi;

    public TextMeshProUGUI mermisayisi, sarjor;

    public Image crosshair;

    bool fire = true;
    bool scope = false;
    bool Breload;

    public AudioSource sfx;
    public AudioClip ak;
    public AudioClip reload;
    public AudioClip Bos;

    public ParticleSystem muzzleflash;

    Animation anim;

    public GameObject kamera, raycast;

    public GameObject TahtaDelik;
    public GameObject TasDelik;
    public GameObject MetalDelik;

    public Vector3 NormalP;
    public Vector3 NisanP;
    public Quaternion NisanR;

    private void Start()
    {
        mermisayisi.text = Mermi_Sayisi.ToString();
        sarjor.text = Sarjor.ToString();

        anim = gameObject.GetComponent<Animation>();

    //    anim.Play("AkCikar");
    }

    void FixedUpdate()
    {
        if (fire == true && Time.time > zamanlayici && (Input.GetMouseButton(0)) && !Breload)
        {
            if (Mermi_Sayisi > 0)
            {
                AtesEt();
                zamanlayici = Time.time + sıkma_aralıgı;
            }
            else if (Sarjor > 0 && Mermi_Sayisi < 30)
            {
                Reload();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Mermi_Sayisi <= 0 && Sarjor <= 0)
            {
                sfx.clip = Bos;
                sfx.Play();
            }
        }

        if(Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, NisanP, 8f * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, NisanR, 8f * Time.deltaTime);
            scope = true;
            crosshair.enabled = false;

            Camera cam = kamera.GetComponent<Camera>();
            cam.fieldOfView = 50;
        }
        else
        {
            scope = false;
            crosshair.enabled = true;
            Camera cam = kamera.GetComponent<Camera>();
            cam.fieldOfView = 60;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Sarjor > 0 && Mermi_Sayisi < 30)
            {
                Reload();
            }
        }

        crosshair.color = Color.white;
        RaycastHit hit;
        if (Physics.Raycast(kamera.transform.position, kamera.transform.forward, out hit, Mesafe))
        {
            if (hit.distance <= Mesafe && hit.collider.gameObject.tag == "Enemy")
                crosshair.color = Color.red;
        }

    }

    public void AtesEt()
    {
        muzzleflash.Play();

    //    sfx.clip = ak;
   //     sfx.Play();

        Mermi_Sayisi--;
        mermisayisi.text = Mermi_Sayisi.ToString();

        //if(scope == false)
        //    anim.Play("Ates");
        //else
        //    anim.Play("ScopeAtes");

        RaycastHit hit;
        if (Physics.SphereCast(raycast.transform.position, AimAssist, raycast.transform.forward, out hit))
        {
            raycast.transform.position += new Vector3(Random.Range(-0.15f, 0.15f), Random.Range(-0.2f, 0.15f),0);

         //   DusmanAsker dusmanasker = hit.transform.GetComponent<DusmanAsker>();
        //    if(dusmanasker != null)
         //   {
        //        dusmanasker.HasarVer(Hasar);
        //    }

        }

        if (Physics.Raycast(raycast.transform.position, raycast.transform.forward, out hit, Mesafe))
        {
            if (hit.distance <= Mesafe && hit.collider.gameObject.tag == "Tahta")
                Instantiate(TahtaDelik, hit.point, Quaternion.LookRotation(hit.normal));
            if (hit.distance <= Mesafe && hit.collider.gameObject.tag == "Tas")
                Instantiate(TasDelik, hit.point, Quaternion.LookRotation(hit.normal));
            if (hit.distance <= Mesafe && hit.collider.gameObject.tag == "Metal")
                Instantiate(MetalDelik, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    public void Reload ()
    {
        Sarjor--;
        Mermi_Sayisi = Sarjor_mermi;
        mermisayisi.text = Mermi_Sayisi.ToString();
        sarjor.text = Sarjor.ToString();
        anim.Play("Reload");
        Breload = true;
        StartCoroutine(SDegis());
    }

    IEnumerator SDegis ()
    {
        yield return new WaitForSeconds(0.5f);
        sfx.clip = reload;
        sfx.Play();
        yield return new WaitForSeconds(1f);
        sfx.clip = reload;
        sfx.Play();
        yield return new WaitForSeconds(0.50f);
        sfx.clip = reload;
        sfx.Play();
        yield return new WaitForSeconds(0.50f);
        Breload = false;
    }

    public void yenile ()
    {
        mermisayisi.text = Mermi_Sayisi.ToString();
        sarjor.text = Sarjor.ToString();
    }
}


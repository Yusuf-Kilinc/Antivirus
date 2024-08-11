using TMPro;
using UnityEngine;

public class FireAttack : MonoBehaviour
{

    public float Mermi, Sarjor, FullBullet, Menzil, Hasar1, Hasar2, NewBullet, FireTime, sayi, Zmn, MaxZmn;
    public bool Fire, Reload;
    RaycastHit hit;
    public TextMeshProUGUI Bullettext;
    float asdasdasdas;
    private void Start()
    {
        Zmn = MaxZmn;
    }
    void Update()
    {
        Bullettext.text = "" + Mermi + "/" + FullBullet;

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
        if (Reload == true && Mermi == 30)
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
    }
    private void FixedUpdate()
    {
        if (Fire)
        {
            Fire = false;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Menzil))
            {
                if (hit.transform.tag == "Enemy")
                {
                    Debug.Log("Dusman Temas");
                }
            }
        }
    }
}

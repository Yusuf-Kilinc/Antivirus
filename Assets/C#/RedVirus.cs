using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RedVirus : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject Player;
    float mesafe;
    bool fire;
    public Transform CikisNok;
    public GameObject Bullet;
    public float AtisZaman;
    bool AtesHazirmi;
    public AudioSource ses;
    public AudioClip fireTrue;
    public float AtesZamanlayici;
    public string enemyColor;
    public float Health = 100;
    private void Start()
    {
        AtesHazirmi = true;
    }

    private void FixedUpdate()
    {
        if (Player != null)
        {
            mesafe = Vector3.Distance(this.transform.position, Player.transform.position);
        }
    }

    void Update()
    {
        if (Player != null)
        {
            if (mesafe > 5)
            {
                agent.SetDestination(Player.transform.position);
            }
            if (mesafe < 5)
            {
                fire = true;
            }
        }else if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }
        if (fire)
        {
            Saldir();
        }
        if (Health <= 0)
        {
            StartCoroutine(YokEt());
        }
    }
    public void Saldir()
    {
        if (AtesHazirmi == true)
        {
            Transform _mermi = Instantiate(Bullet.transform, CikisNok.transform.position, Quaternion.identity);
            _mermi.transform.rotation = CikisNok.transform.rotation;
            ses.clip = fireTrue;
            ses.Play();
            fire = true;
            AtesHazirmi = false;
            StartCoroutine(AtesAralik());
        }
        else
        {
            fire = false;
        }
    }
    IEnumerator AtesAralik()
    {
        yield return new WaitForSeconds(AtesZamanlayici);
        AtesHazirmi = true;
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            StartCoroutine(YokEt());
        }
    }

    IEnumerator YokEt()
    {
        yield return new WaitForSeconds(0.3f);
       // cashMoney = GameObject.FindWithTag("Master").GetComponent<Cash>();
       // cashMoney.CashMoney += 25;
        Destroy(gameObject);
    }

}
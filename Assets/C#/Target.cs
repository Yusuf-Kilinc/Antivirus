using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public float Health = 100;
    public GameObject Hedef;
    public NavMeshAgent agent;
    public Animator anim;
    public bool attack = false;
    public float growthFactor = 1.2f;
    public Cash cashMoney;
    public string enemyColor; // "Red", "Green", "Blue" olarak ayarlanacak
    void Update()
    {
        Hedef = GameObject.FindWithTag("Player");
        anim.SetBool("Attack", attack);
        float Distance = Vector3.Distance(transform.position, Hedef.transform.position);

        if (Health <= 0)
        {
            StartCoroutine(YokEt());
        }
        else
        {
            agent.SetDestination(Hedef.transform.position);
        }
        attack = Distance < 3;
    }
    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            StartCoroutine(YokEt());
        }
    }
    public void GainHealth(float amount)
    {
        Health += amount;
    }
    void Grow()
    {
        transform.localScale *= growthFactor;
        Health += 100;
    }
    IEnumerator YokEt()
    {
        yield return new WaitForSeconds(0.3f);
        cashMoney = GameObject.FindWithTag("Master").GetComponent<Cash>();
        cashMoney.CashMoney += 25;
        Destroy(gameObject);
    }
}

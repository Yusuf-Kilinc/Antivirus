using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BlueAI : MonoBehaviour
{
    public float Health = 100;
    GameObject Hedef;
    public NavMeshAgent agent;
    public Animator anim;
    public bool attack = false;
    public float growthFactor = 1.2f;
    Cash cashMoney;
    void Update()
    {
        Hedef = GameObject.FindWithTag("Player");
        anim.SetBool("Attack", attack);
        float Distance = Vector3.Distance(this.transform.position, Hedef.transform.position);
        if (Health <= 0)
        {
            
            StartCoroutine("YokEt");
        }
        else if (Health > 0)
        {
            agent.SetDestination(Hedef.transform.position);
        }
        if (Distance < 3)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
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
        Destroy(this.gameObject);
    }
}

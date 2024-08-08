using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GreenAI : MonoBehaviour
{
    public float Health = 100;
    GameObject Hedef;
    public NavMeshAgent agent;
    public Animator anim;
    public bool attack = false;
    public float growthFactor = 1.2f;
    Cash cashMoney;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Green")
        {
            Health = 0;
        }
        else if (other.gameObject.tag == "Red" || other.gameObject.tag == "Blue")
        {
            Grow();
        }
    }
    void Update()
    {
        Hedef = GameObject.FindWithTag("Player");
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
        if (attack)
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
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

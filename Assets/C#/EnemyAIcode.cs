using UnityEngine;
using UnityEngine.AI;

public class EnemyAIcode : MonoBehaviour
{
    public float Health = 100;
    GameObject Hedef;
    public NavMeshAgent agent;
    public Animator anim;
    public bool attack = false;


    void Update()
    {
        Hedef = GameObject.FindWithTag("Player");
        float Distance = Vector3.Distance(this.transform.position, Hedef.transform.position);
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("Object destroyed"); // Nesnenin yok olup olmadýðýný kontrol edin.
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
}

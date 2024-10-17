using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class VirusBullet : MonoBehaviour
{
    public float hareketHizi;
    public float Hasar;
    GameObject Hedef;
    public AudioSource ses;
    public AudioClip sound;
    public int hasar;
    HealthSystem HSystem;
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * hareketHizi);
        StartCoroutine("YOKET");
        ses.volume = 0.5f;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HSystem = GameObject.FindWithTag("Player").GetComponent<HealthSystem>();
       //     HSystem = GetComponent<HealthSystem>();
            HSystem.TakeDamage(hasar);
        }
    }
    IEnumerator YOKET()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
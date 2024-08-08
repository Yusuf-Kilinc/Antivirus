using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float hareketHizi;
    GameObject Hedef;
    Material mat;
    public GameObject DeathObj;
    public void Start()
    {
        mat = GetComponent<Material>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * hareketHizi);
        StartCoroutine("YOKET");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Hedef = other.gameObject;
            Transform Konum= other.transform;
            Instantiate(DeathObj, Konum);
            StartCoroutine("YOKET");
           // Destroy(this.gameObject);
        }
    }

    IEnumerator YOKET()
    {
        yield return new WaitForSeconds(5);
        Destroy(Hedef);
    }
}

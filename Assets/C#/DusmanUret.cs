using System;
using System.Collections;
using UnityEngine;

public class DusmanUret : MonoBehaviour
{
    public GameObject RedEnemy, GreenEnemy, BlueEnemy;
    public BakteriCanSistemi BCS;
    public float DusmanSure = 10f;

    private void Update()
    {
        if (BCS.Health > 0)
        {
            DusmanSure -= Time.deltaTime;
            if (DusmanSure <= 0)
            {
                StartCoroutine(SpawnObject());
                DusmanSure = 10f;
            }
        }
    }

    IEnumerator SpawnObject()
    {
        int i = UnityEngine.Random.Range(0, 10);
        if (i >= 0 && i < 2)
        {
            Instantiate(RedEnemy, transform.position, Quaternion.identity);
        }
        else if (i >= 3 && i <= 6)
        {
            Instantiate(GreenEnemy, transform.position, Quaternion.identity);
        }
        else if (i >= 7 && i <= 10)
        {
            Instantiate(BlueEnemy, transform.position, Quaternion.identity);
        }
        yield return null; // Coroutine'in çalýþmasý için bir duraklama gerekiyor
    }
}

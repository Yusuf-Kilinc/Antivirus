using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Spawner : MonoBehaviour
{
    public GameObject RedEnemy, GreenEnemy;
    // public float DusmanSure = 10f;
    public float DusmanSure;
    public float DusmanMaxSure;
    public float Health = 100;
    public Slider HealthSlider;
   
    void Update()
    {
        Health = Mathf.Clamp(Health, 0, 100);
        HealthSlider.value = Health;
        DusmanMaxSure = Health / 10;
        if (Health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            DusmanSure -= Time.deltaTime;
            if (DusmanSure <= 0)
            {
                StartCoroutine(SpawnObject());
                DusmanSure = DusmanMaxSure;
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
        yield return null;
    }
}
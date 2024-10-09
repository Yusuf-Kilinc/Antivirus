using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject RedEnemy, GreenEnemy, BlueEnemy;
    public float DusmanSure = 10f;
    public float Health = 100, GreenCont = 0;
    public bool Green = false;
    public int i;
    public NextLevelDoor NLDoor;
    public List<GameObject> Objeler;
    public Material GreenMat;
    public Light Redligt;
    public Slider slider;
    float NextLvl = 1;
    private bool hasIncrementedNLDoor = false;

    public MeshRenderer SliderColorObject;
    public Image SliderBackground;

    private void Start()
    {
      //  SliderColorObject = GetComponent<MeshRenderer>();
        
    }


    private void Update()
    {
        Health = Mathf.Clamp(Health, 0, 100);
        slider.value = Health;
        SliderBackground.color = SliderColorObject.material.color;


        if (NextLvl == 0 && !hasIncrementedNLDoor)
        {
            NLDoor.i += 1;
            NextLvl = 1;
            hasIncrementedNLDoor = true; // Artýrmadan sonra bayraðý true yap
        }
        if (Health > 0)
        {
            Redligt.range = Health * 0.1f;
        }
        if (Green)
        {
            Redligt.color = Color.green;
            Redligt.range = 0.1f * GreenCont;
            if (GreenCont <= 100)
            {
                GreenCont += 2.5f * Time.deltaTime;
            }
        }
        if (Health <= 0)
        {
            StartCoroutine("YokEt");
        }
        if (!Green && Health > 0)
        {
            DusmanSure -= Time.deltaTime;
            if (DusmanSure <= 0)
            {
                StartCoroutine(SpawnObject());
                DusmanSure = 10f;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Health -= 5 * Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Health > 0)
            {
                Health += 1 * Time.deltaTime;
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
    IEnumerator YokEt()
    {
        yield return new WaitForSeconds(2);
        NextLvl = 0;

        for (int i = 0; i < Objeler.Count; i++)
        {
            Objeler[i].gameObject.GetComponent<MeshRenderer>().material = GreenMat;
            Green = true;
        }
    }
}

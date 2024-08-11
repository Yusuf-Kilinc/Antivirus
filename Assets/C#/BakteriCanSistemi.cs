using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BakteriCanSistemi : MonoBehaviour
{
    public float Health = 100, GreenCont = 0;
    public bool Green = false;
    public int i;
    public NextLevelDoor NLDoor;
    public List<GameObject> Objeler;
    public Material GreenMat;
    public Light Redligt;
    public Slider slider;
    public GameObject SliderTrueFalse;
    float NextLvl = 1;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Health -= 5 * Time.deltaTime;
            SliderTrueFalse.SetActive(true);
            slider.value= Health;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Health > 0)
            {
                Health += 1 * Time.deltaTime;
                SliderTrueFalse.SetActive(false);
            }
        }
    }
    void Update()
    {
        Health = Mathf.Clamp(Health, 0, 100);

        if (NextLvl == 0)
        {
            NLDoor.i += 1;
            NextLvl = 1;
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

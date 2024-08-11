using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girisDoor : MonoBehaviour
{
    public Animation anim;
    float Sayi = 0;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            if (Sayi == 0)
            {
                anim.Play();
                Sayi = 1;
            }
        }
    }
}

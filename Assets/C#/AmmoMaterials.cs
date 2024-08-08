using UnityEngine;
using UnityEngine.EventSystems;

public class AmmoMaterials : MonoBehaviour, IPointerDownHandler
{
    public WeaponCode Anamermi;
    public GameObject NewAmmo;
    public AudioSource ses;
    public AudioClip sound;

    public void OnPointerDown(PointerEventData eventData)
    {
      //      Anamermi.Mermi = NewAmmo;
            ses.clip = sound;
            ses.PlayOneShot(sound); 
    }
}

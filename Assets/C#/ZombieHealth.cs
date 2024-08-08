using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public GameObject MainObje;
    public float Health;
    public Material objectMaterial;
    public float growthFactor = 1.2f;
    float asdsadsa;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ammo"))
        {
            Renderer otherRenderer = other.gameObject.GetComponent<Renderer>();
            if (otherRenderer != null)
            {
                Material mat = otherRenderer.material;
                Debug.Log($"Collided with Ammo. Material: {mat.name}"); // Malzemenin adýný kontrol edin.
                if (mat == this.objectMaterial)
                {
                    Health -= 100;
                    Debug.Log($"Health decreased to {Health}"); // Health deðerini kontrol edin.
                }
                else
                {
                    Grow();
                    Debug.Log("Object grew"); // Grow metodunun çalýþýp çalýþmadýðýný kontrol edin.
                }
            }
            else
            {
                Debug.LogError($"The object that triggered doesn't have a Renderer component. Object name: {other.gameObject.name}");
            }
        }
    }
    void Grow()
    {
        transform.localScale *= growthFactor;
    }
    void Start()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            objectMaterial = renderer.material;
        }
        else
        {
            Debug.LogError("No Renderer found in the object or its children.");
        }
    }

    public void Update()
    {
        if (Health <= 0)
        {
            Destroy(MainObje); 
        }
    }


}

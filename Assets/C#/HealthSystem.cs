using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public Volume volume;
    private Vignette vignette;

    void Start()
    {
        // Oyuncunun can�n� maksimuma ayarla
        currentHealth = maxHealth;

        // Volume'den Vignette efektini al
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = 0f; // Ba�lang��ta Vignette yo�unlu�u s�f�r olmal�
        }
    }

    public void Update()
    {
        float vignetteIntensity = Mathf.Lerp(0f, 0.4f, 1 - (float)currentHealth / maxHealth);
        vignette.intensity.value = vignetteIntensity;
        if (currentHealth < maxHealth)
        {
            currentHealth += 1 * Time.deltaTime;
        }
    }


    public void TakeDamage(int damageAmount)
    {
        // Hasar al
        currentHealth -= damageAmount;

        // Can 0'�n alt�na d��mesin
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Can miktar�na g�re Vignette yo�unlu�unu ayarla
        //   AdjustVignette();



        

        // Oyuncu �ld� m� kontrol et
        if (currentHealth <= 0)
        {
         //   Die();
        }
    }
    void AdjustVignette()
    {
        // Vignette yo�unlu�unu can miktar�na g�re ayarla
        float vignetteIntensity = Mathf.Lerp(0f, 0.4f, 1 - (float)currentHealth / maxHealth);
        vignette.intensity.value = vignetteIntensity;
    }
    void Die()
    {
        // Oyuncu �ld���nde yap�lacaklar
        Debug.Log("Player is dead!");
    }
}

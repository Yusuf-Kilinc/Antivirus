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
        // Oyuncunun canýný maksimuma ayarla
        currentHealth = maxHealth;

        // Volume'den Vignette efektini al
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            vignette.intensity.value = 0f; // Baþlangýçta Vignette yoðunluðu sýfýr olmalý
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

        // Can 0'ýn altýna düþmesin
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Can miktarýna göre Vignette yoðunluðunu ayarla
        //   AdjustVignette();



        

        // Oyuncu öldü mü kontrol et
        if (currentHealth <= 0)
        {
         //   Die();
        }
    }
    void AdjustVignette()
    {
        // Vignette yoðunluðunu can miktarýna göre ayarla
        float vignetteIntensity = Mathf.Lerp(0f, 0.4f, 1 - (float)currentHealth / maxHealth);
        vignette.intensity.value = vignetteIntensity;
    }
    void Die()
    {
        // Oyuncu öldüðünde yapýlacaklar
        Debug.Log("Player is dead!");
    }
}

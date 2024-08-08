using UnityEngine;

public class LaserSystem : MonoBehaviour
{
    public float maxDistance = 30f; // Lazerin maksimum menzili
    public LayerMask collisionLayer; // Hangi katmanlarla çarpýþma kontrolü yapýlacak
    public Transform laserImage; // Lazerin görseli

    void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        Vector3 laserDirection = transform.forward; // Lazerin yönü
        Ray ray = new Ray(transform.position, laserDirection);
        RaycastHit hit;

        // Raycast ile çarpýþma kontrolü
        if (Physics.Raycast(ray, out hit, maxDistance, collisionLayer))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red); // Çarpýþma noktasýný çiz

            // Lazerin boyutunu çarpýþma noktasýna kadar uzat
            float distance = hit.distance;
            laserImage.localScale = new Vector3(distance, laserImage.localScale.y, laserImage.localScale.z);
        }
        else
        {
            // Çarpýþma olmadýysa maksimum mesafeye kadar lazeri uzat
            Debug.DrawLine(ray.origin, ray.origin + laserDirection * maxDistance, Color.green);
            laserImage.localScale = new Vector3(maxDistance, laserImage.localScale.y, laserImage.localScale.z);
        }
    }
}

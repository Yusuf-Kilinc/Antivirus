using UnityEngine;

public class LaserSystem : MonoBehaviour
{
    public float maxDistance = 30f; // Lazerin maksimum menzili
    public LayerMask collisionLayer; // Hangi katmanlarla �arp��ma kontrol� yap�lacak
    public Transform laserImage; // Lazerin g�rseli

    void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        Vector3 laserDirection = transform.forward; // Lazerin y�n�
        Ray ray = new Ray(transform.position, laserDirection);
        RaycastHit hit;

        // Raycast ile �arp��ma kontrol�
        if (Physics.Raycast(ray, out hit, maxDistance, collisionLayer))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.red); // �arp��ma noktas�n� �iz

            // Lazerin boyutunu �arp��ma noktas�na kadar uzat
            float distance = hit.distance;
            laserImage.localScale = new Vector3(distance, laserImage.localScale.y, laserImage.localScale.z);
        }
        else
        {
            // �arp��ma olmad�ysa maksimum mesafeye kadar lazeri uzat
            Debug.DrawLine(ray.origin, ray.origin + laserDirection * maxDistance, Color.green);
            laserImage.localScale = new Vector3(maxDistance, laserImage.localScale.y, laserImage.localScale.z);
        }
    }
}

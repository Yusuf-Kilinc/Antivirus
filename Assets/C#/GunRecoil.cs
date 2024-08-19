using System.Collections;
using UnityEngine;
public class GunRecoil : MonoBehaviour
{
    public Transform gunTransform; // Silahýn baðlý olduðu nesne
    public float recoilAmount = 1f; // Tepmenin þiddeti
    public float recoilSpeed = 10f; // Tepme hareketinin hýzý
    public float returnSpeed = 5f; // Silahýn geri gelme hýzý
    private Vector3 originalPosition; // Silahýn baþlangýç pozisyonu
    void Start()
    {
        originalPosition = gunTransform.localPosition; // Silahýn baþlangýç pozisyonunu sakla
    }
    public void ApplyRecoil()
    {
        StopAllCoroutines(); // Diðer recoil hareketlerini durdur
        StartCoroutine(RecoilCoroutine()); // Tepme hareketini baþlat
    }
    private IEnumerator RecoilCoroutine()
    {
        // Tepme hareketi
        Vector3 recoilPosition = originalPosition + Vector3.left * recoilAmount;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * recoilSpeed;
            gunTransform.localPosition = Vector3.Lerp(originalPosition, recoilPosition, elapsedTime);
            yield return null;
        }

        // Geri dönme hareketi
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * returnSpeed;
            gunTransform.localPosition = Vector3.Lerp(recoilPosition, originalPosition, elapsedTime);
            yield return null;
        }
    }
}

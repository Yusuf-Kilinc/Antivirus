using System.Collections;
using UnityEngine;
public class GunRecoil : MonoBehaviour
{
    public Transform gunTransform; // Silah�n ba�l� oldu�u nesne
    public float recoilAmount = 1f; // Tepmenin �iddeti
    public float recoilSpeed = 10f; // Tepme hareketinin h�z�
    public float returnSpeed = 5f; // Silah�n geri gelme h�z�
    private Vector3 originalPosition; // Silah�n ba�lang�� pozisyonu
    void Start()
    {
        originalPosition = gunTransform.localPosition; // Silah�n ba�lang�� pozisyonunu sakla
    }
    public void ApplyRecoil()
    {
        StopAllCoroutines(); // Di�er recoil hareketlerini durdur
        StartCoroutine(RecoilCoroutine()); // Tepme hareketini ba�lat
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

        // Geri d�nme hareketi
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * returnSpeed;
            gunTransform.localPosition = Vector3.Lerp(recoilPosition, originalPosition, elapsedTime);
            yield return null;
        }
    }
}

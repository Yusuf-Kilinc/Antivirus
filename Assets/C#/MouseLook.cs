using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Range(1, 200)]
    public float sens;
    public Transform body;
    float xRot = 0f;

    public float Smoothing;
    float CurrenRot;

    public FixedTouchField ftf;

    [HideInInspector]
    public Vector2 LookAxis;

    void Start()
    {
     // ftf = GetComponent<FixedTouchField>();
    }


    private void Update() 
    {
        #region Camera Movement
        float rotX = /*LookAxis.x*/ftf.TouchDist.x * sens * Time.deltaTime;
        float rotY = /*LookAxis.y*/ftf.TouchDist.y * sens * Time.deltaTime;


        xRot -= rotY;
        xRot = Mathf.Clamp(xRot, -35f, 25f);
        CurrenRot += rotX;
        CurrenRot = Mathf.Lerp(CurrenRot, 0, Smoothing * Time.deltaTime);


        transform.localRotation = Quaternion.Euler(xRot, 0f, CurrenRot);
        body.Rotate(Vector3.up * rotX);
        #endregion
    }
}

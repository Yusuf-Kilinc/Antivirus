using UnityEngine;

public class PanelScreenKontrol : MonoBehaviour
{
    public GameObject Panel;
    public void PanelAc()
    {
        Panel.SetActive(true);
    }
    public void PanelKapat()
    {
        Panel.SetActive(false);
    }
}

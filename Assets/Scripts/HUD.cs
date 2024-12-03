using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TMP_Text hudText; // Asigna tu TextMesh Pro desde el Inspector.

    void Start()
    {
        UpdateHUD("0");
    }

    public void UpdateHUD(string newText)
    {
        hudText.text = newText;
    }
}
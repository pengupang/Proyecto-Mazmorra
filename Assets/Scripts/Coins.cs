using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] TMP_Text hudText; // Asigna tu TextMesh Pro desde el Inspector.
    private int contador = 0;
    
   private void OnTriggerEnter(Collider other)
{
    Debug.Log($"Colisión detectada con: {other.gameObject.name}");

    if (other.gameObject.tag == "moneda")
    {
        Debug.Log("Moneda recogida!");
        Destroy(other.gameObject);
        contador++;
        hudText.text = $"{contador}";
    }
}
}
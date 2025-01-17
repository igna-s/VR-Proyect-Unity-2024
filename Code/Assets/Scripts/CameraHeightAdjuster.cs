using UnityEngine;
public class CameraHeightAdjuster : MonoBehaviour
{
    public Transform cameraTransform;  // Referencia a la cámara
    public float heightOffset = 1.8f;  // Altura en metros

    void Start()
    {
        // Ajustar la posición de la cámara
        cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, heightOffset, cameraTransform.localPosition.z);
    }
}

using UnityEngine;
using TMPro; // A�adir la referencia a TextMeshPro
using System;
using System.Collections;


public class ChangeHour : MonoBehaviour
{
    public TextMeshProUGUI sharedText; // Usar TextMeshProUGUI en lugar de Text

    private void Start()
    {
        // Iniciar la actualizaci�n del texto cada minuto
        StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        while (true)
        {
            // Obtener la hora y los minutos actuales
            DateTime now = DateTime.Now;
            sharedText.text = now.ToString("HH:mm");

            // Esperar un minuto antes de actualizar nuevamente
            yield return new WaitForSeconds(60);
        }
    }

    public void OnLeftButton()
    {
        // M�todo sin comportamiento
    }

    public void OnRightButton()
    {
        // M�todo sin comportamiento
    }
}
using UnityEngine;
using TMPro;
using System;

public class ChangeDayOfWeek : MonoBehaviour
{
    public TextMeshProUGUI sharedText;

    void Start()
    {
        // Obtener la fecha y la hora actuales
        DateTime now = DateTime.Now;

        // Formatear el d�a de la semana y la fecha
        string dayOfWeek = now.ToString("dddd");
        string date = now.ToString("dd/MM");

        // Asegurarse de que el primer car�cter del d�a est� en may�scula
        dayOfWeek = char.ToUpper(dayOfWeek[0]) + dayOfWeek.Substring(1);

        // Construir el texto final
        sharedText.text = $"{dayOfWeek} {date}";
    }

    public void OnLeftButton()
    {
        // Vac�o
    }

    public void OnRightButton()
    {
        // Vac�o
    }
}

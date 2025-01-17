using UnityEngine;
using TMPro;

public class Cartel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI id; // Referencia al componente TextMeshProUGUI para el ID del cartel

    [SerializeField]
    private TextMeshProUGUI texto; // Referencia al componente TextMeshProUGUI para el texto del cartel

    // Getter y Setter para el texto del ID
    public string IdText
    {
        get { return id.text; }
        set { id.text = value; }
    }

    // Getter y Setter para el texto del Cartel
    public string TextoContent
    {
        get { return texto.text; }
        set { texto.text = value; }
    }

    // Método para configurar los valores del Cartel, tanto ID como texto
    public void Configurar(string idText, string textoContent)
    {
        IdText = idText;
        TextoContent = textoContent;
    }

    // Método para configurar solo el texto del Cartel
    public void Configurar(string textoContent)
    {
        TextoContent = textoContent;
    }
}

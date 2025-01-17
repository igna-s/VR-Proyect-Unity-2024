using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class CartelManager : MonoBehaviour
{
    [SerializeField] private List<Cartel> carteles = new List<Cartel>();
    private List<Clase> clases = new List<Clase>();
    private JsonManager j;
    private string mensaje_defecto = "No se dispone información de la cartelera";

    void Start()
    {
        j = GetComponent<JsonManager>(); // Initialize JsonManager
        if (j == null)
        {
            Debug.LogError("JsonManager component not found on the GameObject.");
            return;
        }

        StartCoroutine(FetchAndAssignData()); // Fetch and assign data asynchronously
        InvokeRepeating("ActualizarCartel", 0f, 60f);
    }

    private IEnumerator FetchAndAssignData()
    {
        // Fetch data and assign to carteles
        yield return StartCoroutine(j.GetClaseData());
        clases = j.GetClases(); // GetClases should be updated to handle async operation correctly

        Asignacion(); // Assign values to carteles
    }

    private void Asignacion()
    {
        ValoresDefecto();

        for (int i = 0; i < clases.Count; i++)
        {
            string nombreAula = clases[i].Aula.Nombre;
            nombreAula= nombreAula.Replace(" ","");
            nombreAula= nombreAula.Replace("-","x");
            Debug.Log(nombreAula);
            int index = Conversion(nombreAula);

            if (index >= 0 && index < carteles.Count) // Ensure the index is valid
            {
                carteles[index].Configurar( clases[i].ToString());
            }
            else
            {
                Debug.LogError($"Invalid index {index} for Aula: {nombreAula}");
            }
        }
    }

    private void ValoresDefecto()
    {
        
        foreach (var cartel in carteles)
        {
            
            
            cartel.Configurar(mensaje_defecto);
          
        }
    }

    private void ActualizarCartel()
    {
        // Obtener la hora actual
        DateTime now = DateTime.Now;

        // Verificar si la hora actual es en punto o y media + 1 minuto
        if ((now.Minute == 1 || now.Minute == 31) && now.Second == 0)
        {
            StartCoroutine(FetchAndAssignData()); // Fetch and assign data asynchronously
            Debug.Log("Carteles actualizados a las " + now.ToString("HH:mm"));
        }
    }

    private int Conversion(string nombreAula)
    {
        try
        {
            if (Enum.TryParse(typeof(Aulas), nombreAula, out var resultado))
            {
                Debug.Log((int)resultado);
                return (int)resultado;
            }
            else
            {
                throw new ArgumentException("El nombre del aula no es v�lido.");
            }
        }
        catch (ArgumentException ex)
        {
            Debug.LogError(ex.Message);
            // Retornar un valor predeterminado o manejar el error de otra forma seg�n tu l�gica
            return -1; // Por ejemplo, retornar -1 para indicar que hubo un error
        }
    }
}

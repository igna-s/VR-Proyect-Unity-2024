using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System;

public class JsonManager : MonoBehaviour
{
    private string url = "https://gestiondocente.info.unlp.edu.ar/reservas/api/consulta/estadoactual";

    // Variable de instancia para almacenar la lista de clases
    private List<Clase> clases;


    // M�todo que retorna la lista de clases
    public List<Clase> GetClases()
    {
        Debug.Log("Start method called");
        StartCoroutine(GetClaseData());
        return clases;
    }

    public IEnumerator GetClaseData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                Debug.Log("JSON received: " + json);

                // Deserializa y asigna a la variable de instancia
                clases = JsonConvert.DeserializeObject<List<Clase>>(json);

                foreach (Clase clase in clases)
                {
                    Debug.Log(clase.ToString());
                }
            }
            else
            {
                Debug.LogError($"Error: {request.error}");
            }
        }
    }
}
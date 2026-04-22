using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        
        LoadGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData();
        {
            saveData.playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            //saveData.mapBoundry = FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D.gameObject.name;
        }
        
            try
            {
                File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Failed to save game: " + ex.Message);
            }
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
 
            //FindObjectOfType<CinemachineConfiner>().m_BoundingShape2D = GameObject.Find(saveData.mapBoundry).GetComponent<PolygonCollider2D>();;
        }
        else
        {
            SaveGame();
        }
    }

}
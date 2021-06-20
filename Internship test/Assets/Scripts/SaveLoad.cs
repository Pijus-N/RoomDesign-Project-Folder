using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

public class SaveLoad : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckSavedFiles()
	{
        if (File.Exists(Application.persistentDataPath
                       + "/SaveFiles.dat"))
        {
            
            return true;
        }
        return false;

    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
                     + "/SaveFiles.dat");
        SaveData data = new SaveData();
        
        data.gameObjects =MakeStringList(GetObjectsInScene());
        bf.Serialize(file, data);
        file.Close();
        
    }


   public List<string> LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
                       + "/SaveFiles.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(Application.persistentDataPath
                       + "/SaveFiles.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            
            
            return data.gameObjects;
        }
		else
		{
           
            return null;
            
        }
            
        
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
                      + "/SaveFiles.dat"))
        {
            File.Delete(Application.persistentDataPath
                              + "/SaveFiles.dat");
            
            
        }
        
    }

    /// <summary>
    /// Gets all objects in scene in order to save them
    /// </summary>
    /// <returns></returns>
    private List<GameObject> GetObjectsInScene()
	{
        List<GameObject> objectsList = new List<GameObject>();
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");
        for(int i=0; i<objects.Length; i++)
		{
            objectsList.Add(objects[i]);
            
		}
        return objectsList;
	}

    private List<string> MakeStringList(List<GameObject> objects)
	{
        List<string> stringList = new List<string>();
        foreach(GameObject obj in objects)
		{
            StringBuilder builder = new StringBuilder();
            builder.Append(obj.transform.position.ToString("F5"));
            builder.Append(";");
            builder.Append(obj.transform.rotation.eulerAngles.ToString());
            builder.Append(";");
            builder.Append(obj.GetComponent<Renderer>().material.color.ToString());
            builder.Append(";");
            builder.Append(obj.name);
            stringList.Add(builder.ToString());

           
        }

        return stringList;
	}

    


}

[Serializable]
class SaveData
{
    
    public List<string> gameObjects;



}

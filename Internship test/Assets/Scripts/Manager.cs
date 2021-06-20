using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    #region variables
    //SCRIPTS
    [SerializeField] private TransitionsManager transitions;
    [SerializeField] private SaveLoad saveLoad;
    [SerializeField] private SceneEditor sceneEditor;
    [SerializeField] private BuildingPanel buildingPanel;
    //SCRIPTS

    //GAMEOBJECTS
    [SerializeField] private GameObject Builder;

    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject SceneCanvas;
    [SerializeField] private GameObject TutorialCanvas;
    [SerializeField] private GameObject FirstPersonCharacter;
    [SerializeField] private GameObject spawnPoint;
    //GAMEOBJECTS

    //UI
    [SerializeField] private GameObject BuildingPanel;
    [SerializeField] private Button BuildButton;
    [SerializeField] private Button previewButton;

    [SerializeField] private GameObject EscapeButtonImage;
    [SerializeField] private GameObject RButtonImage;
    //UI

    #endregion




    // Start is called before the first frame update
    void Start()
    {
        
        transitions = gameObject.GetComponent<TransitionsManager>();
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterBuildMode()
	{
        
        TutorialCanvas.SetActive(true);
        RButtonImage.SetActive(true);
        EscapeButtonImage.SetActive(false);

        transitions.EnterBuildMode();
        BuildButton.interactable = false;
        StartCoroutine(EnterBuildMode(1f));
        
    }

    public void EnterPreviewMode()
	{
       
        TutorialCanvas.SetActive(false) ;
        

        transitions.EnterPreviewMode();
        previewButton.interactable = false;
        Builder.GetComponent<PlaceGhost>().DeleteGameObject();
        Builder.SetActive(false);
        BuildingPanel.SetActive(false);
        StartCoroutine(EnterPreviewMode(1f));

    }

    public void EnterExploreMode()
	{
        
        TutorialCanvas.SetActive(true);
        EscapeButtonImage.SetActive(true);
        RButtonImage.SetActive(false);

        Builder.GetComponent<PlaceGhost>().DeleteGameObject();
        Cursor.lockState = CursorLockMode.Locked;
        FirstPersonCharacter.SetActive(true);
        SceneCanvas.SetActive(false);
        
    }

    public void EnterSceneView()
	{
        spawnPoint.GetComponent<SpawnObjectsInMainmenu>().enabled = false;
        if (FirstPersonCharacter.activeSelf)
		{
            FirstPersonCharacter.SetActive(false);
            FirstPersonCharacter.transform.position = spawnPoint.transform.position;

        }
        MainMenuCanvas.SetActive(false);
        SceneCanvas.SetActive(true);
	}

    public void BackToMenu()
	{
        spawnPoint.GetComponent<SpawnObjectsInMainmenu>().enabled = true;
        spawnPoint.GetComponent<SpawnObjectsInMainmenu>().Reset();

        TutorialCanvas.SetActive(false);

        SceneCanvas.SetActive(false);
        saveLoad.SaveGame();
        EnterPreviewMode();
        StartCoroutine(BackToMenu(1f));
        
       
        
    }

    public void CleanTheScene()
	{
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Object");
        foreach(GameObject obj in objects)
		{
            Destroy(obj);
		}
	}

    public void DeleteSavedData()
	{
        saveLoad.ResetData();
    }

    IEnumerator EnterBuildMode(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        previewButton.interactable = true;

        Builder.SetActive(true);
        BuildingPanel.SetActive(true);
        BuildingPanel.GetComponent<BuildingPanel>().BuildMode();
    }

    IEnumerator EnterPreviewMode(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        BuildButton.interactable = true;
        
    }

    IEnumerator BackToMenu(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        MainMenuCanvas.SetActive(true);
        gameObject.GetComponent<MainMenu>().MenuUpdate();
    }

    IEnumerator EnterExploreMode(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
    }

    public void LoadScene()
	{
        LoadObjectsToScene(saveLoad.LoadGame());
	}

    public bool CheckSavedFiles()
	{
        return saveLoad.CheckSavedFiles();
	}

	void LoadObjectsToScene(List<string> objects)
	{
        foreach(string str in objects)
		{
            Tuple<Vector3, Vector3, Color, string> values = ReturnAndParseSavedValues(str);

            

            buildingPanel.SetByName(values.Item4);


            sceneEditor.LoadGameObjects(values.Item1, values.Item2, values.Item3);
		}




	}

    private  Tuple<Vector3, Vector3, Color, string> ReturnAndParseSavedValues(string data)
    {
        
        string[] words = data.Split(';');

        for(int i=0; i<words.Length; i++)
		{
            
            words[i] =  words[i].Trim(' ', '(', ')');
		}
       
        
        string[] values = words[0].Split(',');
       
        Vector3 position = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
       
        
        values = words[1].Split(',');
        Vector3 rotation = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));

       
        values = words[2].Split(',');

        values[0] = values[0].Substring(5);
        
        Color color = new Color(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));

        string name = words[3];

        return new Tuple<Vector3, Vector3, Color, string>(position, rotation, color, name);


    }

    
}


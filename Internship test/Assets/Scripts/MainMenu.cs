using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    
    [SerializeField] private Button loadButton;

    [SerializeField] private GameObject PopUp;

    [SerializeField] private ConfirmPopUp confirmPopUp;
    bool ExistingSavedFile;

   private Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = gameObject.GetComponent<Manager>();
        MenuUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuUpdate()
	{
        ExistingSavedFile = manager.CheckSavedFiles();
        if (ExistingSavedFile)
        {
            loadButton.interactable = true;
        }
    }


    public void ExitGame()
    {
        PopUp.SetActive(true);
        confirmPopUp.Setup("Exit the game?", () => { Application.Quit(); } );
    }

    public void NewScene()
    {
        if (ExistingSavedFile)
        {
            PopUp.SetActive(true);
            confirmPopUp.Setup("Delete Saved Scene?", () => NewSceneConfirm());
            
        }
        else
        {
            
            manager.EnterSceneView();
            manager.CleanTheScene();
        }

    }

    public void LoadScene()
    {
        manager.CleanTheScene();
        manager.LoadScene();
        manager.EnterSceneView();
    }

    void NewSceneConfirm()
    {
        manager.CleanTheScene();
        manager.DeleteSavedData();
        manager.EnterSceneView();
    }

   

}

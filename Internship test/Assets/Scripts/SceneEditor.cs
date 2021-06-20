using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneEditor : MonoBehaviour
{

	#region variables

	//Bools representing editing modes
	bool buildMode;
    bool removeMode;
    bool editMode;
    bool paintMode;
    bool isInEditMode = false;

    
    //Bools representing editing modes


    //GAMEOBJECTS
    
    private GameObject selectedGameObject;
    private GameObject editingGameObject;
    private Color selectedColor;
    [SerializeField] private GameObject scene;
    private Camera mainCamera;
    //GAMEOBJECTS


    //SCRIPTS
    [SerializeField]
    private PlaceGhost placeGhost;
    [SerializeField] private Grid grid;
    [SerializeField] private BuildingPanel buildingPanel;
    //SCRIPTS

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0) && grid.IsCursorInScene(GetMousePosition()) && !IsPointerOverUIObject() && buildMode)
		{
            
                //PlaceCubeNear(GetMousePosition());
            PlaceCubeNear(placeGhost.ReturnPosition());
            if (isInEditMode && placeGhost.CanBePlaced())
			{
                isInEditMode = false;

                buildingPanel.EditMode();
                DeleteObject(editingGameObject);
            }
           

		}
        else if(Input.GetMouseButtonDown(0) && removeMode)
		{
            
            RaycastHit hitInfo = new RaycastHit();
            
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo) )
			{
                if (hitInfo.transform.tag == "Object") {
                   
                    DeleteObject(hitInfo.transform.gameObject);
                }
			}

        }

        else if (Input.GetMouseButtonDown(0) && editMode)
		{

            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.transform.tag == "Object")
                {
                    editingGameObject = Instantiate( hitInfo.transform.gameObject);
                    editingGameObject.transform.GetChild(0).gameObject.SetActive(false);
                    DeleteObject(hitInfo.transform.gameObject);
                    isInEditMode = true;
                    buildingPanel.BuildMode(editingGameObject);
                    
                    //GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>().EnterBuildMode();
                    
                }
            }

        }

        else if (Input.GetMouseButtonDown(0) && paintMode)
        {

            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.transform.tag == "Object")
                {

                    PaintObject(hitInfo.transform.gameObject);


                }
            }

        }
    }

    /// <summary>
    /// Places cube near given position
    /// </summary>
    /// <param name="position"></param>
    private void PlaceCubeNear(Transform position)
	{
		
		if (placeGhost.CanBePlaced())
		{
           GameObject obj = Instantiate(selectedGameObject, position.position, position.rotation, scene.transform);
            obj.transform.GetChild(0).gameObject.SetActive(true);
            obj.name = selectedGameObject.name;
        }
        
        


    }
    

    private Vector3 GetMousePosition()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit);
        Vector3 mousePos = hit.point;
        return mousePos;
    }
    /// <summary>
    /// Checks if mouse position is other an UI element
    /// </summary>
    /// <returns></returns>
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    
    void DeleteObject(GameObject obj)
	{
        Destroy(obj);
	}

    
    void PaintObject(GameObject obj)
	{
        obj.GetComponent<Renderer>().material.color = selectedColor;
    }

    public void BuildMode()
    {
        AllBoolsToFalse();
        buildMode = true;
    }

    public void EditMode()
    {
        AllBoolsToFalse();
        editMode = true;
    }

    public void RemoveMode()
    {
        AllBoolsToFalse();
        removeMode = true;
    }

    public void PaintMode()
    {
        AllBoolsToFalse();
        paintMode = true;
    }

    public void AllBoolsToFalse()
    {
        buildMode = false;
        editMode = false;
        removeMode = false;
        paintMode = false;
    }

    /// <summary>
    /// Sets painting color
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color)
	{
        selectedColor = color;
	}

    /// <summary>
    /// Sets gameobject which will be built in build mode
    /// </summary>
    /// <param name="obj"></param>
    public void SetGameObject(GameObject obj)
	{
        selectedGameObject = obj;
	}

    public void LoadGameObjects(Vector3 position, Vector3 rotation, Color color)
	{
        GameObject obj = Instantiate(selectedGameObject, position, Quaternion.Euler(rotation));
        obj.GetComponent<Renderer>().material.color = color;
    }

    

    
}

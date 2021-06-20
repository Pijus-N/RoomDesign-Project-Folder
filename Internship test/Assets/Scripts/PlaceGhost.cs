using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceGhost : MonoBehaviour
{



	//SCRIPTS
	[SerializeField] private Grid grid;
	[SerializeField] private GhostObject ghostObject;
	//SCRIPTS
	[SerializeField] GameObject cube;


	//GAMEOBJECTS
	GameObject currentCube;
	[SerializeField] private Button snappingButton;
	//GAMEOBJECTS


	bool isInSnappingMode = false;
	[SerializeField] private LayerMask layerMask;



	// Start is called before the first frame update
	void Start()
	{
		//  CreateGhost();

	}



	// Update is called once per frame
	void Update()
	{

		if (currentCube != null)
		{


			if (grid.IsCursorInScene(GetMousePosition()))
			{


				if (isInSnappingMode)
				{

					currentCube.transform.position = grid.GetNearestGrid(GetMousePosition());
				}
				else
				{
					currentCube.transform.position = grid.GetNearestPosition(GetMousePosition());
				}
				if (Input.GetKeyDown("r"))
				{
					currentCube.transform.Rotate(new Vector3(0, 90, 0));
				}

			}

			
		}
	}

	

	/// <summary>
	/// Gets the position of the mouse in 3D space
	/// </summary>
	/// <returns></returns>
	private Vector3 GetMousePosition()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Physics.Raycast(ray, out hit, float.MaxValue, layerMask);
		Vector3 mousePos = hit.point;
		return mousePos;
	}

	public bool CanBePlaced()
	{
		return currentCube.GetComponent<GhostObject>().CanBePlaced();
	}


	/// <summary>
	/// Deletes the current game object
	/// </summary>
	public void DeleteGameObject()
	{
		Destroy(currentCube);
	}

	/// <summary>
	/// Creates the ghost object which represent current position and placement
	/// </summary>
	public void CreateGhost(GameObject obj)
	{
		if(currentCube != null)
		{
			DeleteGameObject();
		}
		currentCube = Instantiate(obj, GetMousePosition(), obj.transform.rotation);
		currentCube.GetComponent<Renderer>().material.color = new Color(0.4770889f, 0.9445258f, 1f);
		currentCube.AddComponent<GhostObject>();
		currentCube.tag = "Untagged";
		currentCube.transform.GetChild(0).gameObject.SetActive(true);
		//currentCube.transform.rotation = obj.transform.rotation;
	}

	/// <summary>
	/// returns the position of the ghost object
	/// </summary>
	/// <returns></returns>
	public Transform ReturnPosition()
	{
		return currentCube.transform;
	}

	/// <summary>
	/// Turns on or off the snapping mode(snaps objects to the grid)
	/// </summary>
	public void SnappingMode()
	{
		if (!isInSnappingMode)
		{
			isInSnappingMode = true;
			snappingButton.GetComponent<Image>().color = new Color(0.656f, 1f, 0.458221f);
		}
		else
		{
			isInSnappingMode = false;
			snappingButton.GetComponent<Image>().color = Color.white;
		}

	}
}

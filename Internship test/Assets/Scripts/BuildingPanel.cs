using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Text.RegularExpressions;

public class BuildingPanel : MonoBehaviour
{
	// Start is called before the first frame update
	#region variables
	//Scripts
	[SerializeField]
	private SceneEditor sceneEditor;
	//Scripts

	//GAMEOBJECTS
	[SerializeField] private GameObject Builder;
	[SerializeField] private GameObject ColorsPalette;
	[SerializeField] private GameObject ObjectsSelection;
	[SerializeField] private GameObject colorPalette;

	[SerializeField] private GameObject chair;
	[SerializeField] private GameObject table;
	[SerializeField] private GameObject wardrobe;
	//GAMEOBJECTS

	//BUTTONS
	[SerializeField] private Button buildButton;
	[SerializeField] private Button EditButton;
	[SerializeField] private Button RemoveButton;
	[SerializeField] private Button PaintButton;
	[SerializeField] private List<Button> colorButtons;
	[SerializeField] private Button colorPaletteButton;
	[SerializeField] private GameObject RButtonImage;

	[SerializeField]
	private Button snappingButton;

	[SerializeField] private List<Button> objectButtons;
	//BUTTONS

	//DELEGATES
	private Action Callback;
	//DELEGATES

	//UI
	[SerializeField] private Sprite defaultTexture;
	[SerializeField] private Sprite colorPaletteTexture;
	//UI

	private Color color; // selected mode color

	#endregion
	void Start()
	{
		ColorUtility.TryParseHtmlString("7FF383", out color);
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// Turns on building mode
	/// </summary>
	public void BuildMode(GameObject specified = null) 
	{
		RButtonImage.SetActive(true);
		NeutralColor();
		snappingButton.gameObject.SetActive(true);
		ObjectsSelection.SetActive(true);
		buildButton.GetComponent<Image>().color = new Color(0.656f, 1, 0.458221f);
		sceneEditor.BuildMode();

		if (specified != null)
		{
			SetSpecified(specified);
			SetObjectButtonsNeutralColor();
		}

		else
		{
			

			SelectChair();
			
		}


	}
	/// <summary>
	/// Turns on editing mode
	/// </summary>
	public void EditMode()
	{
		RButtonImage.SetActive(false);
		NeutralColor();
		EditButton.GetComponent<Image>().color = new Color(0.656f, 1, 0.458221f);
		sceneEditor.EditMode();
		Builder.GetComponent<PlaceGhost>().DeleteGameObject();
	}
	/// <summary>
	/// Turns on remove mode
	/// </summary>
	public void RemoveMode()
	{
		RButtonImage.SetActive(false);
		NeutralColor();
		RemoveButton.GetComponent<Image>().color = new Color(0.656f, 1, 0.458221f);
		sceneEditor.RemoveMode();
		Builder.GetComponent<PlaceGhost>().DeleteGameObject();
	}
	/// <summary>
	/// Turns on painting mode
	/// </summary>
	public void PaintMode()
	{
		RButtonImage.SetActive(false);
		SetDefaultColor();
		NeutralColor();
		ColorsPalette.SetActive(true);
		PaintButton.GetComponent<Image>().color = new Color(0.656f, 1, 0.458221f);
		sceneEditor.PaintMode();
		Builder.GetComponent<PlaceGhost>().DeleteGameObject();

	}
	/// <summary>
	/// Sets all buttons colors to neutral
	/// </summary>
	public void NeutralColor()
	{
		if (ColorsPalette.activeInHierarchy)
		{
			ColorsPalette.SetActive(false);
		}
		
		buildButton.GetComponent<Image>().color = Color.white;
		EditButton.GetComponent<Image>().color = Color.white;
		RemoveButton.GetComponent<Image>().color = Color.white;
		PaintButton.GetComponent<Image>().color = Color.white;

		ObjectsSelection.SetActive(false);
		snappingButton.gameObject.SetActive(false);
	}

	/// <summary>
	/// Sets the painting color to the specified color in the UI
	/// </summary>
	public void SetColor()
	{
		SetDefaultButtonSize();
		Button selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

		selectedButton.gameObject.transform.localScale = new Vector3(0.6f, 2.6f, 1);
		sceneEditor.SetColor(EventSystem.current.currentSelectedGameObject.GetComponent<Button>().GetComponent<Image>().color);
		//Debug.Log(EventSystem.current.currentSelectedGameObject.GetComponent<Button>().GetComponent<Image>().color);
	}

	public void OpenColorPalette()
	{
		colorPalette.SetActive(true);
		colorPaletteButton.GetComponent<Image>().sprite = defaultTexture;
	}

	public void SetColor(Color color)
	{
		colorPalette.SetActive(false);
		colorPaletteButton.GetComponent<Image>().color = color;
		sceneEditor.SetColor(color);
	}

	/// <summary>
	/// Sets default painting color
	/// </summary>
	public void SetDefaultColor()
	{
		SetDefaultButtonSize();
		colorButtons[1].gameObject.transform.localScale = new Vector3(0.6f, 2.6f, 1);
		sceneEditor.SetColor(colorButtons[1].GetComponent<Image>().color);
	}

	/// <summary>
	/// Sets default button size in the UI
	/// </summary>
	private void SetDefaultButtonSize()
	{
		colorPalette.SetActive(false);
		colorPaletteButton.GetComponent<Image>().sprite = colorPaletteTexture;
		colorPaletteButton.GetComponent<Image>().color = new Color(1,1,1,1);
		foreach (Button button in colorButtons)
		{
			button.transform.localScale = new Vector3(0.5f, 2.5f, 1);
		}
	}

	/// <summary>
	/// Selects chair as primary build object
	/// </summary>
	public void SelectChair()
	{
		SetObjectButtonsNeutralColor();
		objectButtons[0].GetComponent<Image>().color = new Color(0.656f, 1, 0.458221f);

		sceneEditor.SetGameObject(chair);
		Builder.GetComponent<PlaceGhost>().CreateGhost(chair);
	}
	/// <summary>
	/// Selects table as primary build object
	/// </summary>
	public void SelectTable()
	{
		SetObjectButtonsNeutralColor();
		objectButtons[1].GetComponent<Image>().color = new Color(0.656f, 1, 0.458221f);

		sceneEditor.SetGameObject(table);
		Builder.GetComponent<PlaceGhost>().CreateGhost(table);
	}
	/// <summary>
	/// Selects wardrobe as primary build object
	/// </summary>
	public void SelectWardrobe()
	{
		SetObjectButtonsNeutralColor();
		objectButtons[2].GetComponent<Image>().color = new Color(0.656f, 1, 0.458221f);

		sceneEditor.SetGameObject(wardrobe);
		Builder.GetComponent<PlaceGhost>().CreateGhost(wardrobe);
	}

	/// <summary>
	/// Sets specified object as primary build object
	/// </summary>
	/// <param name="obj">specified gameobject</param>
	private void SetSpecified(GameObject obj)
	{
		

		sceneEditor.SetGameObject(obj);
		Builder.GetComponent<PlaceGhost>().CreateGhost(obj);
	}

	public void SetByName(string name)
	{
		
		if(Regex.IsMatch(name, "Chair"))
		{
			sceneEditor.SetGameObject(chair);
		}
		else if (Regex.IsMatch(name, "Table"))
		{
			sceneEditor.SetGameObject(table);
		}
		else if (Regex.IsMatch(name, "Wardrobe"))
		{
			sceneEditor.SetGameObject(wardrobe);
		}
	}

	/// <summary>
	/// Sets all UI buttons to neutral color
	/// </summary>
	private void SetObjectButtonsNeutralColor()
	{
		foreach(Button button in objectButtons)
		{
			button.GetComponent<Image>().color = Color.white;
		}
	}

}

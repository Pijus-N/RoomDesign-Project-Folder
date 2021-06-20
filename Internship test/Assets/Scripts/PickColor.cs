using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;


[Serializable]
public class ColorEvent : UnityEvent<Color> { }
public class PickColor : MonoBehaviour
{
    [SerializeField] private ColorEvent OnColorPreview;
        [SerializeField] private ColorEvent OnColorSelect;

    RectTransform Rect;
   

    Texture2D ColorTexture;

    [SerializeField] private BuildingPanel buildingPanel;

    // Start is called before the first frame update
    void Start()
    {
        Rect = GetComponent<RectTransform>();
        ColorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, null, out delta);

        float width = Rect.rect.width;
        float height = Rect.rect.height;

        delta += new Vector2(width * 0.5f, height * 0.5f);

        float x =  Mathf.Clamp( delta.x / width,0f,1f);
        float y = Mathf.Clamp(delta.y / height, 0f, 1f);



        int textureX =Mathf.RoundToInt( x * ColorTexture.width);
        int TextureY = Mathf.RoundToInt(y * ColorTexture.height);

        Color color = ColorTexture.GetPixel(textureX, TextureY);

        OnColorPreview?.Invoke(color);

		if (Input.GetMouseButtonDown(0))
		{
            OnColorSelect?.Invoke(color);
            buildingPanel.SetColor(color);
        }
         
    }
}

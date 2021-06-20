using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Start is called before the first frame update
    
    public ObjectType Type { get; private set; }

    public Color Color { get; private set; }


    public Object(ObjectType type, Color color)
	{
        
        Type = type;
        Color = color;
	}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(Color color)
	{
        Color = color;
        
	}

    
}

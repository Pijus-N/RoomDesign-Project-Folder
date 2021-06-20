using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private float size = 1f;
  //  [SerializeField] private float GridSize = 10f;


    public Vector3 GetNearestGrid(Vector3 position)
	{

       



        int xPos = Mathf.RoundToInt(position.x / size);
        int yPos = Mathf.RoundToInt(position.y / size);
        int zPos = Mathf.RoundToInt(position.z / size);
        

        return  new Vector3((float)xPos * size,0f  ,(float)zPos * size);
        
    }

    public Vector3 GetNearestPosition(Vector3 position)
    {

        float xPos = position.x / size;
        float yPos = position.y / size;
        float zPos = position.z / size;


        return new Vector3((float)xPos * size, 0f, (float)zPos * size);

    }



    public bool IsCursorInScene(Vector3 position)
	{
        int xPos = Mathf.RoundToInt(position.x / size);
        int yPos = Mathf.RoundToInt(position.y / size);
        int zPos = Mathf.RoundToInt(position.z / size);

		if ((float)xPos * size >= 1 && (float)xPos * size <= 10)
		{
            if  ((float)zPos * size <= 9 && (float)zPos * size >= 0)
            {
                return true;
            }
        }
        
        return false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	
}

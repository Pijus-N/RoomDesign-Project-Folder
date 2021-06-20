using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isOverlapping=false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
        
        if (collision.transform.tag == "Object" || collision.transform.tag == "Wall")
		{
            isOverlapping = true;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
		else
		{
            //isOverlapping = false;
        }
	}
    private void OnCollisionExit(Collision col)
    {
        
        if (col.transform.tag == "Object" || col.transform.tag == "Wall")
		{
            isOverlapping = false;
            gameObject.GetComponent<Renderer>().material.color = new Color(0.4770889f, 0.9445258f, 1f);
        }
        
        
    }

	private void OnCollisionStay(Collision collision)
	{
        if (collision.transform.tag == "Object" || collision.transform.tag == "Wall")
        {
            isOverlapping = true;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }

	public bool CanBePlaced()
	{
        return !isOverlapping;
	}
}

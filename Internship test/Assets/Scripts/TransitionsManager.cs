using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Animator cameraAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Starts the transition and enters the build mode
    /// </summary>
	public void EnterBuildMode()
	{
        cameraAnimator.ResetTrigger("ToPreviewMode");
       
        
        if (cameraAnimator.isActiveAndEnabled == true)
		{
            
            cameraAnimator.SetTrigger("ToBuildMode");
        }
		else
		{
            cameraAnimator.enabled = true;
        }
        
       
    }
    /// <summary>
    /// Enters preview mode
    /// </summary>
    public void EnterPreviewMode()
	{
        cameraAnimator.SetTrigger("ToPreviewMode");
	}

    

   
}

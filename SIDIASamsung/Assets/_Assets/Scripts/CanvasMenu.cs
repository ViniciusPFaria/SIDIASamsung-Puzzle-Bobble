using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CanvasMenu : MonoBehaviour {

	
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}

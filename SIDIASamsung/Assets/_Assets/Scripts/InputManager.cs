using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField] FakeCannon fakeCannon;

    private float startTime = 4;// wait level build
	
	// Update is called once per frame
	void Update () {

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) && Time.timeSinceLevelLoad > startTime)
            fakeCannon.Shoot();
#endif

#if UNITY_ANDROID
        if (Input.touchCount == 1 && Time.timeSinceLevelLoad > startTime)
            fakeCannon.Shoot();
        if (Input.touchCount > 1)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
#endif
    }
}

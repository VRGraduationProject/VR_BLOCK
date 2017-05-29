using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartControl : MonoBehaviour {
    public Camera oculusCamera;
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        if (Input.GetMouseButtonDown(0) || Input.GetButton("Xiaomi button B")) {

            if (Physics.Raycast(new Ray(oculusCamera.transform.position,
                 oculusCamera.transform.rotation * Vector3.forward), out hit))
                BtnStartScene();          
        }
    }
    public void BtnStartScene()
    {
        SceneManager.LoadScene(2);
    }
}

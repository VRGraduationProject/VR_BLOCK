using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverRestart : MonoBehaviour {

    public Camera oculusCamera;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetButton("Xiaomi button B"))
        {
                BtnreLoadScene();
            
        }

    }
    public void BtnreLoadScene()
    {
        SceneManager.LoadScene(2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenButton : MonoBehaviour
{
    public GameObject CloseAppPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseButtonClick()
    {
        CloseAppPanel.SetActive(true);
    }
    public void ClosePanelButtonClick()
    {
        CloseAppPanel.SetActive(false);
    }
    public void QuitGameButtonClick()
    {
        Application.Quit();
    }
}

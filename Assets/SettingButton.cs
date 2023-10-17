using MoreMountains.InfiniteRunnerEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HardBtnClick()
    {
        SettingGUIManager.Instance.HardBtnClick();
    }
    public void EasyBtnClick()
    {
        SettingGUIManager.Instance.EasyBtnClick();
    }
    public void BasicBtnClick()
    {
        SettingGUIManager.Instance.BasicBtnClick();
    }
    public void SaveBtnClick()
    {
        SettingGUIManager.Instance.SaveBtnClick();
    }
    public void CancelBtnClick()
    {
        SettingGUIManager.Instance.CancelBtnClick();
    }
    public void MusicBtnClick()
    {
        SettingGUIManager.Instance.MusicBtnClick();
    }
    public void SoundBtnClick()
    {
        SettingGUIManager.Instance.SoundBtnClick();
    }
}

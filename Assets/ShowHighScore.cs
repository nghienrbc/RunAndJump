using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHighScore : MonoBehaviour
{
    public TMPro.TMP_Text GamePointText1;
    public TMPro.TMP_Text GamePointText2;
    public TMPro.TMP_Text GamePointText3;
    public TMPro.TMP_Text GamePointText4;
    public TMPro.TMP_Text GamePointText5;

    public TMPro.TMP_Text GamePlayerText1;
    public TMPro.TMP_Text GamePlayerText2;
    public TMPro.TMP_Text GamePlayerText3;
    public TMPro.TMP_Text GamePlayerText4;
    public TMPro.TMP_Text GamePlayerText5;
    // Start is called before the first frame update
    void Start()
    {
        // get all value  here
        GamePointText4.text = PlayerPrefs.GetFloat("Run Shilin").ToString("000 000");
        GamePointText1.text = PlayerPrefs.GetFloat("Run Buba").ToString("000 000");
        GamePointText5.text = PlayerPrefs.GetFloat("Run-Fly Machito").ToString("000 000");
        GamePointText3.text = PlayerPrefs.GetFloat("Fly Olek").ToString("000 000");
        GamePointText2.text = PlayerPrefs.GetFloat("Jumping Hybrid").ToString("000 000"); 

        GamePlayerText4.text = !string.IsNullOrEmpty(PlayerPrefs.GetString("Run ShilinName")) && PlayerPrefs.GetFloat("Run Shilin") > 0 ? PlayerPrefs.GetString("Run ShilinName") : "Player Name";
        GamePlayerText1.text = !string.IsNullOrEmpty(PlayerPrefs.GetString("Run BubaName")) && PlayerPrefs.GetFloat("Run Buba") > 0 ? PlayerPrefs.GetString("Run BubaName") : "Player Name";
        GamePlayerText5.text = !string.IsNullOrEmpty(PlayerPrefs.GetString("Run-Fly MachitoName")) && PlayerPrefs.GetFloat("Run-Fly Machito") > 0 ? PlayerPrefs.GetString("Run-Fly MachitoName") : "Player Name";
        GamePlayerText3.text = !string.IsNullOrEmpty(PlayerPrefs.GetString("Fly OlekName")) && PlayerPrefs.GetFloat("Fly Olek") > 0 ? PlayerPrefs.GetString("Fly OlekName") : "Player Name";
        GamePlayerText2.text = !string.IsNullOrEmpty(PlayerPrefs.GetString("Jumping HybridName")) && PlayerPrefs.GetFloat("Jumping Hybrid") > 0 ? PlayerPrefs.GetString("Jumping HybridName") : "Player Name"; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

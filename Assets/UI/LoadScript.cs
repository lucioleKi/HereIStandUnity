using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScript : MonoBehaviour
{
    public Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        
        btn.onClick.AddListener(() => buttonCallBack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void buttonCallBack()
    {
        SaveData saveData = SaveSystem.LoadState();
        saveData.loadData(saveData);
    }
}

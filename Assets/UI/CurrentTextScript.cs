using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI mtext = gameObject.GetComponent<TextMeshProUGUI>();
        mtext.text = "";
        changeColor();
    }

    void OnEnable()
    {
        
        GM2.onPlayerChange += changeColor;
    }

    void OnDisable()
    {
        
        GM2.onPlayerChange -= changeColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void post(string input)
    {
        TextMeshProUGUI mtext = gameObject.GetComponent<TextMeshProUGUI>();
        mtext.text = input;
    }

    public void reset()
    {
        TextMeshProUGUI mtext = gameObject.GetComponent<TextMeshProUGUI>();
        mtext.text = "";
    }

    void changeColor()
    {
        TextMeshProUGUI mtext = gameObject.GetComponent<TextMeshProUGUI>();

        switch (GM1.player)
        {
            case 0:
                mtext.color = new Color(43f / 255f, 165f / 255f, 66f / 255f, 1f);
                break;
            case 1:
                mtext.color = new Color(1f, 223f / 255f, 63f / 255f, 1f);
                break;
            case 2:
                mtext.color = new Color(240f / 255f, 55f / 255f, 63f / 255f, 1f);
                break;
            case 3:
                mtext.color = new Color(10f / 255f, 142f / 255f, 216f / 255f, 1f);
                break;
            case 4:
                mtext.color = new Color(109f / 255f, 73f / 255f, 169f / 255f, 1f);
                break;
            case 5:
                mtext.color = new Color(162f / 255f, 88f / 255f, 61f / 255f, 1f);
                break;

        }
    }

    public void pauseColor()
    {
        GM2.onPlayerChange -= changeColor;
    }

    public void restartColor()
    {
        GM2.onPlayerChange += changeColor;
    }
}

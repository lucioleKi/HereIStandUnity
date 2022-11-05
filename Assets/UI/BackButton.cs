using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public Button btn;
    public int playerIndex;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(() => toCanvasBoard());
    }

    void toCanvasBoard()
    {
        //SceneManager.LoadScene("SceneMap");
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().alpha = 1;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().blocksRaycasts = true;
        GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().interactable = true;
        if (transform.parent.name == "CanvasCards")
        {
            GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().interactable = false;
            foreach (Transform child in GameObject.Find("CardContainer").transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        else if (transform.parent.name == "CanvasDiplomacy")
        {
            GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().alpha = 0;
            GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().blocksRaycasts = false;
            GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().interactable = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

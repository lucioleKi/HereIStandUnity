using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class PDScript : MonoBehaviour
{
    public Image thisImage;
    int player;
    // Start is called before the first frame update
    void Start()
    {

       player = GM1.player;
       thisImage = gameObject.GetComponent<Image>();
       thisImage.sprite = Resources.Load<Sprite>("Sprites/power"+player.ToString());
        
        
    }

    void OnEnable()
    {
        
        GM2.onPlayerChange += changeDisplayPlayer;
    }

    void OnDisable()
    {
       
        GM2.onPlayerChange -= changeDisplayPlayer;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && GameObject.Find("KeyLeft").GetComponent<Button>().interactable)
        {
            GM1.player = (GM1.player - 1) % 6;

            if (GM1.player == -1)
            {
                GM1.player = 5;
            }
            GM2.onPlayerChange();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && GameObject.Find("KeyRight").GetComponent<Button>().interactable)
        {
            GM1.player = (GM1.player + 1) % 6;
            GM2.onPlayerChange();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().alpha == 0) {
                GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().alpha = 1;
                GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().blocksRaycasts = true;
                GameObject.Find("CanvasBoard").GetComponent<CanvasGroup>().interactable = true;
                if (GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().alpha == 1)
                {
                    GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().alpha = 0;
                    GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().blocksRaycasts = false;
                    GameObject.Find("CanvasCards").GetComponent<CanvasGroup>().interactable = false;
                    foreach (Transform child in GameObject.Find("CardContainer").transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }

                }
                else if (GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().alpha == 1)
                {
                    GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().alpha = 0;
                    GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().blocksRaycasts = false;
                    GameObject.Find("CanvasDiplomacy").GetComponent<CanvasGroup>().interactable = false;
                }
            }
        }
        
            
            
        
        

    }

    void changeDisplayPlayer()
    {
        thisImage = gameObject.GetComponent<Image>();
        thisImage.sprite = Resources.Load<Sprite>("Sprites/power" + GM1.player.ToString());
    }
}

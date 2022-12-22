using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Swipe : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int sceneId;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    void Start()
    {
        sceneId = SceneManager.GetActiveScene().buildIndex;
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;  
            if(endTouchPosition.x < startTouchPosition.x)
            {
                if (sceneId + 1 >= SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    SceneManager.LoadScene(sceneId + 1);
                }
            }

            if(endTouchPosition.x > startTouchPosition.x)
            {
                if (sceneId + 1 < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    SceneManager.LoadScene(sceneId -1);
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

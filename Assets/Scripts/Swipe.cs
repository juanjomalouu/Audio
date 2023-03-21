using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Swipe : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    private int sceneId;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private Vector3 panelLocation;

    public void OnEndDrag(PointerEventData eventData)
    {
        sceneId = SceneManager.GetActiveScene().buildIndex;
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        string dragDirection = GetDragDirection(dragVectorDirection).ToString();
        if (dragDirection == "Left")
        {
            if (sceneId + 1 == 8 || sceneId + 1 == 14)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(sceneId + 1);
            }
        }
        else if (dragDirection == "Right")
        {
            if (sceneId - 1 == 7)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene(sceneId - 1);
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {

    }

    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        Debug.Log(draggedDir);
        return draggedDir;
    }


    // Update is called once per frame
    void Update()
    {
        //sceneId = SceneManager.GetActiveScene().buildIndex;
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    startTouchPosition = Input.GetTouch(0).position;
        //}

        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        //{
        //    endTouchPosition = Input.GetTouch(0).position;
        //    if (endTouchPosition.x < startTouchPosition.x)
        //    {
        //        if (sceneId + 1 >= SceneManager.sceneCountInBuildSettings)
        //        {
        //            SceneManager.LoadScene(0);
        //        }
        //        else
        //        {
        //            SceneManager.LoadScene(sceneId + 1);
        //        }
        //    }

        //    if (endTouchPosition.x > startTouchPosition.x)
        //    {
        //        if (sceneId + 1 < SceneManager.sceneCountInBuildSettings)
        //        {
        //            SceneManager.LoadScene(0);
        //        }
        //        else
        //        {
        //            SceneManager.LoadScene(sceneId - 1);
        //        }
        //    }

        //}
    }
}

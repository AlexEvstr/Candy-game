using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject[] objects;
    private int currentIndex = 0;
    private Vector2 startTouchPosition, endTouchPosition;
    private float swipeThreshold = 50f;

    void Start()
    {
        SwitchObject(currentIndex);
    }

    void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Vector2 swipeDirection = endTouchPosition - startTouchPosition;

                if (Mathf.Abs(swipeDirection.x) > swipeThreshold && Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                {
                    if (swipeDirection.x > 0)
                    {
                        SwitchPreviousObject();
                    }
                    else
                    {
                        SwitchNextObject();
                    }
                }
            }
        }
    }

    private void SwitchNextObject()
    {
        objects[currentIndex].SetActive(false);

        currentIndex++;

        if (currentIndex >= objects.Length)
        {
            currentIndex = 0;
        }

        SwitchObject(currentIndex);
    }

    private void SwitchPreviousObject()
    {
        objects[currentIndex].SetActive(false);

        currentIndex--;

        if (currentIndex < 0)
        {
            currentIndex = objects.Length - 1;
        }

        SwitchObject(currentIndex);
    }

    private void SwitchObject(int index)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == index);
        }
    }
}
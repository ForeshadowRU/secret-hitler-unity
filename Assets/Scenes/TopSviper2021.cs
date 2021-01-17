using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopSviper2021 : MonoBehaviour
{
    Vector2 prevPosition;
    float width;
    int screen = 0;
    const float procentWidthToSwipe = 0.2f;
    float swipeWidth;
    bool get = false;
    void OnMouseDown()
    {
        get = true;
        prevPosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        get = false;
        Vector2 position = Input.mousePosition;
        Vector2 delta = (position - prevPosition);
        if (delta.x > swipeWidth)
        {
            if (screen < 1)
                screen += 1;
        }

        if (delta.x < -swipeWidth)
        {
            if (screen > -1)
                screen -= 1;
        }
    }
    void Start()
    {
        width = Screen.width;
        swipeWidth = procentWidthToSwipe * width;
    }

    // Update is called once per frame
    void Update()
    {
        float xPos = transform.position.x;
        Vector2 posSc = new Vector2(((0.5f - screen) * width), 0);
        Vector2 widthWorld = new Vector2(width, 0);
        posSc = Camera.main.ScreenToWorldPoint(posSc);
        xPos += posSc.x;
        if (Mathf.Abs(xPos) > 0.01)
        {
            if (!get)
            {

                float sign = xPos > 0 ? 1 : -1;
                transform.position -= new Vector3(Mathf.Log(Mathf.Abs(xPos) + 1), 0, 0) * sign * Time.deltaTime * 10;
            }
        }
        if(get)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(prevPosition);
            pos.y = transform.position.y;
            pos.x -= posSc.x;
            if(screen > 0)
            {
                if (pos.x + posSc.x > 0)
                    pos = transform.position;
            }
            if (screen < 0)
            {
                if (pos.x + posSc.x < 0)
                    pos = transform.position;
            }
            transform.position = pos; ;
        }
    }
}

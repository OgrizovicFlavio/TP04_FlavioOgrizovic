using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float resetPositionX = -19f;
    [SerializeField] private float backgroundOffset = 19f;

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++) 
        {
            Move(backgrounds[i]);
            if (backgrounds[i].position.x < resetPositionX)
            {
                MoveBackgroundToEnd(i);
            }
        }
    }

    public void Move(Transform background)
    {
        background.localPosition += Vector3.left * (speed * Time.deltaTime);

    }

    private void MoveBackgroundToEnd(int index)
    {
        int lastBackgroundIndex;

        if (index == 0)
        {
            lastBackgroundIndex = backgrounds.Length - 1;
        }
        else
        {
            lastBackgroundIndex = index - 1;
        }

        backgrounds[index].position = backgrounds[lastBackgroundIndex].position + Vector3.right * backgroundOffset;
    }
}

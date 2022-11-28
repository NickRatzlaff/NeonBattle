using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void HoverEffect()
    {
        var tempColor = image.color;
        tempColor.a = 0.5f;
        image.color = tempColor;
    }

    public void UnHoverEffect()
    {
        var tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
    }
}

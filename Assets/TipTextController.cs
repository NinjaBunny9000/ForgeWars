using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipTextController : MonoBehaviour
{

    Text tipText;
    string tip;

    // bubbleText
    [SerializeField] [Range(-0.001f,-0.01f)] float bubbleRate = -0.005f;
    
    [Range(0.01f, 1f)]
    [SerializeField] float bubbleFloor = 0.8f;
    [Range(0.01f, 2f)]
    [SerializeField] float bubbleCeiling = 1f;

    // Start is called before the first frame update
    void Start()
    {
        tipText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        generateTip();
        showTip();
        // bubbleText();
    }

    private void generateTip()
    {
        throw new NotImplementedException();
    }

    private void showTip()
    {
        throw new NotImplementedException();
    }

    void bubbleText()
    {
        Vector3 scaleChange = new Vector3(bubbleRate, bubbleRate, bubbleRate);
        transform.localScale += scaleChange;

        if (transform.localScale.y < bubbleFloor || transform.localScale.y > bubbleCeiling) {
            scaleChange = -scaleChange;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueToText : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void changeText(float value)
    {
        if ((value - (int) value) != 0)
            text.SetText(value.ToString("F2"));
        else
            text.SetText(value.ToString());
    }
}

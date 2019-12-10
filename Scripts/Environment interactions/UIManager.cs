using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] panels;
    private int currentPanel = 0;
    // Start is called before the first frame update
    void Start()
    {
        panels[currentPanel].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeToPanel(int index)
    {
        if (panels[currentPanel].activeSelf)
            panels[currentPanel].SetActive(false);
        panels[index].SetActive(true);
        currentPanel = index;
    }
}

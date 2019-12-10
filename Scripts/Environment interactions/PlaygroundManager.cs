using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private GameObject[] attractions;

    [SerializeField]
    private Values[] data;

    private GameObject[] instanciated;
    private int fractalIndex = -1;
    private int sphereIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        instanciated = new GameObject[attractions.Length];

        for (int i = 0; i < attractions.Length; i++)
        {
            string name = attractions[i].gameObject.name;
            if (name.Contains("Fractal"))
                fractalIndex = i;
            else if (name.Contains("PhysicSphere"))
                sphereIndex = i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && (attractions.Length >= 1))
            instanciateAttraction(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2) && (attractions.Length >= 2))
            instanciateAttraction(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3) && (attractions.Length >= 3))
            instanciateAttraction(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4) && (attractions.Length >= 4))
            instanciateAttraction(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5) && (attractions.Length >= 5))
            instanciateAttraction(4);
    }

    private void instanciateAttraction(int index)
    {
        if (fractalIndex >= 0 && instanciated[fractalIndex] != null)
            DestroyImmediate(instanciated[fractalIndex].gameObject);      

        if (sphereIndex >= 0 && instanciated[sphereIndex] != null)
            DestroyImmediate(instanciated[sphereIndex].gameObject);
        
        if (index == fractalIndex || index == sphereIndex)
            clearAttractions();

        if (instanciated[index] != null)
            Destroy(instanciated[index]);

        GameObject obj = Instantiate(attractions[index]);
        obj.transform.parent = spawnPoints[index];
        obj.transform.position = spawnPoints[index].position;
        data[index].setValues(obj);
        instanciated[index] = obj;
        
    }

    private void clearAttractions()
    {
        for (int i = 0; i < instanciated.Length; i++)
        {
            if (instanciated[i] != null)
                Destroy(instanciated[i]);
        }
    }
}

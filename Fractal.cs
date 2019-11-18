using UnityEngine;
using System.Collections;


public class Fractal : MonoBehaviour {

    [SerializeField]
    private Mesh mesh;
    [SerializeField]
    private Material material;
    [SerializeField]
    private int maxDepth;
    [SerializeField]
    private int depth;
    [SerializeField]
    private float childScale = 0.5f;
    [SerializeField]
    private float speed = 0.5f;

    private Vector3[] directions = {Vector3.up,Vector3.right, Vector3.left, Vector3.forward, Vector3.back};

    private Quaternion[] orientations = {Quaternion.identity
                                            , Quaternion.Euler(0f,0f,-90f)
                                            , Quaternion.Euler(0f,0f,90f)
                                            , Quaternion.Euler(90f, 0f, 0f)
                                            , Quaternion.Euler(-90f, 0f, 0f)};
    private Material[] materials;

    private void Start ()
    {
        if (materials == null)
            InitializeMaterials();

        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = materials[depth];
        InitializeMaterials();
        if (depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }

    }

    private void Update () {
            transform.Rotate(0f, 30f * Time.deltaTime, 0f);
    }

    private void Initialize (Fractal parent, Vector3 direction, Quaternion orientation)
    {
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localRotation = orientation;
        transform.position = parent.transform.position;
        StartCoroutine(EnterPosition(direction * (0.5f + 0.5f * childScale)));
    }


     private void InitializeMaterials ()
    {
         materials = new Material[maxDepth + 1];
         for (int i = 0; i <= maxDepth; i++)
         {
              materials[i] = new Material(material);
              materials[i].color = Color.Lerp(Color.grey, Color.green, (float)i / maxDepth);
         }
        materials[maxDepth].color = Color.cyan;
     }

    private IEnumerator EnterPosition (Vector3 end)
    {
        float sqrRemainingDistance = (transform.localPosition - end).sqrMagnitude;
        float inverseMoveTime = 1 / speed;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.localPosition, end, inverseMoveTime * Time.deltaTime);
            transform.localPosition = newPosition;
            sqrRemainingDistance = (transform.localPosition - end).sqrMagnitude;

            yield return null;
        }
    }

    private IEnumerator CreateChildren ()
    {
        for(int i = 0; i < directions.Length; i++)
        {
            float wait = Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(wait);
            new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, directions[i], orientations[i]);
        }
    }
}

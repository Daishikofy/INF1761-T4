using System.Collections;

using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RepeledMesh : MonoBehaviour
{
    [SerializeField]
    private float radius, time;
    [SerializeField]
    private Mesh mesh;

    private int numberOfParticles;
    private Rigidbody[] particles;
    private float pastTime;
    private bool positionFase;
    Vector3[] vertices;

    public Color color;


    void Start()
    {
        var cube = GetComponent<Cube>();
        cube.meshReady.AddListener(StartRepealing);
        this.transform.position = new Vector3(cube.xSize/2, cube.ySize / 2, cube.zSize / 2);
    }

    void StartRepealing()
    {
        GetComponent<Cube>().drawGizmos = false;
        Debug.Log("StartRepealing");
        generateParticles();
        StartCoroutine(applyForces());
    }

    private void generateParticles()
    {
       mesh = GetComponent<MeshFilter>().mesh;
        numberOfParticles = mesh.vertices.Length;
        particles = new Rigidbody[numberOfParticles];
        for (int i = 0; i < numberOfParticles; i++)
        {
            var particle = new GameObject("Particle").AddComponent<Rigidbody>();
            particle.transform.position = particle.position = mesh.vertices[i];
            particle.useGravity = false;
            particle.transform.parent = this.transform;
            particles[i] = particle;
            mesh.vertices[i] = particle.position;
        }
    }

    private IEnumerator applyForces()
    {
        while (pastTime < time)
        {
            pastTime += Time.deltaTime;
            for (int i = 0; i < numberOfParticles; i++)
            {
                float dist = Vector3.Distance(particles[i].position, this.transform.position);
                if (Vector3.Distance(particles[i].position, this.transform.position) > radius)
                {
                    float distance = (Vector3.Distance(particles[i].position, this.transform.position) - radius);
                    Vector3 direction = (this.transform.position - particles[i].position).normalized;
                    Vector3 target = particles[i].position + direction * distance;
                    particles[i].MovePosition(target);
                }
                else
                    for (int j = 0; j < numberOfParticles; j++)
                    {
                        float distance = Vector3.Distance(particles[i].position, particles[j].position);
                        if (distance > 0)
                            particles[i].AddForce((particles[i].position - particles[j].position).normalized * (1 / distance) * 0.5f);
                    }
                generateVertices();
            }
            yield return null;
        }
        for (int i = 0; i < numberOfParticles; i++)
        {
            particles[i].velocity = Vector3.zero;
        }   
    }

    private void generateVertices()
    {
        vertices = new Vector3[numberOfParticles];
        for (int i = 0; i < numberOfParticles; i++)
        {
            //particles[i].velocity = Vector3.zero;
            vertices[i] = particles[i].position;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    public void OnDrawGizmos()
    {
        if (particles == null) return;
        for (int i = 0; i < particles.Length; i++)
        {
            Color color = new Color(particles[i].position.x / radius, particles[i].position.y / radius, particles[i].position.z / radius);
            Gizmos.color = color;
            Gizmos.DrawSphere(particles[i].position, 0.05f);
        }
    }

    public void setRadiusAndParticles(int radius, int particles)
    {
        numberOfParticles = particles;
        this.radius = radius;
    }
}

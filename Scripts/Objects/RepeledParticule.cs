using System.Collections;

using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RepeledParticule : MonoBehaviour
{
    [SerializeField]
    private int numberOfParticles;
    [SerializeField]
    private float radius, time;

    private Rigidbody[] particles;
    private float pastTime;
    private bool positionFase;
    Vector3[] vertices;
    private Mesh mesh;

    public Color color;
    

    void Start()
    {
        generateParticles();
        StartCoroutine(applyForces());
    }

    private void generateParticles()
    {
        particles = new Rigidbody[numberOfParticles];
        for (int i = 0; i < numberOfParticles; i++)
        {
            var particle = new GameObject("Particle").AddComponent<Rigidbody>();
            Vector3 random = Random.insideUnitSphere*radius + this.transform.position;
            particle.transform.position = particle.position = random;
            particle.useGravity = false;
            particle.transform.parent = this.transform;
            particles[i] = particle;
            
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
                
            }
            yield return null;
        }
        for (int i = 0; i < numberOfParticles; i++)
        {
            particles[i].velocity = Vector3.zero;
        }

        generateVertices();
    }

    private void generateVertices()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural physic based sphere";
        vertices = new Vector3[numberOfParticles];
        for (int i = 0; i < numberOfParticles; i++)
        {
            particles[i].velocity = Vector3.zero;
            vertices[i] = particles[i].position;
            float dist = Vector3.Distance(particles[i].position, this.transform.position);
        }

        mesh.vertices = vertices;
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

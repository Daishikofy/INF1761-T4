using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueSphere : Values
{

    private int radius = 3, particles = 100;

    public override void setValues(GameObject obj)
    {
        RepeledParticule sphere = obj.GetComponent<RepeledParticule>();
        sphere.setRadiusAndParticles(radius, particles);
    }
    public void setParticles(float particles)
    {
        this.particles = (int)particles;
    }

    public void setRadius(float radius)
    {
        this.radius = (int)radius;
    }

}

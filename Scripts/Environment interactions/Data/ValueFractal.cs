using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueFractal : Values
{
    private int depth = 3;
    private float scale = 0.2f;
    public override void setValues(GameObject obj)
    {
        Fractal fractal = obj.GetComponent<Fractal>();
        fractal.setScaleAndDepth(scale, depth);
    }

    public void setScale(float scale)
    {
        this.scale = scale;
    }
    public void setDepth(float depth)
    {
        this.depth = (int)depth;
    }

}

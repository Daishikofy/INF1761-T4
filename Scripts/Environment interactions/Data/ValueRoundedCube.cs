using UnityEngine;

public class ValueRoundedCube : Values
{
    private int x = 3, y = 3, z = 3;
    private float roundness = 1.0f;
    public override void setValues(GameObject obj)
    {
        RoundedCube rCube = obj.GetComponent<RoundedCube>();
        rCube.setSizes(x, y, z);
        rCube.setRoundness(roundness);
    }

    public void setX(float x)
    {
        this.x = (int)x;
    }
    public void setY(float y)
    {
        this.y = (int)y;
    }

    public void setZ(float z)
    {
        this.z = (int)z;
    }

    public void setRoundness(float rd)
    {
        this.roundness = rd;
    }
}

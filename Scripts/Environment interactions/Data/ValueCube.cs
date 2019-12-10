using UnityEngine;

public class ValueCube : Values
{
    private int x = 3, y = 3, z = 3;

    public override void setValues(GameObject obj)
    {
        Cube cube = obj.GetComponent<Cube>();
        cube.setSizes(x, y, z);
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Vector3 begin;
    public Vector3 end;

    private LineRenderer line;
    // Start is called before the first frame update
    public void Render()
    {
        line = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        Vector3[] positions = {begin, end};
        Color[] colors = { Color.white, Color.gray };
        line.enabled = true;

        line.SetPositions(positions);
        line.startColor = Color.white;
        line.endColor = Color.grey;

        line.startWidth = 0.02f;
        line.endWidth = 0.02f;
    }


}

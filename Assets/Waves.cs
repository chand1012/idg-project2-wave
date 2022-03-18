using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public float amplitude = 1f;
    public float velocity = 1f;
    public float lambda = 1f;
    public float decay = 0.01f;
    public float impact = 1f;
    bool colided;
    // Use this for initialization
    void Start()
    {
        colided = false;
    }

    float cosWave(float r, float a, float v, float l, float i)
    {
        float t = Time.time - i;
        float exp = (-r) - (a * t * decay);
        float inCos = (2f * Mathf.PI * (r - (v * t))) / l;
        return a * Mathf.Exp(exp) * Mathf.Cos(inCos);
    }

    // Update is called once per frame
    void Update()
    {
        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        Vector3[] verts = mesh.vertices;
        Vector3 v;

        bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
   
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (shift)
            {
                amplitude += 0.1f;
            } else
            {
                amplitude -= 0.1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (shift)
            {
                lambda += 0.1f;
            } 
            else
            {
                lambda -= 0.1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (shift)
            {
                velocity += 0.1f;
            }
            else
            {
                velocity -= 0.1f;
            }
        }

        for (var i = 0; i < verts.Length; i++)
        {
            v = transform.TransformPoint(verts[i]);
            if (colided)
            {
                float r = Mathf.Sqrt(v.x * v.x + v.z * v.z);
                var w = cosWave(r, amplitude, velocity, lambda, impact);
                v.y = w;
                verts[i] = transform.InverseTransformPoint(v);
                if (w == 0f)
                {
                    colided = false;
                }
            }
            else
            {
                v.y = 0;
                verts[i] = transform.InverseTransformPoint(v);
            }
        }
        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    void OnCollisionEnter(Collision collision)
    {
        colided = true;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Amplitude: " + amplitude.ToString());
        GUI.Label(new Rect(10, 25, 100, 20), "Velocity: " + velocity.ToString());
        GUI.Label(new Rect(10, 40, 100, 20), "Wavelength: " + lambda.ToString());
        GUI.Label(new Rect(10, 55, 100, 20), "Decay: " + decay.ToString());

    }
}
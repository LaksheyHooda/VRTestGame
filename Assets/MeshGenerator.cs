using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 100;
    public int zSize = 100;
    public float scale = 2f;
    public bool whichGraph = false;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;    
        CreateShape();
        UpdateMesh();
        MeshCollider collider = GetComponent<MeshCollider>();
        collider.sharedMesh = mesh;
        //collider.convex = true;
        //collider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
    } 

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y;
                if (whichGraph)
                {
                    y = Mathf.Pow((x - (xSize / 2)) / scale, 2f) - Mathf.Pow((z - (zSize / 2)) / scale, 2f); //Hyperbolic Paraboloid
                }
                else
                {
                    y = Mathf.Pow((x - (xSize / 2)) / scale, 2f) + Mathf.Pow((z - (zSize / 2)) / scale, 2f); //Conic Paraboloid
                }
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 12];

        int vert = 0;
        int tris = 0;
        for(int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                triangles[tris + 11] = vert + 0;
                triangles[tris + 10] = vert + xSize + 1;
                triangles[tris + 9] = vert + 1;
                triangles[tris + 8] = vert + 1;
                triangles[tris + 7] = vert + xSize + 1;
                triangles[tris + 6] = vert + xSize + 2;

                vert++;
                tris += 12;
            }
            vert++;
        }
    }

    /*   
    working code

    vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for(int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

     */

    private void OnDrawGizmos()
    {
        //if (vertices == null) return; 

        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    Gizmos.color = new Color(255, 255, 255);
        //    Gizmos.DrawSphere(vertices[i], .1f);
        //}
    }

    //private void OnDrawGizmos()
    //{
    //    if (vertices == null) return;

    //    for (int i = 0; i < vertices.Length - 1; i++)
    //    {
    //        Gizmos.color = new Color(128, 128, 128);
    //        Gizmos.DrawLine(vertices[i], vertices[i + 1]);
    //    }
    //}

}

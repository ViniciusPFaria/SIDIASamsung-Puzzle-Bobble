using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexColor : MonoBehaviour {

    [SerializeField] Color color;

	// Use this for initialization
	void Start () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Color[] colorVertex = new Color[mesh.vertices.Length];
        
        for(int i = 0; i< colorVertex.Length; i++)
        {
            colorVertex[i] = color;
        }

        mesh.colors = colorVertex;
	}
	
}

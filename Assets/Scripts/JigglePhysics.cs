using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class JigglePhysics : MonoBehaviour
{
    private Mesh originalMesh, meshClone;
    private MeshRenderer meshRenderer;

    public float intensity, mass, stiffness, dampness;
    private Vector3[] vertexArray;

    private GameObjectVertices[] gameObjectVertices;
    // Start is called before the first frame update
    void Start()
    {
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        meshClone = Instantiate(originalMesh);

        GetComponent<MeshFilter>().sharedMesh = meshClone;
        meshRenderer = GetComponent<MeshRenderer>();

        gameObjectVertices = new GameObjectVertices[meshClone.vertices.Length];

        for (int i = 0; i < meshClone.vertices.Length; i++)
        {
            gameObjectVertices[i] = new GameObjectVertices(i, transform.TransformPoint(meshClone.vertices[i]));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vertexArray = originalMesh.vertices;
        for (int i = 0; i < gameObjectVertices.Length; i++)
        {
            Vector3 target = transform.TransformPoint(vertexArray[gameObjectVertices[i].id]);
            float _intensity = (1 - (meshRenderer.bounds.max.y - target.y) / meshRenderer.bounds.size.y) * intensity;
            gameObjectVertices[i].Shake(target, mass, stiffness, dampness);
            vertexArray[gameObjectVertices[i].id] = Vector3.Lerp(vertexArray[gameObjectVertices[i].id], target, _intensity);
        }
        meshClone.vertices = vertexArray;
    }

    public class GameObjectVertices
    {
        public int id;
        public Vector3 position;
        public Vector3 velocity, force;

        public float speed;

        public GameObjectVertices(int _id, Vector3 pos)
        {
            id = _id;
            position = pos;
        }

        public void Shake(Vector3 target, float m, float s, float d)
        {
            force = (target - position) * s;
            velocity = (velocity + force / m) * d;
            position += velocity;

            if ((velocity + force + force / m).magnitude < 0.0001f)
            {
                position = target;
            }

        }
    }
}

using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class DynamicMeshCollider : MonoBehaviour
{
    public float yOffset = 0.0f;

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        CreateMeshCollider();
    }

    private void Update()
    {
        UpdateMeshCollider();
    }

    private void CreateMeshCollider()
    {
        Mesh mesh = meshFilter.mesh;

        meshCollider.sharedMesh = mesh;
        UpdateMeshColliderPosition();
    }

    private void UpdateMeshCollider()
    {
        Mesh mesh = meshFilter.mesh;

        meshCollider.sharedMesh = mesh;
        UpdateMeshColliderPosition();
    }

    private void UpdateMeshColliderPosition()
    {
        Vector3 waterPosition = transform.position;
        waterPosition.y += yOffset;
        meshCollider.transform.position = waterPosition;
    }
}
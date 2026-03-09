using System.Collections.Generic;
using UnityEngine;

public class ImagePointSpawner : MonoBehaviour
{
    [SerializeField] private ImageDetectionManager imageDetectionManager;
    [SerializeField] private Texture2D sourceImage;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector2 worldSize = new Vector2(50f, 50f);
    [SerializeField] private float spawnZ = 0f;
    [SerializeField] private string spawnOnCharacter = "x";
    [SerializeField] private bool spawnOnStart = false;

    private readonly List<GameObject> _spawnedObjects = new List<GameObject>();

    void Start()
    {
        if (spawnOnStart)
            Spawn();
    }

    public void Spawn()
    {
        Clear();

        string grid = imageDetectionManager.GetStringFromImage(sourceImage);
        string[] rows = grid.Split('\n');

        int gridH = rows.Length;

        for (int y = 0; y < gridH; y++)
        {
            if (string.IsNullOrEmpty(rows[y])) continue;

            int gridW = rows[y].Length;

            for (int x = 0; x < gridW; x++)
            {
                if (spawnOnCharacter.Length == 0 || rows[y][x] != spawnOnCharacter[0]) continue;

                float wx = (x / (float)gridW - 0.5f) * worldSize.x;
                float wy = ((gridH - 1 - y) / (float)gridH - 0.5f) * worldSize.y;

                Vector3 worldPos = transform.TransformPoint(new Vector3(wx, wy, spawnZ));
                GameObject obj = Instantiate(prefab, worldPos, Quaternion.identity, transform);
                obj.SetActive(true);
                _spawnedObjects.Add(obj);
            }
        }
    }

    public void Merge()
    {
        MeshFilter[] filters = GetComponentsInChildren<MeshFilter>();
        if (filters.Length == 0) return;

        CombineInstance[] combine = new CombineInstance[filters.Length];
        for (int i = 0; i < filters.Length; i++)
        {
            combine[i].mesh = filters[i].sharedMesh;
            combine[i].transform = filters[i].transform.localToWorldMatrix;
        }

        Mesh merged = new Mesh();
        merged.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        merged.CombineMeshes(combine, true, true);

        Clear();

        GameObject mergedObj = new GameObject("MergedMesh");
        mergedObj.transform.SetParent(transform);
        mergedObj.transform.localPosition = Vector3.zero;
        mergedObj.transform.localRotation = Quaternion.identity;
        mergedObj.transform.localScale = Vector3.one;

        mergedObj.AddComponent<MeshFilter>().sharedMesh = merged;
        mergedObj.AddComponent<MeshRenderer>().sharedMaterial = filters[0].GetComponent<MeshRenderer>()?.sharedMaterial;
    }

    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (Application.isPlaying)
                Destroy(transform.GetChild(i).gameObject);
            else
                DestroyImmediate(transform.GetChild(i).gameObject);
        }

        _spawnedObjects.Clear();
    }
}

using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    [SerializeField] private float XScrollSpeed = 0.1f;
    [SerializeField] private float YScrollSpeed = 0.1f;
    private Vector2 offset;
    private Renderer waterRenderer;
    void Awake()
    {
        if (gameObject.TryGetComponent<Renderer>(out Renderer renderer))
        {
            waterRenderer = renderer;
        }
        else
        {
            Debug.Log("Can't find Renderer");
        }
    }

    void Update()
    {
        offset.x += Time.deltaTime * XScrollSpeed;
        offset.y += Time.deltaTime * YScrollSpeed;
        if (waterRenderer != null)
        {
            waterRenderer.material.mainTextureOffset = offset;
        }
    }
}

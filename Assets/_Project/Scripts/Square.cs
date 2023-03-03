
using UnityEngine;

public class Square : MonoBehaviour
{
   [SerializeField] Material _squareMaterial;
   [SerializeField] Renderer _squareRenderer;

    private void OnEnable()
    {
        _squareRenderer.material = _squareMaterial;
    }
}

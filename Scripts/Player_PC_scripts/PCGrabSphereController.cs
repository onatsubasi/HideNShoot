using UnityEngine;

public class PCGrabSphereController : MonoBehaviour
{
    [SerializeField] private Material wrongMaterial;
    [SerializeField] private Material correctMaterial;

    private MeshRenderer _meshRenderer;
    private Light _light;

    private void Awake()
    {
        _light = GetComponentInChildren<Light>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = wrongMaterial;
    }

    public void SetMaterial(bool isInside)
    {
        if(_light != null)
            _light.enabled = isInside;

        _meshRenderer.material = isInside ? correctMaterial : wrongMaterial;
    }

}

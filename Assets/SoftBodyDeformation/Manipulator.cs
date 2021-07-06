using UnityEngine;

[ExecuteAlways]
public class Manipulator : MonoBehaviour
{
    public Transform Anchor;
    public Transform Handle;

    public Renderer Renderer;
    public float Hardness;
    public float Radius;

    Material _softbodyMaterial;
    Matrix4x4 _transformationMatrix;

    static readonly int TransformationMatrixId = Shader.PropertyToID("_TransformationMatrix");
    static readonly int AnchorPositionId = Shader.PropertyToID("_AnchorPosition");
    static readonly int HardnessId = Shader.PropertyToID("_Hardness");
    static readonly int RadiusId = Shader.PropertyToID("_Radius");

    void Update()
    {
        _softbodyMaterial = Renderer.sharedMaterial;
        
        Vector3 anchorPosition = Anchor.position;
        _transformationMatrix = Handle.localToWorldMatrix * Anchor.worldToLocalMatrix;

        _softbodyMaterial.SetMatrix(TransformationMatrixId, _transformationMatrix);
        _softbodyMaterial.SetVector(AnchorPositionId, anchorPosition);
        _softbodyMaterial.SetFloat(HardnessId, Hardness);
        _softbodyMaterial.SetFloat(RadiusId, Radius);
    }
}
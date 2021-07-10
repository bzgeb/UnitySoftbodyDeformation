using UnityEngine;

[ExecuteAlways]
public class Manipulator : MonoBehaviour
{
    public Transform Anchor;
    public Transform Handle;

    public Renderer Renderer;
    [Range(0, 1)] public float Hardness;
    public float Radius;

    static readonly int TransformationMatrixId = Shader.PropertyToID("_TransformationMatrix");
    static readonly int AnchorPositionId = Shader.PropertyToID("_AnchorPosition");
    static readonly int HardnessId = Shader.PropertyToID("_Hardness");
    static readonly int RadiusId = Shader.PropertyToID("_Radius");

    void Update()
    {
        var transformationMatrix = Handle.localToWorldMatrix * Anchor.worldToLocalMatrix;
        
        var softbodyMaterial = Renderer.sharedMaterial;
        
        softbodyMaterial.SetMatrix(TransformationMatrixId, transformationMatrix);
        softbodyMaterial.SetVector(AnchorPositionId, Anchor.position);
        softbodyMaterial.SetFloat(HardnessId, Hardness);
        softbodyMaterial.SetFloat(RadiusId, Radius);
    }
}
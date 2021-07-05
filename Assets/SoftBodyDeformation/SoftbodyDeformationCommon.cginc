float4x4 _TransformationMatrix;
float4 _AnchorPosition;

float _Hardness;
float _Radius;

float SphereMask(float3 position, float3 center, float radius, float hardness)
{
    return 1 - saturate((distance(position, center) - radius) / (1 - hardness));
}

float3 ApplyManipulator(float3 position, float4x4 transformationMatrix, float3 anchorPosition, float maskRadius, float maskHardness)
{
    float3 manipulatedPosition = mul(transformationMatrix, float4(position, 1)).xyz;

    const float falloff = SphereMask(position, anchorPosition, maskRadius, maskHardness);
    manipulatedPosition = lerp(position, manipulatedPosition, falloff);
    
    return manipulatedPosition;
}
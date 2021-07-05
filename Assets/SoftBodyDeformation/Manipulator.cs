using System;
using UnityEngine;

public class Manipulator : MonoBehaviour
{
    public Transform Anchor;
    public Transform Handle;

    Matrix4x4 _transformationMatrix;

    void Update()
    {
        Vector3 anchorPosition = Anchor.position;
        _transformationMatrix = Handle.localToWorldMatrix * Anchor.worldToLocalMatrix;
    }
}

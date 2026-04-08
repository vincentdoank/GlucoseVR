using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ArmStretchIK : MonoBehaviour
{
    public TwoBoneIKConstraint ik;
    public float maxStretchRatio = 1.3f; // up to 30% longer

    public enum ScaleAxis { X, Y, Z }
    public ScaleAxis stretchAxis = ScaleAxis.Y;

    private float defaultLength;

    void Start()
    {
        var data = ik.data;
        defaultLength = Vector3.Distance(data.root.position, data.mid.position)
                      + Vector3.Distance(data.mid.position, data.tip.position);
    }

    void LateUpdate()
    {
        var data = ik.data;
        float currentDist = Vector3.Distance(data.root.position, data.target.position);
        float stretch = Mathf.Clamp(currentDist / defaultLength, 1f, maxStretchRatio);

        Vector3 scale = Vector3.one;

        switch (stretchAxis)
        {
            case ScaleAxis.X: scale.x = stretch; break;
            case ScaleAxis.Y: scale.y = stretch; break;
            case ScaleAxis.Z: scale.z = stretch; break;
        }

        data.mid.localScale = scale;
        data.tip.localScale = scale;
    }
}

using UnityEngine;

public class ParticleFlow : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private ParticleSystem particleSystemRef;
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        if (pointA == null || pointB == null)
            return;

        Vector3 direction =
            (pointB.position - pointA.position).normalized;

        transform.position = pointA.position;
        transform.rotation = Quaternion.LookRotation(direction);

        var main = particleSystemRef.main;
        main.startSpeed = speed;
    }
}
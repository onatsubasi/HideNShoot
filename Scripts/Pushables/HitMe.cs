using UnityEngine;

public class HitMe : MonoBehaviour, IPushable
{
    private const float VelocityScaler = 4f;

    private void Awake()
    {
    }

    public void Push(Vector3 handVelocity, Vector3 hitPosition)
    {
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        var objectVelocity = rigidBody.velocity;

        var fistDirection = Vector3.Normalize(handVelocity);
        var fistComponent = Vector3.Dot(objectVelocity, fistDirection) * fistDirection;
        var nonFistComponent = objectVelocity - fistComponent;

        rigidBody.isKinematic = false;
        rigidBody.useGravity = true;
        var rawForce = nonFistComponent + fistComponent + handVelocity;
        rigidBody.AddForceAtPosition(rawForce * VelocityScaler, hitPosition);

    }
}

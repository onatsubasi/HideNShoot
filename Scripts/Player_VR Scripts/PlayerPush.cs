using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    [SerializeField] private HandSide handSide;

    private const int VelocityFrameCount = 9;
    private const float VelocityMultiplier = 175f;

    [HideInInspector] public Vector3 averageVelocity { get; private set; }
    private Vector3[] velocities;
    private Vector3 lastFramePosition;

    // Check for both grab and trigger state of the hand if a fist is required
    //bool isFistActive => (Input.GetAxis(InputTagManager.GetGrip( handSide)) >= InputTagManager.VRInputThreshold
    //                   && Input.GetAxis(InputTagManager.GetIndex(handSide)) >= InputTagManager.VRInputThreshold)
    //                   || Input.GetKey(KeyCode.O);

    private GameObject lastHitObject;
    private int lastFrameIndex;

    private void Awake()
    {
        velocities = new Vector3[VelocityFrameCount];
        lastHitObject = null;
        for (int i = 0; i < VelocityFrameCount; i++)
            velocities[i] = Vector3.zero;
        lastFramePosition = transform.position;
        lastFrameIndex    = 0;
    }

    private void Update()
    {
        var frameVelocity = (transform.position - lastFramePosition) / Time.deltaTime;
        averageVelocity = (averageVelocity * VelocityFrameCount - velocities[lastFrameIndex] + frameVelocity) / VelocityFrameCount;
        velocities[lastFrameIndex] = frameVelocity;
        lastFrameIndex++;

        lastFrameIndex %= VelocityFrameCount;
        //int _ = (lastFrameIndex >= VelocityFrameCount) ? (lastFrameIndex = 0) : 0;
    }

    private void LateUpdate()
    {
        lastFramePosition = transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PlayerTagHolder.PushableTag)) // if tag isn't Pushable, return
            return;

        if (lastHitObject == null ) // && isFistActive
        {
            lastHitObject = other.gameObject;
            other.GetComponent<IPushable>()?.Push(averageVelocity * VelocityMultiplier, other.ClosestPoint(transform.position));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lastHitObject != null && other.gameObject == lastHitObject)
        {
            lastHitObject = null;
        }
    }
}

using UnityEngine;

public class HandColliderSetup : MonoBehaviour
{
    public float colliderRadius = 0.02f; // Adjust this based on finger size

    void Start()
    {
        // Add colliders to both hands
        AddHandCollider(gameObject);
    }

    void AddHandCollider(GameObject hand)
    {
        if (hand.GetComponent<SphereCollider>() == null)
        {
            SphereCollider collider = hand.AddComponent<SphereCollider>();
            collider.radius = colliderRadius;
            collider.isTrigger = true; // Make sure it detects touch
        }

        // Tag the hand so dots recognize it
        hand.tag = "Hand";
    }
}

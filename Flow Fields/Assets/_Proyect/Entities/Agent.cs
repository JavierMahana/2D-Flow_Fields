using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float maxSpeed;
    public float maxForce;

    public Unit Daddy;
    public Rigidbody2D Rigidbody;


    private void FixedUpdate()
    {
        FollowFlowFiled();
    }


    public void FollowFlowFiled()
    {
        Vector2Int currentIdx = Daddy.map.WorldPositionToGridInices(transform.position);

        FlowDirection direction =  Daddy.flowField.FlowFieldValues[currentIdx.x, currentIdx.y];
        Vector2 forceDirection = FlowField.GetNormilizedVectorFromDirectionEnum(direction);

        Rigidbody.AddForce(forceDirection * maxForce);

        float dfr = Rigidbody.velocity.magnitude - maxSpeed;

        if (dfr > 0)
        {
            Rigidbody.AddForce(-forceDirection * dfr, ForceMode2D.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkElement : MonoBehaviour
{

    float generalCount;
    float localCount;
    public float maxGeneralCount;
    public float maxLocalCount;

    public PlayerController player;
    Vector3 offset;

    bool overIt;
    bool dropped;

    bool playerMarked;

    Vector3 moveVector;
    float verticalVelocity;

    public TextMesh counter;

    void Start()
    {
        offset = Vector3.zero;
        offset.y = 5f;
        ResetCounts();
    }

    void Update()
    {
        if (!playerMarked)
        {
            if (overIt)
            {
                counter.text = ((int)localCount).ToString();
                transform.position = player.transform.position + offset;
                localCount -= Time.deltaTime;
                if (localCount <= 0)
                {
                    Mark();
                    EndCount();
                }
            }
            else if (dropped)
            {
                counter.text = ((int)generalCount).ToString();
                if (!IsGrounded())
                {
                    verticalVelocity -= 7 * Time.deltaTime * 2;
                    moveVector.y = verticalVelocity;
                    transform.Translate(moveVector * Time.deltaTime);
                }
                generalCount -= Time.deltaTime;
                if (generalCount <= 0)
                {
                    ResetCounts();
                }
                else
                {
                    foreach (var pl in Physics.OverlapSphere(transform.position, 3f, 1 << 9))
                    {
                        if (pl.GetComponent<PlayerController>() != player)
                        {
                            if (Vector3.Distance(transform.position, pl.transform.position) < 2f)
                            {
                                player = pl.GetComponent<PlayerController>();
                                ResetCounts();
                            }
                        }
                    }
                }
            }
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f);
    }

    public void Drop()
    {
        moveVector = Vector3.zero;
        moveVector.x = Mathf.Sign(player.transform.forward.x) * 25f;   
        overIt = false;
        dropped = true;
    }

    void ResetCounts()
    {
        localCount = maxLocalCount;
        generalCount = maxGeneralCount;
        overIt = true;
        dropped = false;
        player.carryingMark = true;
        player.markElement = this;
        verticalVelocity = 0;
        transform.position = player.transform.position + offset;
    }

    public void StartCount()
    {
        playerMarked = false;
    }

    void EndCount()
    {
        playerMarked = true;
    }

    void Mark()
    {
        player.marked = true;
        Destroy(gameObject);
    }
}

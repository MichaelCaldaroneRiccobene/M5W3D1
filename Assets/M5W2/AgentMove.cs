using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Device;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class AgentMove : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 4;
    [SerializeField] private float runSpeed = 8;

    private NavMeshAgent agent;
    private LineRenderer lineRenderer;
    private NavMeshPath path;

    private bool onSwichtModeWalk;
    private bool onRunnig;

    private Vector3 startPos;

    private void Awake() => startPos = transform.position;

    private void Start()
    {    
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();

        path = new NavMeshPath();
    }

    private void Update()
    {
        agent.speed = onRunnig ? runSpeed : walkSpeed;

        InputPlayer();

        PathStatus();
    }

    private void InputPlayer()
    {
        // Swicht Walk Run
        if (Input.GetKeyDown(KeyCode.Q)) onSwichtModeWalk = !onSwichtModeWalk;

        // Run
        if (Input.GetKeyDown(KeyCode.LeftShift)) onRunnig = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) onRunnig = false;

        // Can Move
        if (Input.GetKeyDown(KeyCode.E)) agent.isStopped = !agent.isStopped;

        // Walk WASD
        if (onSwichtModeWalk) WalkWASD();

        // Walk Mouse Click
        if (Input.GetMouseButton(0)) GoOnDestination();
    }

    private void GoOnDestination()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (agent.CalculatePath(hit.point, path))
            {
                SetLineRenderNextocation();
                agent.destination = path.corners[path.corners.Length - 1];
            }
        }
    }

    private void WalkWASD()
    {
        agent.ResetPath();

        float x = Input.GetAxis("Horizontal"); float z = Input.GetAxis("Vertical");

        Vector3 fowr = Camera.main.transform.forward; Vector3 hori = Camera.main.transform.right;
        Vector3 direction = fowr * z + hori * x;

        agent.velocity = direction.normalized * agent.speed;
        return;
    }

    private void PathStatus()
    {
        switch (path.status)
        {
            case NavMeshPathStatus.PathInvalid:
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.red;
                break;
            case NavMeshPathStatus.PathPartial:
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.yellow;
                break;
            case NavMeshPathStatus.PathComplete:
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.green;
                break;
        }
    }

    private void SetLineRenderNextocation()
    {
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);
    }

    public void OnWarp() => agent.Warp(startPos);
}

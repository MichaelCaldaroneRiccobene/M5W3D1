using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentFollowPath : MonoBehaviour
{
    public enum StateAI {None,Patrol,Chase}

    [SerializeField] private Transform[] points;

    private NavMeshAgent agent;

    private int destinationIndex;
    private bool hasReachPoint;

    public Transform target;
    private Coroutine currentRoutine;

    private StateAI stateAI;
    private StateAI curretStateAI;
    public float timeSeeTargetLeft { get => timeSeeTargetRight; set => timeSeeTargetRight = Mathf.Clamp(value,0,10); }
    private float timeSeeTargetRight;

    private LineRenderer lineRendererFollwPath;
    private NavMeshPath pathFollw;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stateAI = StateAI.Patrol;

        lineRendererFollwPath = GetComponent<LineRenderer>();
        pathFollw = new NavMeshPath();   
    }

    private void Update()
    {
        DoTo();

        Debug.Log("Stato AI " + stateAI);
        Debug.Log("Time " + timeSeeTargetRight);

        TimerSeeTarget();
        SenseTarget();

        PathStatus();
    }

    private void TimerSeeTarget()
    {
        if (timeSeeTargetRight > 0 && target != null)
        {
            timeSeeTargetRight -= Time.deltaTime;
            curretStateAI = StateAI.Chase;
        }
        else curretStateAI = StateAI.Patrol;
    }

    private void PathStatus()
    {
        switch (pathFollw.status)
        {
            case NavMeshPathStatus.PathInvalid:
                lineRendererFollwPath.startColor = Color.black;
                lineRendererFollwPath.endColor = Color.red;
                break;
            case NavMeshPathStatus.PathPartial:
                lineRendererFollwPath.startColor = Color.black;
                lineRendererFollwPath.endColor = Color.yellow;
                break;
            case NavMeshPathStatus.PathComplete:
                lineRendererFollwPath.startColor = Color.black;
                lineRendererFollwPath.endColor = Color.green;
                break;
        }
    }

    private void DoTo()
    {
        if (curretStateAI != stateAI)
        {
            if(currentRoutine != null) StopCoroutine(currentRoutine);
            stateAI = curretStateAI;

            switch (stateAI)
            {
                default:
                    Debug.Log("Default case");
                    break;
                case StateAI.None:
                    break;
                case StateAI.Patrol:
                    currentRoutine = StartCoroutine(Patrol());
                    break;
                case StateAI.Chase:
                    currentRoutine = StartCoroutine(GoOnTarget());
                    break;
            }       
        }
    }

    private IEnumerator Patrol()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

        while (true)
        {
            if(agent.CalculatePath(points[destinationIndex].position,pathFollw))
            {
                SetLineRenderFollow();
                agent.destination = pathFollw.corners[pathFollw.corners.Length - 1];
            }

            while (agent.pathPending) yield return null;

            while (!hasReachPoint)
            {
                Debug.Log("punto" + destinationIndex + " nome " + points[destinationIndex].name);
                Debug.Log(agent.remainingDistance);

                if (agent.remainingDistance <= 1) hasReachPoint = true;
                yield return waitForSeconds;
            }

            yield return new WaitForSeconds(1);
            destinationIndex = (1 + destinationIndex) % points.Length;
            hasReachPoint = false;
        }
    }

    private IEnumerator GoOnTarget()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.5f);
        agent.ResetPath();

        while (target != null)
        {
            if (agent.CalculatePath(target.position, pathFollw))
            {
                SetLineRenderFollow();
                agent.destination = target.position;
            }

            yield return waitForSeconds;
        }
    }

    private void SenseTarget()
    {
        if (timeSeeTargetRight > 0 && target != null)
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, 5);

            foreach (Collider colin in collider)
            {
                AgentMove agentMove = colin.GetComponent<AgentMove>();
                if (agentMove != null) timeSeeTargetLeft = 10;
            }
        }
    }

    private void SetLineRenderFollow()
    {
        lineRendererFollwPath.positionCount = pathFollw.corners.Length;
        lineRendererFollwPath.SetPositions(pathFollw.corners);
    }

    public void OnSeeTarget(float timeSeeTargetLeft, Transform target)
    {
        this.timeSeeTargetLeft += timeSeeTargetLeft;
        this.target = target;
    }
}

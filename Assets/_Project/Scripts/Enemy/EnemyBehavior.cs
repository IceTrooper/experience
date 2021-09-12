using Atoms;
using System.Collections;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private TransformList playerFortressList;
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float rotationSpeed = 160f;

    [Header("Hacking")]
    [SerializeField] private LineRenderer hackingPipe;
    [SerializeField] private float damageAmount = 20;
    [SerializeField] private float damageDelay = 2;

    [Header("Events")]
    [SerializeField] private AtomEvent killedEvent;

    public bool CanHacking
    {
        get
        {
            return canHacking;
        }
        set
        {
            if(value != canHacking)
            {
                canHacking = value;
                animator.SetBool(hashCanHack, canHacking);
            }
        }
    }
    private bool canHacking;

    private Coroutine hackingCoroutine;


    public Transform Target => target;
    private Transform target;
    private Damagable targetDamagable;
    public bool IsTargetNearby => isTargetNearby;
    private bool isTargetNearby;

    // References
    private Rigidbody rb;
    private Animator animator;

    // Animations
    public static int hashCanHack = Animator.StringToHash("CanHack");

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckTarget();

        CanHacking = isTargetNearby && target != null;
    }

    private void FixedUpdate()
    {
        if(target != null && !isTargetNearby)
        {
            MoveAndRotate();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyBehavior otherEnemyBehavior = null;
        if(other.transform == target
            || (other.CompareTag(ProjectConstants.EnemyTag) && other.TryGetComponent<EnemyBehavior>(out otherEnemyBehavior) && otherEnemyBehavior.IsTargetNearby))
        {
            isTargetNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform == target)
        {
            isTargetNearby = false;
        }
    }

    public void PrepareToHack()
    {
        var pipeEndPosition = hackingPipe.transform.InverseTransformPoint(target.position);
        hackingPipe.SetPosition(hackingPipe.positionCount - 1, pipeEndPosition);
        hackingPipe.enabled = true;
        targetDamagable = target.GetComponent<Damagable>();
        hackingCoroutine = StartCoroutine(DoHacking());
    }

    public IEnumerator DoHacking()
    {
        var delayWFS = new WaitForSeconds(damageDelay);
        yield return delayWFS;

        while(canHacking)
        {
            targetDamagable.TakeDamage(damageAmount);
            yield return delayWFS;
        }

        yield return null;
    }

    public void StopHacking()
    {
        StopCoroutine(hackingCoroutine);
        hackingPipe.enabled = false;
        targetDamagable = null;
    }

    private void CheckTarget()
    {
        var newTarget = playerFortressList.GetClosest(transform.position);
        if(target != newTarget) isTargetNearby = false;
        target = newTarget;
    }

    private void MoveAndRotate()
    {
        var targetDirection = new Vector3(
            target.position.x - rb.position.x,
            0f,
            target.position.z - rb.position.z
        ).normalized;
        rb.velocity = targetDirection * movementSpeed + Vector3.up * rb.velocity.y;

        var newRotation = Quaternion.LookRotation(targetDirection, transform.up);
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, newRotation, rotationSpeed * Time.fixedDeltaTime));
    }

    public void OnDie()
    {
        killedEvent.Raise();
        Destroy(gameObject);
    }
}

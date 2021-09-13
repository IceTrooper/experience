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

    [Header("Particles")]
    [SerializeField] private GameObject dieParticles;

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

    /// <summary>
    /// Returns the current target for the enemy.
    /// </summary>
    public Transform Target => target;
    private Transform target;
    private Damagable targetDamagable;
    /// <summary>
    /// Is current target nearby?
    /// </summary>
    public bool IsTargetNearby => isTargetNearby;
    private bool isTargetNearby;

    // References
    private Rigidbody rb;
    private Animator animator;

    /// <summary>
    /// Hash for 'CanAttack' animation parameter.
    /// </summary>
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

    /// <summary>
    /// Called when there is the beginning of the hacking process.
    /// </summary>
    public void PrepareToHack()
    {
        var pipeEndPosition = hackingPipe.transform.InverseTransformPoint(target.position);
        hackingPipe.SetPosition(hackingPipe.positionCount - 1, pipeEndPosition);
        hackingPipe.enabled = true;
        targetDamagable = target.GetComponent<Damagable>();
        hackingCoroutine = StartCoroutine(DoHacking());
    }

    /// <summary>
    /// Coroutine indicating hacking process. Apply damage every time when delay pass.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Called to stop hacking when object has been already hacked.
    /// </summary>
    public void StopHacking()
    {
        StopCoroutine(hackingCoroutine);
        hackingPipe.enabled = false;
        targetDamagable = null;
    }

    /// <summary>
    /// Checks if there is any target to follow and takes closest.
    /// </summary>
    private void CheckTarget()
    {
        var newTarget = playerFortressList.GetClosest(transform.position);
        if(target != newTarget) isTargetNearby = false;
        target = newTarget;
    }

    /// <summary>
    /// Move and rotate object via physics.
    /// </summary>
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

    /// <summary>
    /// Handler for Die event.
    /// </summary>
    public void OnDie()
    {
        killedEvent.Raise();
        Instantiate(dieParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

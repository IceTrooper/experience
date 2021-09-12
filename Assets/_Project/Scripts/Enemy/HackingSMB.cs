using UnityEngine;

public class HackingSMB : StateMachineBehaviour
{
    private EnemyBehavior enemyBehavior;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemyBehavior == null)
        {
            enemyBehavior = animator.GetComponent<EnemyBehavior>();
        }
        enemyBehavior.PrepareToHack();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyBehavior.StopHacking();
    }
}

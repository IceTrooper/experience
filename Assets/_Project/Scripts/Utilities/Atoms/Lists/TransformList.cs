using UnityEngine;

namespace Atoms
{
    [CreateAssetMenu(fileName = "TransformList", menuName = "IceTrooper/Atoms/TransformList")]
    public class TransformList : AtomList<Transform>
    {
        public Transform GetClosest(Vector3 position)
        {
            Transform result = null;
            var smallestDistance = float.MaxValue;
            foreach(var i in items)
            {
                var sqrMagnitude = (i.position - position).sqrMagnitude;
                if(sqrMagnitude < smallestDistance)
                {
                    smallestDistance = sqrMagnitude;
                    result = i;
                }
            }
            return result;
        }
    }
}

using UnityEngine;
using UnityEngine.AI;

namespace Defenses
{
    public class MobBehavior : MonoBehaviour
    {
        private NavMeshAgent agent;

        [SerializeField] 
        private GameObject endZone;
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(endZone.transform.position);
        }
    }
}

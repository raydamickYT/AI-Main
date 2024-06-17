using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEngine.UI;
using UnityEngine.AI;

namespace BehaviourTree
{

    public class TaskFollowPlayer : Node
    {
        public Transform transform, playerTransform;
        private NavMeshAgent nav;
        private Tree BT;


        public TaskFollowPlayer(Transform _transform, Transform _playerTransform, NavMeshAgent _nav)
        {
            nav = _nav;
            transform = _transform;
            if (_playerTransform != null)
            {
                playerTransform = _playerTransform;
            }
            else
            {
                //in het geval dat de parent class geen playertransform bezit.
                playerTransform = null;
            }
        }
        public override void OnEnter()
        {
            // Debug.LogWarning("following player");

            base.OnEnter();
        }

        public override NodeState Evaluate()
        {
            //update tekst boven ai hoofd
            if (BT == null)
            {
                BT = transform.GetComponent<Tree>();
                BT.StateText.text = "TaskFollowPlayer";
            }
            else
            {
                BT.StateText.text = "TaskFollowPlayer";
            }

            // Debug.Log("running");
            if (playerTransform != null)
            {
                nav.SetDestination(playerTransform.position);
            }
            else
            {
                //in het geval dat de parent class geen playertransform bezit.
                //hier nog een extra flag switch, omdat het soms voorkomt dat de ai niet langs de check node komt.
                GlobalBlackboard.Instance.SetVariable(GlobalBlackboard.Instance.IsChasingPlayerStr, true);

                playerTransform = blackboard.GetVariable<Transform>(GuardBT.Settings.TargetStr);
                if (playerTransform != null)
                {
                    nav.SetDestination(playerTransform.position);
                    playerTransform = null;
                }
                else
                {
                    Debug.LogError($"Target not found using key {GuardBT.Settings.TargetStr}. Failing attack range check.");
                    state = NodeState.FAILURE;
                    return state;
                }
            }

            state = NodeState.RUNNING;
            // Debug.Log("state: " + state);
            return state;

        }
    }
}

using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

namespace BehaviourTree
{

    /// <summary>
    /// This node type operates by evaluating all its children simultaneously.
    /// The node succeeds if at least one child succeeds, fails if all children fail, and keeps running if at least one child is running.
    /// </summary>
    public class Parallel : Composite
    {
        public Parallel() : base() { }
        public Parallel(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            if (!wasEntered)
            {
                OnEnter();
                wasEntered = true;
            }

            bool anyChildRunning = false;
            bool anyChildSucceeded = false;

            foreach (Node node in Children)
            {
                if (!node.wasEntered)
                {
                    node.OnEnter();
                    node.wasEntered = true;
                }

                NodeState result = node.Evaluate();
                switch (result)
                {
                    case NodeState.FAILURE:
                        node.OnExit();
                        node.wasEntered = false;
                        continue;
                    case NodeState.SUCCES:
                        node.OnExit();
                        node.wasEntered = false;
                        anyChildSucceeded = true;
                        break;
                    case NodeState.RUNNING:
                        anyChildRunning = true;
                        break;
                }
            }

            if (anyChildSucceeded)
            {
                state = NodeState.SUCCES;
                OnExit();
                wasEntered = false;
            }
            else if (anyChildRunning)
            {
                state = NodeState.RUNNING;
            }
            else
            {
                state = NodeState.FAILURE;
                OnExit();
                wasEntered = false;
            }

            return state;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
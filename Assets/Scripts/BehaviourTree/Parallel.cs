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
            if (!WasEntered)
            {
                OnEnter();
                WasEntered = true;
            }

            bool anyChildRunning = false;
            bool anyChildSucceeded = false;

            foreach (Node node in Children)
            {
                if (!node.WasEntered)
                {
                    node.OnEnter();
                    node.WasEntered = true;
                }

                NodeState result = node.Evaluate();
                switch (result)
                {
                    case NodeState.FAILURE:
                        node.OnExit();
                        node.WasEntered = false;
                        continue;
                    case NodeState.SUCCES:
                        node.OnExit();
                        node.WasEntered = false;
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
                WasEntered = false;
            }
            else if (anyChildRunning)
            {
                state = NodeState.RUNNING;
            }
            else
            {
                state = NodeState.FAILURE;
                OnExit();
                WasEntered = false;
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
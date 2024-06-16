using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    /// <summary>
    /// This node type operates by checking its children, but it requires all children to succeed, one after another.
    /// If any node fails, the entire sequence fails.
    /// 
    /// If a child is running, it will just return that a child is running (the bool "anyChildIsRunning").
    /// 
    /// Similar to a logical AND operation.
    /// </summary>
    public class Sequence : Composite
    {
        private int currentIndex = 0;

        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            if (!WasEntered)
            {
                OnEnter();
                WasEntered = true;
            }
            for (int i = currentIndex; i < Children.Count; i++)
            {
                Node node = Children[i];

                if (!node.WasEntered)
                {
                    node.OnEnter();
                    node.WasEntered = true;
                }

                NodeState nodeState = node.Evaluate();

                switch (nodeState)
                {
                    case NodeState.FAILURE:
                        node.OnExit();
                        node.WasEntered = false;
                        state = NodeState.FAILURE;
                        currentIndex = 0; // Reset index on failure
                        OnExit();
                        WasEntered = false;
                        return state;
                    case NodeState.SUCCES:
                        node.OnExit();
                        node.WasEntered = false;
                        continue;
                    case NodeState.RUNNING:
                        // Debug.Log("Node " + i + " is running.");
                        currentIndex = i; // Store the current index
                        state = NodeState.RUNNING;
                        return state;
                }
            }

            state = NodeState.SUCCES;
            currentIndex = 0; // Reset index if all children succeed
            OnExit();
            WasEntered = false;
            return state;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            currentIndex = 0;
        }

        public override void OnExit()
        {
            base.OnExit();
            currentIndex = 0;
        }
    }
}

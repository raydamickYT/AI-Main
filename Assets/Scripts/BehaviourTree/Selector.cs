using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    /// <summary>
    /// This node type operates by checking all its children and stops once it finds a node that returns success or running.
    /// Similar to a logical OR operation.
    /// </summary>
    public class Selector : Composite
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            if (!wasEntered)
            {
                OnEnter();
                wasEntered = true;
            }

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
                        state = NodeState.SUCCES;
                        OnExit();
                        wasEntered = false;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                }
            }

            state = NodeState.FAILURE;
            OnExit();
            wasEntered = false;
            return state;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Entering Selector");
        }

        public override void OnExit()
        {
            base.OnExit();
            Debug.Log("Exiting Selector");
        }
    }
}

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
            if (!WasEntered)
            {
                OnEnter();
                WasEntered = true;
            }

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
                        state = NodeState.SUCCES;
                        OnExit();
                        WasEntered = false;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                }
            }

            state = NodeState.FAILURE;
            OnExit();
            WasEntered = false;
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

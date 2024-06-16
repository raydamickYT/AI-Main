using UnityEngine;
using BehaviourTree;
using System.Collections.Generic;

namespace BehaviourTree
{
    public class Composite : Node
    {
        public Composite() : base() { }

        public Composite(List<Node> _children)
        {
            foreach (Node Child in _children)
            {
                attach(Child);
            }
        }
        private void attach(Node node)
        {
            node.Parent = this;
            Children.Add(node);
        }

        public override void SetupBlackboard(Blackboard blackboard)
        {
            base.SetupBlackboard(blackboard);
            //takes the Children list in the Parent node
            foreach (Node node in Children)
            {
                node.SetupBlackboard(blackboard);
            }
        }
    }
}
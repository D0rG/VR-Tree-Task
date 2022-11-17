using System;

namespace TreeJsonUtility
{
    [Serializable]
    public class TreeNode : Node<string>
    {
        public TreeNode[] Node;
    }

    public abstract class Node<T>
    {
        public T Value;
    }
}

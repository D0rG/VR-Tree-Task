using System;

namespace TreeJsonUtility
{
    [Serializable]
    public struct TreeNode
    {
        public string Name;
        public TreeNode[] Node;
    }
}

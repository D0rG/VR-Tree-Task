using System.Collections;
using System.Collections.Generic;
using TreeJsonUtility;
using UnityEditor.Events;
using UnityEngine;

public class UnityTree : MonoBehaviour
{
    [SerializeField] private LayersColor _layersColor;
    [SerializeField] private UnityTreeNode _treeNodePrefab;
    private List<UnityTreeNode> _tree = new List<UnityTreeNode>();
    private TreeNode root;

    private void Start()
    {
        root = JsonTreeReader.instance.root; 
        DrawTree();
    }

    private void DrawTree()
    {
        DrawNode(root);
    }

    private void DrawNode(TreeNode node, UnityTreeNode parent = null, int layerNumber = 0)
    {
        var parentTransform = (parent != null) ? parent.transform : this.transform;
        var nodeSctipt = Instantiate(_treeNodePrefab, parentTransform);
        Color layerColor = _layersColor.colors[layerNumber % _layersColor.colors.Length];
        nodeSctipt.Init(layerColor, Vector3.zero, parent, node);
        _tree.Add(nodeSctipt);

        layerNumber++;
        foreach (var childNode in node.Node)
        {
            DrawNode(childNode, nodeSctipt, layerNumber);
        }
    }
}

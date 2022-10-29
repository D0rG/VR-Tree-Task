using System.Collections.Generic;
using TreeJsonUtility;
using UnityEngine;
using System.Linq;

public class UnityTree : MonoBehaviour
{
    [SerializeField] private LayersColor _layersColor;
    [SerializeField] private UnityTreeNode _treeNodePrefab;
    private List<UnityTreeNode> _tree = new List<UnityTreeNode>();
    private TreeNode root;
    private Dictionary<int, int> countOnLayer = new Dictionary<int, int>()
    {
        { 0, 1 },   //root node
    };

    private void Start()
    {
        root = JsonTreeReader.instance.root;
        DrawTree();
    }

    private void DrawTree()
    {
        CalculateCountOnLayer(root);
        foreach (var dic in countOnLayer)
        {
            Debug.Log($"layer: {dic.Key}, value: {dic.Value}");
        }
        DrawNode(root);
        ArrangeNode();
    }

    private void DrawNode(TreeNode node, UnityTreeNode parent = null, int layerNumber = 0)
    {
        var parentTransform = (parent != null) ? parent.transform : this.transform;
        var nodeSctipt = Instantiate(_treeNodePrefab, parentTransform);
        Color layerColor = _layersColor.colors[layerNumber % _layersColor.colors.Length];
        nodeSctipt.Init(layerColor, Vector3.zero, parent, node, layerNumber);
        _tree.Add(nodeSctipt);

        layerNumber++;
        foreach (var childNode in node.Node)
        {
            DrawNode(childNode, nodeSctipt, layerNumber);
        }
    }

    private void ArrangeNode()
    {
        for (int i = 1; i < countOnLayer.Count; i++)
        {
            int countOnCurrentLayer = countOnLayer[i];
            var nodesOnCurrentLayer = _tree
                .Where(x => x.layerNumber == i)
                .ToArray();
            float stepPositionXSize = nodesOnCurrentLayer[0].transform.localScale.x * 1.5f;
            var x = (stepPositionXSize * nodesOnCurrentLayer.Length / 2 + nodesOnCurrentLayer[0].transform.localScale.x);
            var currnetNodePosition = new Vector3(x, i * 1.5f, 0);
            foreach(var node in nodesOnCurrentLayer)
            {
                currnetNodePosition.x -= stepPositionXSize;
                node.transform.position = currnetNodePosition;
            }
        }
    }

    private void CalculateCountOnLayer(TreeNode rootNode, int layerNumber = 0)
    {
        layerNumber++;
        foreach (var childNode in rootNode.Node)
        {
            if (countOnLayer.ContainsKey(layerNumber))
            {
                countOnLayer[layerNumber]++;
            }
            else
            {
                countOnLayer[layerNumber] = 1;
            }
            CalculateCountOnLayer(childNode, layerNumber);
        }
    }
}

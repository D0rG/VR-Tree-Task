using System.Collections.Generic;
using TreeJsonUtility;
using UnityEngine;
using System.Linq;

public class UnityTree : MonoBehaviour
{
    [SerializeField] private LayersColor _layersColor;
    [SerializeField] private UnityTreeNode _treeNodePrefab;
    [SerializeField] private float _lineWidht = 0.055f;
    private List<UnityTreeNode> _tree = new List<UnityTreeNode>();
    private TreeNode _root;
    private Dictionary<int, int> _countOnLayer = new Dictionary<int, int>()
    {
        { 0, 1 },   //root node
    };
    private LineRenderer[] _lines;

    private void Start()
    {
        _root = JsonTreeReader.instance.root;
        DrawTree();
    }

    private void DrawTree()
    {
        CalculateCountOnLayer(_root);
        DrawNode(_root);
        ArrangeNode();
        _lines = _tree.Select(x => x.GetLine()).ToArray();
    }

    private void DrawNode(TreeNode node, UnityTreeNode parent = null, int layerNumber = 0)
    {
        var parentTransform = (parent != null) ? parent.transform : this.transform;
        var nodeSctipt = Instantiate(_treeNodePrefab, parentTransform);
        Color layerColor = _layersColor.colors[layerNumber % _layersColor.colors.Length];
        nodeSctipt.Init(layerColor, this.transform.position, parent, node, layerNumber);
        _tree.Add(nodeSctipt);

        layerNumber++;
        foreach (var childNode in node.Node)
        {
            DrawNode(childNode, nodeSctipt, layerNumber);
        }
    }

    private void ArrangeNode()
    {
        for (int i = 1; i < _countOnLayer.Count; i++)
        {
            int countOnCurrentLayer = _countOnLayer[i];
            var nodesOnCurrentLayer = _tree
                .Where(x => x.layerNumber == i)
                .ToArray();
            float stepPositionXSize = nodesOnCurrentLayer[0].transform.localScale.x * 1.5f;
            var x = (stepPositionXSize * nodesOnCurrentLayer.Length / 2 + nodesOnCurrentLayer[0].transform.localScale.x);
            var currnetNodePosition = new Vector3(transform.position.x + x, (i * 1.5f) + transform.position.y, transform.position.z);
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
            if (_countOnLayer.ContainsKey(layerNumber))
            {
                _countOnLayer[layerNumber]++;
            }
            else
            {
                _countOnLayer[layerNumber] = 1;
            }
            CalculateCountOnLayer(childNode, layerNumber);
        }
    }

    private void LateUpdate()
    {
        foreach(var line in _lines)
        {
            var point = line.widthCurve[0];
            point.value = transform.lossyScale.x * _lineWidht;
            line.widthCurve = new AnimationCurve(point);
        }
    }
}

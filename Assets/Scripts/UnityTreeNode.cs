using TMPro;
using TreeJsonUtility;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(LineRenderer))]
public sealed class UnityTreeNode : MonoBehaviour
{
    [Header("Required")]
    [SerializeField] private Transform _transform;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private TextMeshProUGUI _textMesh;

    [Header("Settings")]
    [Range(0, 1)] [SerializeField] private float sizeMultiplier;

    public UnityTreeNode parent { get; private set; }
    public int layerNumber { get; private set; }
    [SerializeField] private TreeNode node;
    public TreeNode currentNode
    {
        get { return node; }
        private set { node = value; }
    }

    public void Init(Color color, Vector3 spawnPos, UnityTreeNode parent, TreeNode node, int layerNumber)
    {
        this.parent = parent;
        this.layerNumber = layerNumber;
        currentNode = node;
        _transform.position = spawnPos;
        _meshRenderer.material.color = color;
        _textMesh.text = node.Name;

        if(parent == null) { return; }
        _transform.localScale = parent.transform.localScale * sizeMultiplier;
    }

    private void Awake()
    {
        _transform = this.transform;
    }

    private void FixedUpdate()
    {
        if (parent == null) { return; }
        ClearLine();
        _lineRenderer.SetPosition(0, parent._transform.position);
        _lineRenderer.SetPosition(1, this.transform.position);
    }

    private void ClearLine()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.positionCount = 2;
    }
}

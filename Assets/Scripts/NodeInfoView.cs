using System.Text;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(HandVRInput))]
public class NodeInfoView : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private TextMeshProUGUI _view;
    private bool _insideNode = false;
    private HandVRInput _handVRInput;

    private void Awake()
    {
        _handVRInput = GetComponent<HandVRInput>();
    }

    private void FixedUpdate()
    {
        _canvas.SetActive(_handVRInput.triggerState && _insideNode);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out UnityTreeNode node))
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(node.currentNode.Name);
            builder.Append("\n");
            if (node.parent != null)
            {
                builder.Append("Предок: ");
                builder.Append(node.parent.currentNode.Name);
                builder.Append("\n");
            }
            builder.Append("Потомки: ");
            foreach(var child in node.currentNode.Node)
            {
                builder.Append(child.Name);
                builder.Append(" ");
            }
            _view.text = builder.ToString();
            _insideNode = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _insideNode = false;
    }
}

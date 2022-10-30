using System.IO;
using UnityEngine;

namespace TreeJsonUtility
{
    public class JsonTreeReader : MonoBehaviour
    {
        public static JsonTreeReader instance;
        [SerializeField] private string _fileName;
        private string _path;
        [SerializeField] private TreeNode _root;
        public TreeNode root
        {
            get { return _root; }
            private set { _root = value; }
        }

        private void Awake()
        {
            if(instance == null) { instance = this; }

            _path = Path.Combine(Application.dataPath, _fileName);

            if (!File.Exists(_path))
            {
                root = new TreeNode
                {
                    Name = "���������",
                    Node = new TreeNode[]
                    {
                        new TreeNode
                        {
                            Name = "��������",
                            Node = new TreeNode[]
                            {
                                new TreeNode
                                {
                                    Name = "�������",
                                    Node = null
                                },
                                new TreeNode
                                {
                                    Name = "�����",
                                    Node = null
                                },
                                new TreeNode
                                {
                                    Name = "������",
                                    Node = null
                                }
                            }
                        },
                        new TreeNode
                        {
                            Name = "��������",
                            Node = new TreeNode[]
                            {
                                new TreeNode
                                {
                                    Name = "���������",
                                    Node = null
                                },
                                new TreeNode
                                {
                                    Name = "���",
                                    Node = null
                                },
                                new TreeNode
                                {
                                    Name = "�����������",
                                    Node = null
                                },
                                new TreeNode
                                {
                                    Name = "�������",
                                    Node = null
                                }
                            }
                        }
                    }
                };
                var jsonText = JsonUtility.ToJson(root);
                File.WriteAllText(_path, jsonText);
            }
            UpdateRootFormFile(_path);
        }

        private void UpdateRootFormFile(string _path)
        {
            var json = File.ReadAllText(_path);
            root = JsonUtility.FromJson<TreeNode>(json);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using TreeEditor;
using UnityEngine;

namespace TreeJsonUtility
{
    public class JsonTreeReader : MonoBehaviour
    {
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
            _path = Path.Combine(Application.dataPath, _fileName);

            if (File.Exists(_path))
            {
                var json = File.ReadAllText(_path);
                root = JsonUtility.FromJson<TreeNode>(json);
            }
            else
            {
                throw new FileNotFoundException($"Can't find file {_path}");
            }
        }
    }
}

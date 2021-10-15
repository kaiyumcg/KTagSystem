using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KTagSystem
{
    [System.Serializable]
    public class ParentTreeData
    {
        [SerializeField]
        List<KTag> tree;
        internal List<KTag> Tree { get { return tree; } set { tree = value; } }
    }

    [CreateAssetMenu(fileName = "New Tag", menuName = "KTag/Create New Tag", order = 1)]
    public class KTag : ScriptableObject
    {
        [SerializeField] string tagName = "";
        [SerializeField] bool isPlayer = false;
        [SerializeField] List<KTag> childTags;
        [CanNotEdit] [SerializeField] KTag parentTag;
        [SerializeField] [CanNotEdit] ParentTreeData parentsInfo;

        public bool IsPlayer { get { return isPlayer; } }
        public string TagName { get { return tagName; } }
        public List<KTag> ChildTags { get { return childTags; } }
        public KTag ParentTag { get { return parentTag; } }
        public List<KTag> ParentTree { get { return parentsInfo.Tree; } }

        internal void LoadReferencesIfReq()
        {
            if (parentTag != null)
            {
                if (parentTag.ChildTags == null || parentTag.ChildTags.Count == 0)
                {
                    parentTag = null;
                }
                else
                {
                    if (parentTag.ChildTags.Contains(this) == false)
                    {
                        parentTag = null;
                    }
                }
            }

            if (parentsInfo != null && parentsInfo.Tree != null && parentsInfo.Tree.Count > 0)
            {
                for (int i = 0; i < parentsInfo.Tree.Count; i++)
                {
                    var treeParTag = parentsInfo.Tree[i];
                    if (treeParTag == null) { continue; }
                    if (treeParTag.ChildTags == null || treeParTag.ChildTags.Count == 0)
                    {
                        parentsInfo.Tree[i] = null;
                    }
                    else
                    {
                        if (treeParTag.ChildTags.Contains(this) == false)
                        {
                            parentsInfo.Tree[i] = null;
                        }
                    }
                }
            }

            if (parentsInfo != null && parentsInfo.Tree != null && parentsInfo.Tree.Count > 0)
            {
                parentsInfo.Tree.RemoveAll((data) => { return data == null; });
            }

            if (ChildTags != null)
            {
                childTags.RemoveAll((tag) => { return tag == null; });
            }

            LoadParentTree();

            if (childTags != null && childTags.Count > 0)
            {
                for (int i = 0; i < childTags.Count; i++)
                {
                    var childTag = childTags[i];
                    if (childTag == null) { continue; }
                    childTag.parentTag = this;
                    childTag.LoadReferencesIfReq();
                }
            }
        }

        void LoadParentTree()
        {
            KTag parent = parentTag;
            if (parent != null)
            {
                parentsInfo = new ParentTreeData();
                parentsInfo.Tree = new List<KTag>();
                parentsInfo.Tree.Add(parent);

                int iteration = 0;
                while (parent != null)
                {
                    parent = parent.parentTag;
                    if (parentsInfo.Tree.Contains(parent) == false)
                    {
                        parentsInfo.Tree.Add(parent);
                    }
                    iteration++;

                    if (iteration > 40)
                    {
                        Debug.LogError("Can not support parent-child level greater than 40 or there is cyclic relation!" +
                            " Error generated from: " + this.name);
                        parentsInfo = null;
                        childTags = null;
                        parentTag = null;
                        break;
                    }
                }
            }
        }

        private void OnValidate()
        {
            LoadReferencesIfReq();
        }

        private void OnEnable()
        {
            LoadReferencesIfReq();
        }

        private void Awake()
        {
            LoadReferencesIfReq();
        }
    }
}
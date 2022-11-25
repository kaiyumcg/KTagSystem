using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KTagSystem
{
    public static class KTagExt
    {
        public static bool IsOrSubtypeOf(this KTag m_tag, KTag tag)
        {
            Debug.Log("entered!");

            if (m_tag.ParentTree == null)
            {
                return m_tag == tag;
            }
            else
            {
                return m_tag == tag || m_tag.ParentTree.Contains(tag);
            }
        }
    }
}
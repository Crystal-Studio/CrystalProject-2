/*
http://www.cgsoso.com/forum-257-1.html

CG搜搜 Unity3d 插件团购

CGSOSO 主打游戏开发，影视设计等CG资源素材。

每日Unity3d插件免费更新，仅限下载试用，如若商用，请务必官网购买！
*/

using UnityEngine;
using System.Collections;

public class DeadTime: MonoBehaviour
{
  public float deadTime = 1.5f;
  public bool destroyRoot;
  void Awake()
  {
    Destroy(!destroyRoot ? gameObject : transform.root.gameObject, deadTime);
  }
}

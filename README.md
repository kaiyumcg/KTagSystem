# KTagSystem
Relational Tag system for unity games. So you can have cow, sahiwal and wagyu tags and then have sahiwal and wagyu to be child tag of cow tag! Currently uses plain scriptable objects, I will use xNode to mode stuffs into a node based tag definition file. Very early stage prototype of an aspiring advaned tag system. Unity version: 2020.3.3f1 or up

#### Installation:
* Add an entry in your manifest.json as follows:
```C#
"com.kaiyum.ktagsystem": "https://github.com/kaiyumcg/KTagSystem.git"
```

Since unity does not support git dependencies, you need the following entries as well:
```C#
"com.kaiyum.attributeext" : "https://github.com/kaiyumcg/AttributeExt.git",
"com.kaiyum.unityext": "https://github.com/kaiyumcg/UnityExt.git",
"com.github.siccity.xnode": "https://github.com/siccity/xNode.git",
"com.kaiyum.editorutil": "https://github.com/kaiyumcg/EditorUtil.git"
```
Add them into your manifest.json file in "Packages\" directory of your unity project, if they are already not in manifest.json file.
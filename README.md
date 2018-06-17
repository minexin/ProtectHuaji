# "保卫滑稽" 项目介绍

## 队伍介绍：
### 1511455 王信
### 1511433 李沛昂
###	1511473 张爽
###	1511483 禚晨晨

## 项目功能：2D高难度射击游戏
### 本游戏名为“保卫滑稽”，故事发生在一卷古老的羊皮纸上，玩家将扮演纸上的滑稽，抵御异界侵袭而来的“黑脸”。随着游戏进程推进，黑脸将越来越多，而玩家也能不时收到来自纸外的援助“补给盒”，玩家坚持时间越长，消灭的黑脸越多，获得的积分也越多。而随着游戏的进行，幕后黑手也会不时排出得力手下来入侵，努力保卫滑稽吧！

## 技术框架：基于unity的2D移动游戏开发
### （1）unity：Unity是由Unity Technologies开发的一个让玩家轻松创建诸如三维视频游戏、建筑可视化、实时三维动画等类型互动内容的多平台的综合型游戏开发工具，是一个全面整合的专业游戏引擎。Unity类似于Director,Blender game engine, Virtools 或 Torque Game Builder等利用交互的图型化开发环境为首要方式的软件。其编辑器运行在Windows 和Mac OS X下，可发布游戏至Windows、Mac、Wii、iPhone、WebGL（需要HTML5）、Windows phone 8和Android平台。也可以利用Unity web player插件发布网页游戏，支持Mac和Windows的网页浏览。它的网页播放器也被Mac 所支持。

### （2）项目内容
#### 1.Plugins文件夹：里面包含了一些需要的配置文件，比如打包到Android时需要的AndroidManifest.xml，和一些外部引用，比如MySql.Data.dll
#### 2.Prefabs文件夹：里面包含了一些unity中会使用到的游戏对象GameObject，包括了怪物和BOSS、玩家和敌人所使用的武器等，在文件夹中存储后可避免重复创建GameObject，可提高游戏流畅性。
#### 3.Resources文件夹：里面包含了游戏中会用到的各种资源文件，包括了界面、地图、怪物、玩家、武器的形象，是整个游戏显示效果的根本。
#### 4.Scenes文件夹：里面包含了unity项目的全部场景game、record、start、upload，其中game为游戏界面，是其中核心，玩家进行游戏便位于其中，start为开始游戏界面，是主要控制界面，可从此界面开始游戏、查看前十积分排行榜record，upload界面则是game游戏结束后上传积分的界面，上传后可跳转排行榜record界面。
#### 5.Scripts文件夹：里面是控制游戏运行的C#代码，包含Control、Global、GUI三个文件夹。其中，Control主要是控制怪物、BOSS的行为，控制玩家的血量子弹数量等一些数据，Global内的脚本用于控制一些全局变量，比如暂停游戏、积分的传递等，GUI内的脚本主要用于控制用户界面的一些行为操作，用户通过手机界面上的轮盘行走、攻击以及用户操作进入游戏都是通过其中脚本完成。


### （3）后端支持：本游戏中包含上传游戏积分功能，故需要数据库支持，本游戏选择使用mysql数据库，通过服务器端nodejs获取mysql连接，游戏中通过c#脚本webservice连接获取传来的json数据，以此读取、操作数据库，实现了积分上传功能以及排行榜的显示功能，为游戏增添了趣味性。

服务端代码位于`DH database`文件夹中，MySQL数据库建表SQL语句位于`huaji.sql`文件中，项目文档位于`Document`文件夹中

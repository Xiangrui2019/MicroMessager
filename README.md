# MicroMessager --- 微信消息传输简单Demo

微信的架构设计Demo, 写的很简单, 可以用作学习.

1. 注意! 这个项目实现并不完全, 没有完全分布式, 仍然使用类似BT的Tracker服务器. 过段时间我会尝试使用Gossip协议来进行用户->服务器数据的维护.
2. 参考: [微信消息协议Anduin2017](https://anduin.aiursoft.com/post/2020/5/21/how-wechat-design-distributed-messging-protocol)

## 项目结构

MicroMessager.Tracker: 用户跟踪服务器, 类似于BT的Tracker, 用于对应用户->服务器Ip, 方便进行查找
MicroMessager.MessagerServer: 消息服务器, 真正的按照用户进行消息传递, 传输用户的消息.

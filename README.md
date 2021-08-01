# PhotonChatRx
As with [PUN2Rx](https://github.com/nekomimi-daimao/PUN2Rx), convert callbacks of [Photon Chat](https://www.photonengine.com/en-US/Chat) to [UniRx](https://github.com/neuecc/UniRx) Operators

## Installation via UPM

```json
{
  // ...
  "dependencies": {
    "dev.r01.photonchat2rx": "https://github.com/r01hee/PhotonChat2Rx.git",
    // ...
  }
  // ...
}
```
## Requirement
- Photon Chat v2.16
    - [AssetStore](https://assetstore.unity.com/packages/tools/network/photon-chat-45334)
    - [official](https://www.photonengine.com/en-US/Chat)

- UniRx v7.1.0
    - [AssetStore](https://assetstore.unity.com/packages/tools/integration/unirx-reactive-extensions-for-unity-17276)
    - [github](https://github.com/neuecc/UniRx)

## Example

```c#
using System;
using System.Linq;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Chat;
using PhotonChat2Rx;
using UniRx;

public class ChatExample : MonoBehaviour
{
    public string UserName { get => userName; set => userName = value; }

    // Set AppIdChat via Inspector
    [SerializeField] private ChatAppSettings chatAppSettings;

    [SerializeField] private ConnectionProtocol connectionProtocol;

    [SerializeField] private string defaultChannel = "default";

    [SerializeField] private string userName;

    private void Start()
    {
        if (string.IsNullOrEmpty(this.UserName))
        {
            this.UserName = "user" + Environment.TickCount % 99; //made-up username
        }

        var chatClient = new ChatClientWithObservable(this, connectionProtocol);

        chatClient.AuthValues = new AuthenticationValues(this.UserName);
        chatClient.ConnectUsingSettings(chatAppSettings);

        chatClient.OnConnectedAsObservable()
            .Do(x => Debug.Log("OnConnected"))
            .Take(1)
            .TakeUntilDestroy(this)
            .SubscribeWithState(chatClient, (_, c) => c.Subscribe(defaultChannel));

        chatClient.OnSubscribedAsObservable()
            .Do(x => Debug.Log("OnSubscribed: " + string.Join(", ", x.Item1)))
            .TakeUntilDestroy(this)
            .SelectMany(x => x.Item1)
            .SubscribeWithState(chatClient, (_, c) => c.PublishMessage(defaultChannel, $"Hello, I'm {c.UserId}."));

        chatClient.OnGetMessagesAsObservable()
            .TakeUntilDestroy(this)
            .Subscribe(x => Debug.Log($"OnGetMessages: '{x.Item1}' {string.Join(", ", Enumerable.Range(0, x.Item2.Length).Select(i => $"{x.Item2[i]}>{x.Item3[i]}"))}"));

        Observable.EveryUpdate()
            .TakeUntilDestroy(this)
            .SubscribeWithState(chatClient, (_, c) => c.Service());
    }
}
```

## LICENSE
[zlib license](https://github.com/r01hee/PhotonChat2Rx/blob/master/LICENSE)

## Special Thanks
This library is inspired by [nekomimi-daimao](https://qiita.com/nekomimi-daimao)'s [PUN2Rx](https://github.com/nekomimi-daimao/PUN2Rx)
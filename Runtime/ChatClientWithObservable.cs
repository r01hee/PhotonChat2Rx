using System;
using UnityEngine;
using UniRx;
using ExitGames.Client.Photon;
using Photon.Chat;

namespace PhotonChat2Rx
{
    public class ChatClientWithObservable : ChatClient
    {
        private readonly ChatClientListenerTriggers triggers;

        public ChatClientWithObservable(Component component, ConnectionProtocol protocol = ConnectionProtocol.Udp) : this(component?.gameObject, protocol) { }

        public ChatClientWithObservable(GameObject gameObject, ConnectionProtocol protocol = ConnectionProtocol.Udp)
            : base(gameObject == null ? throw new ArgumentNullException(nameof(gameObject)) : GetOrAddComponent<ChatClientListenerTriggers>(gameObject), protocol)
        {
            triggers = gameObject.GetComponent<ChatClientListenerTriggers>();
        }

        public IObservable<Tuple<DebugLevel, string>> DebugReturnAsObservable()
        {
            return triggers.DebugReturnAsObservable();
        }

        public IObservable<ChatState> OnChatStateChangeAsObservable()
        {
            return triggers.OnChatStateChangeAsObservable();
        }

        public IObservable<Unit> OnConnectedAsObservable()
        {
            return triggers.OnConnectedAsObservable();
        }

        public IObservable<Unit> OnDisconnectedAsObservable()
        {
            return triggers.OnDisconnectedAsObservable();
        }

        public IObservable<Tuple<string, string[], object[]>> OnGetMessagesAsObservable()
        {
            return triggers.OnGetMessagesAsObservable();
        }

        public IObservable<Tuple<string, object, string>> OnPrivateMessageAsObservable()
        {
            return triggers.OnPrivateMessageAsObservable();
        }

        public IObservable<Tuple<string, int, bool, object>> OnStatusUpdateAsObservable()
        {
            return triggers.OnStatusUpdateAsObservable();
        }

        public IObservable<Tuple<string[], bool[]>> OnSubscribedAsObservable()
        {
            return triggers.OnSubscribedAsObservable();
        }

        public IObservable<string[]> OnUnsubscribedAsObservable()
        {
            return triggers.OnUnsubscribedAsObservable();
        }

        public IObservable<Tuple<string, string>> OnUserSubscribedAsObservable()
        {
            return triggers.OnUserSubscribedAsObservable();
        }

        public IObservable<Tuple<string, string>> OnUserUnsubscribedAsObservable()
        {
            return triggers.OnUserUnsubscribedAsObservable();
        }

        private static T GetOrAddComponent<T>(GameObject gameObject)
            where T : Component
        {
            var component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
    }
}

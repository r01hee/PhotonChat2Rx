using System;
using UniRx;
using UniRx.Triggers;
using ExitGames.Client.Photon;
using Photon.Chat;

namespace PhotonChat2Rx
{
    public class ChatClientListenerTriggers : ObservableTriggerBase, IChatClientListener
    {
        #region ChatClientListeners

        private Subject<Tuple<DebugLevel, string>> debugReturn;

        public void DebugReturn(DebugLevel level, string message)
        {
            debugReturn?.OnNext(Tuple.Create(level, message));
        }

        public IObservable<Tuple<DebugLevel, string>> DebugReturnAsObservable()
        {
            return debugReturn ?? (debugReturn = new Subject<Tuple<DebugLevel, string>>());
        }

        private Subject<ChatState> onChatStateChange;

        public void OnChatStateChange(ChatState state)
        {
            onChatStateChange?.OnNext(state);
        }

        public IObservable<ChatState> OnChatStateChangeAsObservable()
        {
            return onChatStateChange ?? (onChatStateChange = new Subject<ChatState>());
        }

        private Subject<Unit> onConnected;

        public void OnConnected()
        {
            onConnected?.OnNext(Unit.Default);
        }

        public IObservable<Unit> OnConnectedAsObservable()
        {
            return onConnected ?? (onConnected = new Subject<Unit>());
        }

        private Subject<Unit> onDisconnected;

        public void OnDisconnected()
        {
            onDisconnected?.OnNext(Unit.Default);
        }

        public IObservable<Unit> OnDisconnectedAsObservable()
        {
            return onDisconnected ?? (onDisconnected = new Subject<Unit>());
        }

        private Subject<Tuple<string, string[], object[]>> onGetMessages;

        public void OnGetMessages(string channelName, string[] senders, object[] messages)
        {
            onGetMessages?.OnNext(Tuple.Create(channelName, senders, messages));
        }

        public IObservable<Tuple<string, string[], object[]>> OnGetMessagesAsObservable()
        {
            return onGetMessages ?? (onGetMessages = new Subject<Tuple<string, string[], object[]>>());
        }

        private Subject<Tuple<string, object, string>> onPrivateMessage;

        public void OnPrivateMessage(string sender, object message, string channelName)
        {
            onPrivateMessage?.OnNext(Tuple.Create(sender, message, channelName));
        }

        public IObservable<Tuple<string, object, string>> OnPrivateMessageAsObservable()
        {
            return onPrivateMessage ?? (onPrivateMessage = new Subject<Tuple<string, object, string>>());
        }

        private Subject<Tuple<string, int, bool, object>> onStatusUpdate;

        public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {
            onStatusUpdate?.OnNext(Tuple.Create(user, status, gotMessage, message));
        }

        public IObservable<Tuple<string, int, bool, object>> OnStatusUpdateAsObservable()
        {
            return onStatusUpdate ?? (onStatusUpdate = new Subject<Tuple<string, int, bool, object>>());
        }

        private Subject<Tuple<string[], bool[]>> onSubscribed;

        public void OnSubscribed(string[] channels, bool[] results)
        {
            onSubscribed?.OnNext(Tuple.Create(channels, results));
        }

        public IObservable<Tuple<string[], bool[]>> OnSubscribedAsObservable()
        {
            return onSubscribed ?? (onSubscribed = new Subject<Tuple<string[], bool[]>>());
        }

        private Subject<string[]> onUnsubscribed;

        public void OnUnsubscribed(string[] channels)
        {
            onUnsubscribed?.OnNext(channels);
        }

        public IObservable<string[]> OnUnsubscribedAsObservable()
        {
            return onUnsubscribed ?? (onUnsubscribed = new Subject<string[]>());
        }

        private Subject<Tuple<string, string>> onUserSubscribed;

        public void OnUserSubscribed(string channel, string user)
        {
            onUserSubscribed?.OnNext(Tuple.Create(channel, user));
        }

        public IObservable<Tuple<string, string>> OnUserSubscribedAsObservable()
        {
            return onUserSubscribed ?? (onUserSubscribed = new Subject<Tuple<string, string>>());
        }

        private Subject<Tuple<string, string>> onUserUnsubscribed;

        public void OnUserUnsubscribed(string channel, string user)
        {
            onUserUnsubscribed?.OnNext(Tuple.Create(channel, user));
        }

        public IObservable<Tuple<string, string>> OnUserUnsubscribedAsObservable()
        {
            return onUserUnsubscribed ?? (onUserUnsubscribed = new Subject<Tuple<string, string>>());
        }

        #endregion

        #region UniRx

        protected override void RaiseOnCompletedOnDestroy()
        {
            debugReturn?.OnCompleted();
            onChatStateChange?.OnCompleted();
            onConnected?.OnCompleted();
            onDisconnected?.OnCompleted();
            onGetMessages?.OnCompleted();
            onPrivateMessage?.OnCompleted();
            onStatusUpdate?.OnCompleted();
            onSubscribed?.OnCompleted();
            onUnsubscribed?.OnCompleted();
            onUserSubscribed?.OnCompleted();
            onUserUnsubscribed?.OnCompleted();
        }

        #endregion
    }
}

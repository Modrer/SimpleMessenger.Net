namespace MessegnerBackend.Models.Notification
{
    public class NotificationManager
    {
        public delegate void Notify(Notification notification);
        private Dictionary<int, List<Notify>> Notificants
        {
            get => _notificants;
            set => _notificants = value;
        }

        private static readonly NotificationManager s_sender = new();

        private Dictionary<int, List<Notify>> _notificants = [];

        public static NotificationManager GetInstance()
        {
            return s_sender;
        }

        private NotificationManager() { }

        public void AddReceiver(int id, Notify channel)
        {
            lock (Notificants)
            {

                if (Notificants.ContainsKey(id))
                {
                    Notificants[id].Add(channel);
                }
                else
                {
                    Notificants.Add(id, [channel]);
                }
            }
        }
        public void RemoveReceiver(int id, Notify channel)
        {
            lock (Notificants)
            {

                if (!Notificants.TryGetValue(id, out List<Notify>? value))
                {
                    return;
                }

                value.Remove(channel);

                if (value.Count == 0)
                {
                    Notificants.Remove(id);
                }
            }
        }

        private void _Send(int id, Notification notification)
        {
            if (!Notificants.TryGetValue(id, out var value))
            {
                return;
            }

            foreach (var channel in value)
            {

                channel.Invoke(notification);

            }

        }

        public void Send(int id, Notification notification)
        {
            lock (Notificants)
            {
                _Send(id, notification);
            }
        }

        public void Send(IEnumerable<int> ids, Notification notification)
        {
            lock (Notificants)
            {
                List<Task> tasks = [];

                foreach (var id in ids)
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        _Send(id, notification);
                    }));
                }

                Task.WhenAll(tasks).Wait();

            }

        }
    }
}

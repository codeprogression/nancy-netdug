using System;
using System.Collections.Generic;
using System.IO;
using Nancy.Json;
using System.Linq;

namespace NancyDemo.Models
{
    public class Repository
    {
        readonly JavaScriptSerializer _serializer;
        static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static readonly string AppData = Path.Combine(BaseDirectory,"App_Data");
        static readonly object Locker = new object();
        
        public Repository(JavaScriptSerializer serializer)
        {
            _serializer = serializer;
        }

        public IList<Meeting> Meetings
        {
            get { return GetCollection<Meeting>(); }
        } 

        public IList<CommunityEvent> Events
        {
            get { return GetCollection<CommunityEvent>(); }
        } 

        public IList<T> GetCollection<T>()
        {
            var fileStream = new FileStream(Path.Combine(AppData, typeof (T).Name + "s.json"), FileMode.Open,
                                            FileAccess.Read);
            using (var stream = new StreamReader(fileStream))
            {
                var json = stream.ReadToEnd();
                return _serializer.Deserialize<IList<T>>(json);
            }
        }


        public void SaveCollection<T>(IList<T> list) where T: Entity
        {
            lock (Locker)
            {
                var maxId = list.Max(x => x.Id);
                foreach (var t in list.Where(t => !t.Id.HasValue))
                {
                    t.Id = ++maxId;
                }
                var fileStream = new FileStream(Path.Combine(AppData, typeof (T).Name + "s.json"), FileMode.Truncate,
                                                FileAccess.Write);
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.Write(_serializer.Serialize(list));
                }
            }

        }
    }
}
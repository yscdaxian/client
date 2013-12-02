namespace AgentHelper.SipPhone
{
    using Sipek.Common;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    public class CCallLog : ICallLogInterface
    {
        private Stack<CCallRecord> _callList;
        private const string COUNT = "Count";
        private const string DATETIME = "Time";
        private const string DURATION = "Duration";
        private const string NAME = "Name";
        private const string NUMBER = "Number";
        private const string TYPE = "Type";
        private string XMLCallLogFile = "calllog.xml";

        public CCallLog()
        {
            this.load();
        }

        public void addCall(ECallType type, string number, string name, DateTime time, TimeSpan duration)
        {
            int totalSeconds = (int) duration.TotalSeconds;
            CCallRecord record = new CCallRecord {
                Name = name,
                Number = number,
                Duration = duration,
                Type = type,
                Time = time
            };
            this.addRecord(record);
        }

        protected void addRecord(CCallRecord record)
        {
            CCallRecord record2 = this.findRecord(record.Number, record.Type);
            if ((record2 == null) || (record.Type != record2.Type))
            {
                this._callList.Push(record);
            }
            else
            {
                this.deleteRecord(record);
                record.Count = record2.Count + 1;
                this._callList.Push(record);
            }
        }

        public void clearAll()
        {
            this._callList.Clear();
            this.save();
        }

        public void deleteRecord(CCallRecord record)
        {
            this.deleteRecord(record.Number, record.Type);
        }

        public void deleteRecord(string number, ECallType type)
        {
            List<CCallRecord> list = new List<CCallRecord>(this._callList.ToArray());
            list.Reverse();
            this._callList.Clear();
            foreach (CCallRecord record in list)
            {
                if (!(record.Number == number) || (record.Type != type))
                {
                    this._callList.Push(record);
                }
            }
        }

        private CCallRecord findRecord(string number, ECallType type)
        {
            foreach (CCallRecord record in this._callList)
            {
                if ((record.Number == number) && (record.Type == type))
                {
                    return record;
                }
            }
            return null;
        }

        public Stack<CCallRecord> getList()
        {
            return this._callList;
        }

        public Stack<CCallRecord> getList(ECallType type)
        {
            Stack<CCallRecord> stack = new Stack<CCallRecord>();
            foreach (CCallRecord record in this._callList)
            {
                if ((record.Type == type) || (type == ECallType.EAll))
                {
                    stack.Push(record);
                }
            }
            return stack;
        }

        private void load()
        {
            this.load(this.XMLCallLogFile);
        }

        public void load(string fileName)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(fileName);
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
                XmlNode newChild = document.CreateNode("element", "Calllog", "");
                document.AppendChild(newChild);
            }
            catch (XmlException exception2)
            {
                Console.WriteLine(exception2.Message);
            }
            XmlNodeList list = document.SelectNodes("/Calllog/Record");
            this._callList = new Stack<CCallRecord>();
            foreach (XmlNode node2 in list)
            {
                CCallRecord item = new CCallRecord();
                XmlNode node3 = node2.SelectSingleNode("Name");
                if (((node3 != null) && (node3.FirstChild != null)) && (node3.FirstChild.Value != null))
                {
                    item.Name = node3.FirstChild.Value;
                }
                node3 = node2.SelectSingleNode("Number");
                if (((node3 != null) && (node3.FirstChild != null)) && (node3.FirstChild.Value != null))
                {
                    item.Number = node3.FirstChild.Value;
                }
                else
                {
                    continue;
                }
                node3 = node2.SelectSingleNode("Time");
                if (((node3 != null) && (node3.FirstChild != null)) && (node3.FirstChild.Value != null))
                {
                    item.Time = DateTime.Parse(node3.FirstChild.Value);
                }
                node3 = node2.SelectSingleNode("Duration");
                if (((node3 != null) && (node3.FirstChild != null)) && (node3.FirstChild.Value != null))
                {
                    item.Duration = TimeSpan.Parse(node3.FirstChild.Value);
                }
                node3 = node2.SelectSingleNode("Count");
                if (((node3 != null) && (node3.FirstChild != null)) && (node3.FirstChild.Value != null))
                {
                    item.Count = int.Parse(node3.FirstChild.Value);
                }
                node3 = node2.SelectSingleNode("Type");
                if (((node3 != null) && (node3.FirstChild != null)) && (node3.FirstChild.Value != null))
                {
                    item.Type = (ECallType) int.Parse(node3.FirstChild.Value);
                }
                this._callList.Push(item);
            }
        }

        public void save()
        {
        }

        public int Count
        {
            get
            {
                return this._callList.Count;
            }
        }

        public CCallRecord Top
        {
            get
            {
                return this._callList.Peek();
            }
        }
    }
}

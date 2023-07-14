using Microsoft.Extensions.DependencyInjection;
using snus_back.Models;
using snus_back.Repositories;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace snus_back.Services
{
    public class ScanService
    {
        private Dictionary<string, AnalogInput> activeAnalogInputs= new Dictionary<string, AnalogInput>();
        private Dictionary<string, DigitalInput> activeDigitalInputs = new Dictionary<string, DigitalInput>();
        private ICollection<TagRecord> tagRecords = new List<TagRecord>();
        private ICollection<AlarmRecord> alarmRecords = new List<AlarmRecord>();


        private readonly ThreadLocal<TagRepository> _tagRepository;
        private readonly ThreadLocal<AlarmRepository> _alarmRepository;


        private TagRepository tagRepository;
        private AlarmRepository alarmRepository;
        private IOEntryRepository ioEntryRepository;

        private readonly object _lock = new object();

        public ScanService(TagRepository tagRepository, IOEntryRepository iOEntryRepository, AlarmRepository alarmRepository)
        {
            this.tagRepository = tagRepository;
            this.alarmRepository = alarmRepository;
            this.ioEntryRepository = iOEntryRepository;

        }

        public void Run()
        {
            InitDictionary();
            var threads = new List<Thread>();

            foreach (string add in activeAnalogInputs.Keys)
            {
                Thread t;
                if ("SCR".Contains(add))
                    t = new Thread(ScanSimulationAnalog);
                else
                    t = new Thread(ScanRTUAnalog);
                t.Start(add);
            }

            foreach (string add in activeDigitalInputs.Keys)
            {
                Thread t;
                if ("SCR".Contains(add))
                    t = new Thread(ScanSimulationDigital);
                else
                    t = new Thread(ScanRTUDigital);
                t.Start(add);
            }



            while (true)
            {
                Thread.Sleep(3000);
                tagRepository.UpdateDigitalInputs(activeDigitalInputs);
                tagRepository.UpdateAnalogInputs(activeAnalogInputs);
                tagRepository.AddTagRecords(new List<TagRecord>(tagRecords));
                tagRecords.Clear();
                alarmRepository.AddAlarmRecords(new List<AlarmRecord>(alarmRecords));
                alarmRecords.Clear();
            }
        }
        private void InitDictionary()
        {
            foreach (AnalogInput input in tagRepository.getAllAnalogInputs())
            {
                activeAnalogInputs.Add(input.IOAddress, input);
            }
            foreach (DigitalInput input in tagRepository.getAllDigitalInputs())
            {
                activeDigitalInputs.Add(input.IOAddress, input);
            }
        }

        public void ScanRTUAnalog(object param)
        {
            string address = (string)param;
            double currentValue = -10000;
            int scanTime = activeAnalogInputs[address].ScanTime;
            List<Alarm> alarms;
            Alarm currentAlarm;
            while (true)
            {
                if (activeAnalogInputs[address].IsScanOn)
                {
                    // read new scanned value from dictionary
                    currentValue = IOEntryRepository.entries[address];

                    // check for alarms and alarm update if one is raised
                    // alarms are loaded every time because of posibility that new alarm has been added to the tag
                    lock (_lock)
                    {
                        alarms = activeAnalogInputs[address].Alarms.ToList();
                    }
                    currentAlarm = null;
                    foreach (Alarm alarm in activeAnalogInputs[address].Alarms.ToList())
                    {
                        if (alarm.Type == AlarmType.HIGHER && currentValue >= alarm.Value)
                        {
                            if (currentAlarm == null)
                            {
                                currentAlarm = alarm;
                            }
                            if (currentAlarm != null && currentAlarm.Priority < alarm.Priority)
                            {
                                currentAlarm = alarm;
                            }
                        }
                        if (alarm.Type == AlarmType.LOWER && currentValue <= alarm.Value)
                        {
                            if (currentAlarm == null)
                            {
                                currentAlarm = alarm;
                            }
                            if (currentAlarm != null && currentAlarm.Priority < alarm.Priority)
                            {
                                currentAlarm = alarm;
                            }
                        }
                    }

                    // update AnalogInput's value in db
                    lock (_lock)
                    {
                        try
                        {
                            activeAnalogInputs[address].Value = currentValue;
                        }
                        catch (Exception e)
                        {
                            // something
                        }
                    }

                    if (currentAlarm != null)
                    {
                        AlarmRecord alarmRecord = new AlarmRecord { AlarmId = currentAlarm.Id, Timestamp = DateTime.Now, TagId = activeAnalogInputs[address].Id };
                        lock (_lock)
                        {
                            alarmRecords.Add(alarmRecord);
                        }
                    }

                    // add new tagRecord every time AnalogInput is updated 
                    TagRecord tagRecord = new TagRecord { Tag = activeAnalogInputs[address], Value = currentValue, Timestamp = DateTime.Now, TagId = activeAnalogInputs[address].Id };
                    lock(_lock)
                    {
                        tagRecords.Add(tagRecord);
                    }
                }
                Thread.Sleep(scanTime);
            }
        }

        public void ScanSimulationAnalog(object param)
        {
            string address = (string)param;
            double currentValue = -10000;
            int scanTime = activeAnalogInputs[address].ScanTime;
            while (true)
            {
                if (activeAnalogInputs[address].IsScanOn)
                {
                    // read new scanned value from dictionary and update AnalogInput's value in db
                    currentValue = SimulationDriver.ReturnValue(address);
                    lock (_lock)
                    {
                        try
                        {
                            activeAnalogInputs[address].Value = currentValue;
                        }
                        catch (Exception e)
                        {
                            // something
                        }
                    }

                    // add new tagRecord every time AnalogInput is updated 
                    TagRecord tagRecord = new TagRecord { Tag = activeAnalogInputs[address], Value = currentValue, Timestamp = DateTime.Now, TagId = activeAnalogInputs[address].Id };
                    lock (_lock)
                    {
                        tagRecords.Add(tagRecord);
                    }
                }
                Thread.Sleep(scanTime);
            }
        }

        public void ScanRTUDigital(object param)
        { 
            string address = (string)param;
            double currentValue = 0;
            int scanTime = activeDigitalInputs[address].ScanTime;
            while (true)
            {
                if (activeDigitalInputs[address].IsScanOn)
                {
                    // read new scanned value from dictionary and update AnalogInput's value in db
                    currentValue = IOEntryRepository.entries[address];
                    lock (_lock)
                    {
                        try
                        {
                            activeDigitalInputs[address].Value = currentValue;
                        }
                        catch (Exception e)
                        {
                            // something
                        }
                    }

                    // add new tagRecord every time AnalogInput is updated 
                    TagRecord tagRecord = new TagRecord { Tag = activeDigitalInputs[address], Value = currentValue, Timestamp = DateTime.Now, TagId = activeDigitalInputs[address].Id };
                    lock (_lock)
                    {
                        tagRecords.Add(tagRecord);
                    }
                }
                Thread.Sleep(scanTime);
            }
        }

        public void ScanSimulationDigital(object param)
        {
            string address = (string)param;
            double currentValue = 0;
            int scanTime = activeDigitalInputs[address].ScanTime;
            while (true)
            {
                if (activeDigitalInputs[address].IsScanOn)
                {
                    // read new scanned value from dictionary and update AnalogInput's value in db
                    currentValue = SimulationDriver.ReturnValue(address);
                    lock (_lock)
                    {
                        try
                        {
                            activeDigitalInputs[address].Value = currentValue;
                        }
                        catch (Exception e)
                        {
                            // something
                        }
                    }

                    // add new tagRecord every time AnalogInput is updated 
                    TagRecord tagRecord = new TagRecord { Tag = activeDigitalInputs[address], Value = currentValue, Timestamp = DateTime.Now, TagId = activeDigitalInputs[address].Id };
                    lock (_lock)
                    {
                        tagRecords.Add(tagRecord);
                    }
                }
                Thread.Sleep(scanTime);
            }
        }
    }
}

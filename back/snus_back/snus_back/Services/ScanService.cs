using Microsoft.AspNetCore.SignalR;
using snus_back.DTOs;
using snus_back.Hubs;
using snus_back.Models;
using snus_back.Repositories;
using snus_back.WebSockets;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace snus_back.Services
{
    public class ScanService
    {
        private Dictionary<int, AnalogInput> activeAnalogInputs= new Dictionary<int, AnalogInput>();
        private Dictionary<int, DigitalInput> activeDigitalInputs = new Dictionary<int, DigitalInput>();
        private ICollection<TagRecord> tagRecords = new List<TagRecord>();
        private ICollection<AlarmRecord> alarmRecords = new List<AlarmRecord>();


        private readonly ThreadLocal<TagRepository> _tagRepository;
        private readonly ThreadLocal<AlarmRepository> _alarmRepository;


        private TagRepository tagRepository;
        private AlarmRepository alarmRepository;
        private IOEntryRepository ioEntryRepository;
        private UpdateAlarmHandler udateAlarmHandler;
        private UpdateInputHandler updateInputHandler;
        private readonly IHubContext<UpdateInputHub> inputHub;
        private readonly IHubContext<UpdateAlarmHub> alarmHub;
        private readonly object _lock = new object();

        public ScanService(TagRepository tagRepository, IOEntryRepository iOEntryRepository, AlarmRepository alarmRepository, UpdateAlarmHandler updateAlarmHandler, UpdateInputHandler updateInputHandler,
            IHubContext<UpdateInputHub> inputHub, IHubContext<UpdateAlarmHub> alarmHub)
        {
            this.tagRepository = tagRepository;
            this.alarmRepository = alarmRepository;
            this.ioEntryRepository = iOEntryRepository;
            this.udateAlarmHandler = updateAlarmHandler;
            this.updateInputHandler = updateInputHandler;
            this.inputHub = inputHub;
            this.alarmHub = alarmHub;
        }

        public void AddNewTagThread(AnalogInput tag)
        {
            activeAnalogInputs.Add(tag.Id, tag);
            Thread t;
            if ("SCR".Contains(tag.IOAddress))
                t = new Thread(ScanSimulationAnalog);
            else
                t = new Thread(ScanRTUAnalog);
            t.Start(tag.Id);
        }

        public void AddNewTagThread(DigitalInput tag)
        {
            activeDigitalInputs.Add(tag.Id, tag);
            Thread t;
            if ("SCR".Contains(tag.IOAddress))
                t = new Thread(ScanSimulationDigital);
            else
                t = new Thread(ScanRTUDigital);
            t.Start(tag.Id);
        }

        public void Run()
        {
            InitDictionary();
            var threads = new List<Thread>();

            // OVO ZA ADD TAG
            foreach (int id in activeAnalogInputs.Keys)
            {
                Thread t;
                if ("SCR".Contains(activeAnalogInputs[id].IOAddress))
                    t = new Thread(ScanSimulationAnalog);
                else
                    t = new Thread(ScanRTUAnalog);
                t.Start(id);
            }

            foreach (int id in activeDigitalInputs.Keys)
            {
                Thread t;
                if ("SCR".Contains(activeDigitalInputs[id].IOAddress))
                    t = new Thread(ScanSimulationDigital);
                else
                    t = new Thread(ScanRTUDigital);
                t.Start(id);
            }


            Thread tBatch = new Thread(batchDBUpdate);
            tBatch.Start();
        }

        private void batchDBUpdate()
        {
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
                activeAnalogInputs.Add(input.Id, input);
            }
            foreach (DigitalInput input in tagRepository.getAllDigitalInputs())
            {
                activeDigitalInputs.Add(input.Id, input);
            }
        }

        public void ScanRTUAnalog(object param)
        {
            int id = (int)param;
            double currentValue = -10000;
            int scanTime = activeAnalogInputs[id].ScanTime;
            string address = activeAnalogInputs[id].IOAddress;
            List<Alarm> alarms;
            Alarm currentAlarm;
            while (true)
            {
                if (activeAnalogInputs[id].IsScanOn)
                {
                    // read new scanned value from dictionary
                    currentValue = IOEntryRepository.entries[address];

                    // check for alarms and alarm update if one is raised
                    // alarms are loaded every time because of posibility that new alarm has been added to the tag
                    lock (_lock)
                    {
                        alarms = activeAnalogInputs[id].Alarms.ToList();
                    }
                    currentAlarm = null;
                    foreach (Alarm alarm in activeAnalogInputs[id].Alarms.ToList())
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
                            activeAnalogInputs[id].Value = currentValue;
                        }
                        catch (Exception e)
                        {
                            // something
                        }
                    }

                    if (currentAlarm != null)
                    {
                        if (currentAlarm.Type == AlarmType.HIGHER)
                        {
                            currentValue = activeAnalogInputs[id].HighLimit;
                        } else
                        {
                            currentValue = activeAnalogInputs[id].LowLimit;
                        }
<<<<<<< HEAD
                        AlarmRecordDTO arDTO = new AlarmRecordDTO { TagId = activeAnalogInputs[address].Id, Priority = currentAlarm.Priority, Type = currentAlarm.Type, Value = currentAlarm.Value}
=======

                        AlarmRecord alarmRecord = new AlarmRecord { AlarmId = currentAlarm.Id, Timestamp = DateTime.Now, TagId = id };

>>>>>>> 3f80ffb3a05e4f9db645e3634bb44711ccd38748
                        lock (_lock)
                        {
                            alarmHub.Clients.All.SendAsync("alarm", arDTO);
                            udateAlarmHandler.SendDataToClient("alarm", currentAlarm);
                        }
                        AlarmRecord alarmRecord = new AlarmRecord { AlarmId = currentAlarm.Id, Timestamp = DateTime.Now, TagId = activeAnalogInputs[address].Id };

                        
                        lock (_lock)
                        {
                            alarmRecords.Add(alarmRecord);
                        }
                    }

                    // add new tagRecord every time AnalogInput is updated 
                    TagRecord tagRecord = new TagRecord { Tag = activeAnalogInputs[id], Value = currentValue, Timestamp = DateTime.Now, TagId = id };
                    lock(_lock)
                    {
                        tagRecords.Add(tagRecord);
                    }
                    lock (_lock)
                    {
                        inputHub.Clients.All.SendAsync("input", tagRecord);
                        updateInputHandler.SendDataToClient("input", tagRecord);
                    }
                }
                Thread.Sleep(scanTime);
            }
        }

        public void ScanSimulationAnalog(object param)
        {
            int id = (int)param;
            double currentValue = -10000;
            int scanTime = activeAnalogInputs[id].ScanTime;
            string address = activeAnalogInputs[id].IOAddress;
            while (true)
            {
                if (activeAnalogInputs[id].IsScanOn)
                {
                    // read new scanned value from dictionary and update AnalogInput's value in db
                    currentValue = SimulationDriver.ReturnValue(address);
                    lock (_lock)
                    {
                        try
                        {
                            activeAnalogInputs[id].Value = currentValue;
                        }
                        catch (Exception e)
                        {
                            // something
                        }
                    }

                    // add new tagRecord every time AnalogInput is updated 
                    TagRecord tagRecord = new TagRecord { Tag = activeAnalogInputs[id], Value = currentValue, Timestamp = DateTime.Now, TagId = id };
                    lock (_lock)
                    {
                        tagRecords.Add(tagRecord);
                    }
                    lock (_lock)
                    {
                        inputHub.Clients.All.SendAsync("input", tagRecord);
                        updateInputHandler.SendDataToClient("input", tagRecord);
                    }
                }
                Thread.Sleep(scanTime);
            }
        }

        public void ScanRTUDigital(object param)
        { 
            int id = (int)param;
            double currentValue = 0;
            int scanTime = activeDigitalInputs[id].ScanTime;
            string address = activeDigitalInputs[id].IOAddress;
            while (true)
            {
                if (activeDigitalInputs[id].IsScanOn)
                {
                    // read new scanned value from dictionary and update AnalogInput's value in db
                    currentValue = IOEntryRepository.entries[address];
                    lock (_lock)
                    {
                        try
                        {
                            activeDigitalInputs[id].Value = currentValue;
                        }
                        catch (Exception e)
                        {
                            // something
                        }
                    }

                    // add new tagRecord every time AnalogInput is updated 
                    TagRecord tagRecord = new TagRecord { Tag = activeDigitalInputs[id], Value = currentValue, Timestamp = DateTime.Now, TagId = id };
                    lock (_lock)
                    {
                        tagRecords.Add(tagRecord);
                    }
                    lock (_lock)
                    {
                        inputHub.Clients.All.SendAsync("input", tagRecord);
                        updateInputHandler.SendDataToClient("input", tagRecord);
                    }
                }
                Thread.Sleep(scanTime);
            }
        }

        public void ScanSimulationDigital(object param)
        {
            int id = (int)param;
            double currentValue = 0;
            int scanTime = activeDigitalInputs[id].ScanTime;
            string address = activeDigitalInputs[id].IOAddress;
            while (true)
            {
                if (activeDigitalInputs[id].IsScanOn)
                {
                    // read new scanned value from dictionary and update AnalogInput's value in db
                    currentValue = SimulationDriver.ReturnValue(address);
                    lock (_lock)
                    {
                        try
                        {
                            activeDigitalInputs[id].Value = currentValue;
                        }
                        catch (Exception e)
                        {
                            // something
                        }
                    }

                    // add new tagRecord every time AnalogInput is updated 
                    TagRecord tagRecord = new TagRecord { Tag = activeDigitalInputs[id], Value = currentValue, Timestamp = DateTime.Now, TagId = id };
                    lock (_lock)
                    {
                        tagRecords.Add(tagRecord);
                    }
                    lock (_lock)
                    {
                        inputHub.Clients.All.SendAsync("input", tagRecord);
                        updateInputHandler.SendDataToClient("input", tagRecord);
                    }
                }
                Thread.Sleep(scanTime);
            }
        }
    }
}

using snus_back.Models;
using snus_back.Repositories;

namespace snus_back.Services
{
    public class ScanService
    {
        //private ICollection<string> analogIOAddresses = new List<string>();
        //private ICollection<string> digitalIOAddresses = new List<string>();
        private Dictionary<string, AnalogInput> activeAnalogInputs= new Dictionary<string, AnalogInput>();
        private Dictionary<string, DigitalInput> activeDigitalInputs = new Dictionary<string, DigitalInput>();

        private TagRepository tagRepository;
        private IOEntryRepository ioEntryRepository;

        private readonly object _lock = new object();

        public ScanService(TagRepository tagRepository, IOEntryRepository iOEntryRepository)
        {
            this.tagRepository = tagRepository;
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
            while (true)
            {
                if (activeAnalogInputs[address].IsScanOn)
                {
                    currentValue = ioEntryRepository.entries[address];
                    lock (_lock)
                    {
                        try
                        {
                            tagRepository.UpdateAnalogInput(address, currentValue);
                        }
                        catch (Exception e)
                        {
                            // something
                        }
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
                    currentValue = SimulationDriver.ReturnValue(address);
                    lock (_lock)
                    {
                        try
                        {
                            tagRepository.UpdateAnalogInput(address, currentValue);
                        }
                        catch (Exception e)
                        {
                            // something
                        }
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
                    currentValue = ioEntryRepository.entries[address];
                    lock (_lock)
                    {
                        try
                        {
                            tagRepository.UpdateDigitalInput(address, currentValue);
                        }
                        catch (Exception e)
                        {
                            // something
                        }
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
                    currentValue = SimulationDriver.ReturnValue(address);
                    lock (_lock)
                    {
                        try
                        {
                            tagRepository.UpdateDigitalInput(address, currentValue);
                        }
                        catch (Exception e)
                        {
                            // something
                        }
                    }
                }
                Thread.Sleep(scanTime);
            }
        }
    }
}

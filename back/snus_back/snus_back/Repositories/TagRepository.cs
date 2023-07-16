using snus_back.data_access;
using snus_back.DTOs;
using snus_back.Models;
using snus_back.Services;
using System.Net;

namespace snus_back.Repositories
{
    public class TagRepository
    {
        private SNUSDbContext dbContext;
        public TagRepository(SNUSDbContext context)
        {
            this.dbContext = context;
        }

        public List<string> GetTakenAdresses()
        {
            var analogInputs = this.dbContext.AnalogInputs.Select(x => x.IOAddress).ToList();
            var digitalInputs = this.dbContext.DigitalInputs.Select(x => x.IOAddress).ToList();

            return analogInputs.Concat(digitalInputs).ToList();
        }

        public List<string> GetTakenOutputAdresses()
        {
            var analog = this.dbContext.AnalogOutputs.Select(x => x.IOAddress).ToList();
            var digital = this.dbContext.DigitalOutputs.Select(x => x.IOAddress).ToList();

            return analog.Concat(digital).ToList();
        }

        public ICollection<TagRecordDTO> getAllTagRecords()
        {
            ICollection<TagRecordDTO> ret = new List<TagRecordDTO>();
            foreach (var tagRecord in dbContext.TagRecords)
            {
                ret.Add(new TagRecordDTO(tagRecord));
            }

            return ret;
        }

        public void AddAlarmToTag(Alarm ret, int id)
        {
            AnalogInput analogInput = dbContext.AnalogInputs.Find(id);
            analogInput.Alarms.Add(ret);
            dbContext.SaveChanges();

        }

        public ICollection<TagRecordDTO> getAllTagByIOAddress(string address)
        {
            var  tagRecords = dbContext.TagRecords
                .Where(tr => tr.Tag.IOAddress == address)
                .ToList();

            ICollection<TagRecordDTO> ret = new List<TagRecordDTO>();
            foreach (var tagRecord in tagRecords)
            {
                ret.Add(new TagRecordDTO(tagRecord));
            }

            return ret;
        }

        public ICollection<InputTagDBManagerDTO> GetAllInputTagsDBManager()
        {
            var digitalInputs = dbContext.DigitalInputs.ToList();
            var analogInputs = dbContext.AnalogInputs.ToList();
            ICollection<InputTagDBManagerDTO> ret = new List<InputTagDBManagerDTO>();

            foreach (var digitalInput in digitalInputs)
            {
                ret.Add(new InputTagDBManagerDTO(digitalInput));
            }

            foreach (var analogInput in analogInputs)
            {
                ret.Add(new InputTagDBManagerDTO(analogInput));
            }

            return ret;
        }
        public ICollection<AnalogInputDTO> getAllAITags()
        {
            ICollection<AnalogInputDTO> ret = new List<AnalogInputDTO>();
            foreach (var analogInput in dbContext.AnalogInputs)
            {
                ret.Add(new AnalogInputDTO(analogInput));
            }

            return ret;
        }

        public ICollection<OutputTagDBManagerDTO> GetAllOutputTagsDBManager()
        {
            /*AnalogOutput ao = new AnalogOutput
            {
                Unit = "km",
                IOAddress = "s",
                Value = 10,
                Description = "najlepsi na svijet"
            };
            dbContext.AnalogOutputs.Add(ao);
            dbContext.SaveChanges();*/
            var digitalOutputs = dbContext.DigitalOutputs.ToList();
            var analogOutputs = dbContext.AnalogOutputs.ToList();
            ICollection<OutputTagDBManagerDTO> ret = new List<OutputTagDBManagerDTO>();

            foreach (var digitalOutput in digitalOutputs)
            {
                ret.Add(new OutputTagDBManagerDTO(digitalOutput));
            }

            foreach (var analogOutput in analogOutputs)
            {
                ret.Add(new OutputTagDBManagerDTO(analogOutput));
            }

            return ret;
        }
        public ICollection<DigitalInputDTO> getAllDITags()
        {
            ICollection<DigitalInputDTO> ret = new List<DigitalInputDTO>();
            foreach (var digitalInput in dbContext.DigitalInputs)
            {
                ret.Add(new DigitalInputDTO(digitalInput));
            }

            return ret;
        }

        public void UpdateAnalogOutputValue(int id, double value)
        {
            AnalogOutput analogOutput = dbContext.AnalogOutputs.Find(id);
            analogOutput.Value = value;
            dbContext.SaveChanges();
        }

        public void UpdateDigitalOutputValue(int id, double value)
        {
            DigitalOutput digitalOutput = dbContext.DigitalOutputs.Find(id);
            digitalOutput.Value = value;
            dbContext.SaveChanges();
        }

        public void DeleteAnalogOutput(int id)
        {
            AnalogOutput analogOutput = dbContext.AnalogOutputs.Find(id);
            dbContext.AnalogOutputs.Remove(analogOutput);
            dbContext.SaveChanges();
        }

        public void DeleteDigitalOutput(int id)
        {
            DigitalOutput digitalOutput = dbContext.DigitalOutputs.Find(id);
            dbContext.DigitalOutputs.Remove(digitalOutput);
            dbContext.SaveChanges();
        }

        public AnalogInput AddAnalogInput(AnalogInput value)
        {
            dbContext.AnalogInputs.Add(value);
            dbContext.SaveChanges();
            return value;
        }

        public void AddAnalogOutput(AnalogOutput value)
        {
            dbContext.AnalogOutputs.Add(value);
            dbContext.SaveChanges();
        }

        public DigitalInput AddDigitalInput(DigitalInput value)
        {
            dbContext.DigitalInputs.Add(value);
            dbContext.SaveChanges();
            return value;
        }

        public void AddDigitalOutput(DigitalOutput value)
        {
            dbContext.DigitalOutputs.Add(value);
            dbContext.SaveChanges();
        }

        public ICollection<AnalogInput> getAllAnalogInputs()
        {
            return dbContext.AnalogInputs.ToList();
        }

        public ICollection<DigitalInput> getAllDigitalInputs()
        {
            return dbContext.DigitalInputs.ToList();
        }

        public void UpdateAnalogInputs()
        {
            foreach (int id in ScanService.activeAnalogInputs.Keys)
            {
                AnalogInput analogInput = dbContext.AnalogInputs.FirstOrDefault(input => input.Id == id);
                if (analogInput == null)
                {
                    throw new Exception("AnalogInput not found.");
                }
                else
                {
                    analogInput.Value = ScanService.activeAnalogInputs[id].Value;
                }
            }
            dbContext.SaveChanges();
        }

        public void UpdateDigitalInputs()
        {
            foreach (int id in ScanService.activeDigitalInputs.Keys) { 

                DigitalInput digitalInput = dbContext.DigitalInputs.FirstOrDefault(input => input.Id == id);
                if (digitalInput == null)
                {
                    continue;
                }
                else
                {
                    digitalInput.Value = ScanService.activeDigitalInputs[id].Value;
                }
            }
            dbContext.SaveChanges();
        }

        public void AddTagRecords(ICollection<TagRecord> tagRecords)
        {
            foreach (TagRecord tagRecord in tagRecords)
            {
                dbContext.TagRecords.Add(tagRecord);
            }
            dbContext.SaveChanges();
        }

        public AnalogInput UpdateAnalogInputScan(int id, Boolean value)
        {
            AnalogInput analogInput = dbContext.AnalogInputs.Find(id);
            if (analogInput != null)
            {
                analogInput.IsScanOn = value;
                dbContext.SaveChanges();
                return analogInput;
            }
            else
            {
                throw new Exception("No analog input with given id exists.");
            }

            return null;
        }

        public DigitalInput UpdateDigitalInputScan(int id, Boolean value)
        {
            DigitalInput digitalInput = dbContext.DigitalInputs.Find(id);
            if (digitalInput != null)
            {
                digitalInput.IsScanOn = value;
                dbContext.SaveChanges();
                return digitalInput;
            }
            else
            {
                throw new Exception("No digital input with given id exists.");
            }

            return null;
        }

        public string DeleteAnalogInput(int id)
        {
            AnalogInput analogInput = dbContext.AnalogInputs.Find(id);
            string ioAddress = analogInput.IOAddress;
            dbContext.AnalogInputs.Remove(analogInput);
            dbContext.SaveChanges();

            return ioAddress;
        }

        public void DeleteDigitalInput(int id)
        {
            DigitalInput digitalInput = dbContext.DigitalInputs.Find(id);
            dbContext.DigitalInputs.Remove(digitalInput);
            dbContext.SaveChanges();

        }

        public AnalogInput GetAnalogInputById(int id)
        {
            return dbContext.AnalogInputs.Find(id);
        }

    }
}

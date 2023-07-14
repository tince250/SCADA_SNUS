﻿using snus_back.data_access;
using snus_back.DTOs;
using snus_back.Models;

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

        public ICollection<TagRecordDTO> getAllTagByIOAddress(string address)
        {
            var  tagRecrods = dbContext.TagRecords
                .Where(tr => tr.Tag.IOAddress == address)
                .ToList();

            ICollection<TagRecordDTO> ret = new List<TagRecordDTO>();
            foreach (var tagRecord in dbContext.TagRecords)
            {
                ret.Add(new TagRecordDTO(tagRecord));
            }

            return ret;
        }

        public ICollection<OutputTagDBManagerDTO> GetAllOutputTagsDBManager()
        {
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

        public void AddAnalogInput(AnalogInput value)
        {
            dbContext.AnalogInputs.Add(value);
            dbContext.SaveChanges();
        }

        public void AddAnalogOutput(AnalogOutput value)
        {
            dbContext.AnalogOutputs.Add(value);
            dbContext.SaveChanges();
        }

        public void AddDigitalInput(DigitalInput value)
        {
            dbContext.DigitalInputs.Add(value);
            dbContext.SaveChanges();
        }

        public void AddDigitalOutput(DigitalOutput value)
        {
            dbContext.DigitalOutputs.Add(value);
            dbContext.SaveChanges();
        }
    }
}

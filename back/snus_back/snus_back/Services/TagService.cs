﻿using snus_back.DTOs;
using snus_back.Models;
using snus_back.Repositories;
using snus_back.Services.ServiceInterfaces;

namespace snus_back.Services
{
    public class TagService : ITagService
    {
        private TagRepository allTags;

        public TagService(TagRepository allTags) 
        {
            this.allTags = allTags;
        }

        public ICollection<Tag> getAllTagByIOAddress(string address)
        {
            return allTags.getAllTagByIOAddress(address);
        }

        public ICollection<TagRecordDTO> getAllTagRecords()
        {
            return allTags.getAllTagRecords();
        }
    }
}

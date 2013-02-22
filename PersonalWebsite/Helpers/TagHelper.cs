﻿using PersonalWebsite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace PersonalWebsite.Helpers
{
    public static class TagHelper
    {
        public static void AddTag(string tag)
        {
            using (var context = new WebsiteContext())
            {
                var tag_exists = context.Tags.Any(x => x.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase));
                if (!tag_exists)
                {
                    var newtag = context.Tags.Create();
                    newtag.TagName = tag;
                    context.Tags.Add(newtag);
                    context.SaveChanges();                    
                }
            }
        }

        public static void AddTagRange(string[] tags)
        {            
            foreach (string tag in tags) AddTag(tag);
        }

        public static Tag GetTag(string tag)
        {
            using (var context = new WebsiteContext())
            {
                if (tag == "") return null;
                return context.Tags.First(x => x.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase));
            }
        }

        public static Tag GetTag(WebsiteContext context, string tag)
        {
            if (tag == "") return null;
            return context.Tags.First(x => x.TagName.Equals(tag, StringComparison.OrdinalIgnoreCase));
        }

        public static string[] GetTagArray(string list)
        {
            if (list == "") return new string[] { };
            return list.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] GetTagArray(List<Tag> tags)
        {
            if (tags == null) return new string[] { };
            List<string> list = new List<string>();
            foreach (var tag in tags)
                list.Add(tag.TagName);
            return list.ToArray();                
        }

        public static List<Tag> GetTagRange(string[] range)
        {
            List<Tag> list = new List<Tag>();
            foreach (var t in range)
            {
                Tag tag = GetTag(t);
                if (tag != null) list.Add(tag);
            }
            return list;
        }
    }
}
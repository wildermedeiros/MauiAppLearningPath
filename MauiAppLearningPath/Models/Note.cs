using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppLearningPath.Models
{
    public class Note
    {
        public string Description { get; set; }

        public Note()
        {

        }

        public Note(string description)
        {
            Description = description;
        }
    }
}

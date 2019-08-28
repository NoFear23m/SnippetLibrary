using SnippetLibrary.App.Model.SnippetProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippetLibrary.App.Model
{
    public class Snippet
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public List<SnippetEnitry> SnippetEnitries { get; set; }

        public Guid LanguageID { get; set; }
        public List<Tag> Tags { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public Snippet()
        {
            ID = Guid.NewGuid();
            SnippetEnitries = new List<SnippetEnitry>();
            Tags = new List<Tag>();
        }

    }
}

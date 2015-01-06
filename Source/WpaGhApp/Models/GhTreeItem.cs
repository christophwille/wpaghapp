using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;

namespace WpaGhApp.Models
{
    public class GhTreeItem
    {
        public GhTreeItem()
        {
        }

        public GhTreeItem(Octokit.TreeItem ti, string name, string relativePath)
        {
            Name = name;
            RelativePath = relativePath;

            Path = ti.Path;
            Mode = ti.Mode;
            ItemType = ti.Type;
            Size = ti.Size;
            Sha = ti.Sha;
            Url = ti.Url;
        }

        public string Name { get; set; }
        public string RelativePath { get; set; }

        public string ItemTypeAsFontawesomeGlyph
        {
            get
            {
                switch (ItemType)
                {
                    case TreeType.Blob:
                        return "\uf016"; // fa-file-o
                        break;
                    case TreeType.Tree:
                        return "\uf114"; // fa-folder-o
                        break;
                    case TreeType.Commit:
                        return "\uf003"; //  fa-envelope-o
                        break;
                    default:
                        return "\uf10c"; // fa-circle-o
                }
            }
        }

        public string Path { get; set; }
        public string Mode { get; set; }
        public Octokit.TreeType ItemType { get; set; }
        public int Size { get; set; }
        public string Sha { get; set; }
        public Uri Url { get; set; }
    }
}

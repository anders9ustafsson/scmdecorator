using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace scmdecorator
{
    class Program
    {
        private static readonly string ApplicationName = "scmdecorator";

        private class ScmInfo
        {
            internal string Scm;
            internal string Folder;
            internal string Icon;
        }

        private static readonly List<ScmInfo> ScmInfos =
            new List<ScmInfo>
                {
                    new ScmInfo { Scm = "Subversion", Folder = ".svn", Icon = "svn.ico" },
                    new ScmInfo { Scm = "Git", Folder = ".git", Icon = "git.ico" },
                    new ScmInfo { Scm = "Mercurial", Folder = ".hg", Icon = "hg.ico" }
                };

        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("usage: {0} [scm-folder]", ApplicationName);
                return;
            }

            string scmFolder;
            if (args.Length == 1)
            {
                scmFolder = args[0];
            }
            else
            {
                Console.Write("Enter name of SCM folder: ");
                scmFolder = Console.ReadLine() ?? String.Empty;
            }

            ScmInfo currentScmInfo;
            try
            {
                if (!Directory.Exists(scmFolder))
                {
                    Console.WriteLine("{0}: Folder '{1}' does not exist.", ApplicationName, scmFolder);
                    return;
                }

                var dirInfo = new DirectoryInfo(scmFolder);
                currentScmInfo = ScmInfos.SingleOrDefault(info => dirInfo.GetDirectories(info.Folder).Length > 0);
                if (currentScmInfo == null)
                {
                    Console.WriteLine("{0}: Folder '{1}' does not appear to be an SCM folder; support directory is missing", ApplicationName, scmFolder);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}: Error when accessing folder, message: {1}", ApplicationName, e.Message);
                return;
            }


        }
    }
}

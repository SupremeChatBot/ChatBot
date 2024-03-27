using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Constants
{
    public class FilePaths
    {
        private static string workingDirectory = Environment.CurrentDirectory;
        private static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
        public static string ConversationIdJson = $"{projectDirectory}\\ChatBot_Repo\\AppData\\conversations.json";
    }
}

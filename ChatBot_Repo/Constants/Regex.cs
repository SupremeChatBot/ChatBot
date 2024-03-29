using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Constants
{
    public class RegexStrings
    {
        public static string ValidConversationName = @"^(?=.{1,50}$)[a-zA-Z0-9\s\-',.()!?]+$";
    }
}

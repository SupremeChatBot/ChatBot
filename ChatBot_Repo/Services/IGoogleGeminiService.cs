using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot_Repo.Services
{
    public interface IGoogleGeminiService
    {
        Task<string> Chat(string prompt);
    }
}

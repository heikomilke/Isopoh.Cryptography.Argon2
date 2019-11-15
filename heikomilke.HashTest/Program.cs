using System;
using Isopoh.Cryptography.Argon2;

namespace heikomilke.HashTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            while (true)
            {
                string bogusPass = Guid.NewGuid().ToString();
                Console.WriteLine("Creating hash for: "+bogusPass);
                string hash= Argon2.Hash(
                    password: bogusPass,
                    type: Argon2Type.DataDependentAddressing,
                    memoryCost: 4096
                );
                Console.WriteLine("Hash for: "+bogusPass + " is " + hash);
                
            }
        }
    }
}
using System;
using System.IO;
using Akka.Actor;
using Akka.Configuration;

namespace SeedNode
{
    public static class HoconLoader
    {
        public static Config FromFile(string path)
        {
            return ConfigurationFactory.ParseString(File.ReadAllText(path));
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("ActorCluster", HoconLoader.FromFile("config.hocon"));

            Console.ReadLine();
        }
    }
}
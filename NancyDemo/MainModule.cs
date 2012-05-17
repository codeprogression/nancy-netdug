using System;
using Nancy;
using NancyDemo.Models;
using System.Linq;

namespace NancyDemo
{
    public class MainModule : NancyModule
    {
        public MainModule(Repository repository)
        {
            Get["/"] = _ => View["index"];
            Get["/spec"] = _ => View["spec"];
        }
    }
}
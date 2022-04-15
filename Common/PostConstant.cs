﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class PostConstant
    {
        public static string Event = "Event";
        public static string CollaborativeProgram = "CollaborativeProgram";
        public static string News = "News";
        public static string Blog = "Blog";
    }

    public class RegisterConstant
    {
        public static string Event = "Event";
        public static string Home = "Home";
        public static string Scholarship = "Scholarship";
    }

    public enum PrizeType
    {
        FirstPrize = 1,
        ConsolationPrize = 5
    }
}

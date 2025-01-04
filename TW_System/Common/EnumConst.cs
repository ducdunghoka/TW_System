using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TW.Common
{
    public class AuthenticationTypes
    {
        public const string ApplicationCookie = "Identity.Application";
    }
    public static class EnumDashboardType
    {
        public const int MenuTitle = 1;
        public const int DxDashboard = 2;
        public const int MVCAction = 3;

        public static Dictionary<int, string> GetKeys = new()
        {
            { 1, "Menu title" },
            { 3, "MVC Action" }
        };
    }

    public class AuthorizeName
    {
        public static class Admin
        {
            public const string Menu = "Admin.Menu";
            public const string Language = "Admin.Language";
            public const string Roles = "Admin.Roles";
            public const string Authorize = "Admin.Authorize";
        }

        public static class MES{
            public const string Inventory = "MES.Inventory";
            public const string Distribution = "MES.Distribution";
            public const string Config = "MES.Config";
            public const string Member = "MES.Member";
        }
    }

    public static class AuthorizeMethod
    {
        public const string View = "View";
        public const string Add = "Add";
        public const string Modify = "Modify";
        public const string Delete = "Delete";
        public const string Import = "Import";
        public const string Control = "Control";
    }

    public static class RoleType
    {
        public static int Admin = 1;
        public static int User = 100;
        //public static int Modified = 2;

        public static KeyValuePair<int, string>[] ToArray(){
            var lang = (Thread.CurrentThread.CurrentCulture).Name;
            return new Dictionary<int, string>(){
                {Admin, lang == "vi"? "Quản trị viên" : "Admin"},
                {User, lang == "vi"? "Người dùng" : "User"},
            }.ToArray();
        }
    }
}

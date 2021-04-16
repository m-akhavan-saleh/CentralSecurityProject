using System;
using System.ComponentModel;

namespace CentralSecurityProject.Common
{
    public class MyDisplayAttribute : DisplayNameAttribute
    {
        public MyDisplayAttribute(string resourceName) : base(GetDisplayNameFromResource(resourceName))
        {
        }

        private static string GetDisplayNameFromResource(string resourceName)
        {
            string result = string.Empty;
            switch (resourceName)
            {
                case "RoleId":
                    result = Models.Resources.Resource.RoleId;
                    break;
                case "RoleCode":
                    result = Models.Resources.Resource.RoleCode;
                    break;
                case "RoleName":
                    result = Models.Resources.Resource.RoleName;
                    break;
                case "IsActive":
                    result = Models.Resources.Resource.IsActive;
                    break;
                case "Description":
                    result = Models.Resources.Resource.Description;
                    break;
                default:
                    break;
            }
            return result;
        }
    }

    public class MyStringValueAttribute : Attribute
    {
        private string _value;

        /// <summary>
        /// Creates a new <see cref="MyStringValueAttribute"/> instance.
        /// </summary>
        /// <param name="value">Value.</param>
        public MyStringValueAttribute(string value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value></value>
        public string Value
        {
            get { return _value; }
        }
    }
}
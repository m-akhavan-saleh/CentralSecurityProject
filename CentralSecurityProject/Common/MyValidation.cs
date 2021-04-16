using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;
using System.Reflection;

namespace CentralSecurityProject.Common
{
    /// <summary>
    /// کلاس تنظیم شده در خصوص اعتبار سنجی از نوع اجباری
    /// </summary>
    public class MyRequired : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{validationContext.DisplayName} قید نشده است.");
            }
            else
            {
                //return new ValidationResult(null);
                //return base.IsValid(value, validationContext);
                return ValidationResult.Success;
            }
        }
    }

    /// <summary>
    /// کلاس تنظیم شده در خصوص اعتبار سنجی طول رشته
    /// </summary>
    public class MyStringLength : StringLengthAttribute
    {
        private int _maximumLength;

        public MyStringLength(int maxLength) : base(maxLength)
        {
            _maximumLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (MinimumLength != 0)
                {
                    if (value.ToString().Length > _maximumLength || value.ToString().Length < MinimumLength)
                    {
                        return new ValidationResult($"طول رشته {validationContext.DisplayName} نمی تواند بیشتر از {_maximumLength} و کمتر از {MinimumLength} کراکتر تعریف شود.");
                    }
                }
                else
                {
                    if (value.ToString().Length > _maximumLength)
                    {
                        return new ValidationResult($"طول رشته {validationContext.DisplayName} نمی تواند بیشتر از {_maximumLength} کراکتر تعریف شود.");
                    }
                }
            }

            return ValidationResult.Success;
        }

    }

    /// <summary>
    /// کلاس تنظیم شده در خصوص مقایسه دو خصوصیت با همدیگر
    /// </summary>
    public class MyCompare : CompareAttribute
    {
        private string _otherProperty;

        public MyCompare(string otherProperty) : base(otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_otherProperty);
            if (property == null)
            {
                return new ValidationResult(string.Format("خصوصیت با عنوان {0} شناخته شده نیست.", _otherProperty));
            }

            var otherValue = property.GetValue(validationContext.ObjectInstance, null);
            if (value != null && otherValue != null)
            {
                var propDisplayName = property.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                if (!String.IsNullOrEmpty(value.ToString()) && !String.IsNullOrEmpty(otherValue.ToString()))
                {
                    if (value.ToString().Trim() != otherValue.ToString().Trim())
                    {
                        return new ValidationResult($"مقدار ورودی {validationContext.DisplayName} با مقدار ورودی {propDisplayName.Description} مطابقت ندارد.");
                    }
                }
            }
            return null;
        }
    }

    /// <summary>
    /// کلاس تنظیم شده در خصوص اعتبار سنجی طول رشته ورودی
    /// </summary>
    public class MyMaxLength : MaxLengthAttribute
    {
        private int _maxLength;
        public MyMaxLength(int length) : base(length)
        {
            _maxLength = length;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value.ToString().Length > _maxLength)
                {
                    return new ValidationResult($"طول رشته {validationContext.DisplayName} نمی تواند بیشتر از {_maxLength} کراکتر تعریف شود.");
                }
            }

            //return base.IsValid(value, validationContext);
            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// کلاس تنظیم شده در خصوص اعتبار سنجی دامنه اطلاعات ورودی
    /// </summary>
    public class MyRange : RangeAttribute
    {
        private int _minLength;
        private int _maxLength;
        public MyRange(int minLength, int maxLength) : base(minLength, maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                int val = 0;
                int.TryParse(value.ToString(), out val);
                if (val < _minLength || val > _maxLength)
                {
                    return new ValidationResult($"{validationContext.DisplayName} می بایست بین عدد {_minLength} تا {_maxLength} تعریف شود.");
                }
            }

            return ValidationResult.Success;
        }
    }

    /// <summary>
    /// کلاس تنظیم شده در خصوص اعتبار سنجی متن ارسالی
    /// از بابت استفاده از کلمات غیر معمول
    /// </summary>
    public class UnusualWordAttribute : ValidationAttribute
    {
        private int _maxUnusualWord;

        public UnusualWordAttribute(int MaxUnusualWord)
        {
            _maxUnusualWord = MaxUnusualWord;
        } // Method Name On Time Show :  UnusualWord

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                int cnt = 0;
                foreach (string item in value.ToString().Split(' ').ToList())
                {
                    if (item == "احمق") cnt += 1;
                    if (item == "دیوانه") cnt += 1;
                    if (item == "نکبت") cnt += 1;
                }

                if (cnt > _maxUnusualWord)
                {
                    return new ValidationResult($"تعداد کلمات غیر معمول بکار رفته در {validationContext.DisplayName} بیشتر از {_maxUnusualWord} کلمه شده است.");
                }
            }
            return ValidationResult.Success;
        }
    }
}

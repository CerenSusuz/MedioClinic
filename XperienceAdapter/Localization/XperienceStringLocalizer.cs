using CMS.Helpers;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace XperienceAdapter.Localization
{
    //Create a string localizer
    public class XperienceStringLocalizer : IStringLocalizer
    {
        private string _cultureName;

        public XperienceStringLocalizer() : this(Thread.CurrentThread.CurrentUICulture) { }

        public XperienceStringLocalizer(CultureInfo culture)
        {
            _cultureName = culture.Name;
        }


        private string GetString(string key) =>
            ResHelper.GetString(key, _cultureName);

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);

                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);

                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        /// <summary>
        /// does not make sense in Experience websites, therefore should throw a NotImplementedException.
        /// </summary>
        /// <param name="includeParentCultures"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// returns a new instance of the localizer through the above parametrized constructor.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IStringLocalizer WithCulture(CultureInfo culture) => new XperienceStringLocalizer(culture);
    }
}

//In the XperienceAdapter project, create a "Localization" folder.
//In the folder, create a new "XperienceStringLocalizer" class file
//Make the class implement IStringLocalizer.
//Add a string field for the culture name and populate it through a parametrized constructor.
//Add another parameterless constructor that calls the parametrized one with a current thread's culture as an argument.
//In the next steps, implement the GetAllStrings and WithCulture interface methods.

//Create an expression-bodied GetString method that wraps the call to ResHelper.GetString() with a key and the _cultureName field value.
//Finally, implement two C# indexers that accept a name string value and thereby get the appropriate Xperience resource strings, through the GetString method you've just created. As dictated by the IStringLocalizer contract, return a LocalizedString object.